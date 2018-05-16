using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityManager : MonoBehaviour {

    public BoxCollider2D hitbox;

    public BoxCollider2D hurtbox;

    public StanceManager sm;

    public Rigidbody2D rb2d;

    public SpriteRenderer sprend;

    public Animator anim;

    public AttackHandler ah;
    public ParryHandler ph;

    public float speed;
	public float attackSpeed;

	public int health;
	public int damage;

    public int looking; // 1 if looking right, -1 if looking left

	public bool staggering = false;

    public bool death = false;

    private int damageFlashFrames = 8;
    private int damageFlashFramesCounter = 0;

	private float rightXBound = 8.0f;
	private float leftXBound = -8.0f;

    private float hurtBoxXPosition;

    // Use this for initialization
    void Start () {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
		hurtbox = gameObject.transform.Find("SpriteHolder").Find("HitDetector").GetComponent<BoxCollider2D>();
        sm = gameObject.GetComponent<StanceManager>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        sprend = gameObject.GetComponentInChildren<SpriteRenderer>();

        anim = gameObject.GetComponentInChildren<Animator>();

        ah = gameObject.GetComponent<AttackHandler>();

        ph = gameObject.GetComponent<ParryHandler>();

        hurtBoxXPosition = hurtbox.transform.position.x;

		if (this.gameObject.tag == "Player" && LevelConfigManager.playerHealth > 0 ){
			health = LevelConfigManager.playerHealth;
		}

	}

	
	// Update is called once per frame
	void Update () {
        //Debug.Log(gameObject);
        //Debug.Log(staggering);
		if (this.gameObject.tag == "Player") {
			LevelConfigManager.playerHealth = health;
		}
		if (LevelConfigManager.GameOver) {
			StartCoroutine (waiter ());
		}

		if (staggering) {
            // removed push back for parry riposte
            rb2d.velocity = new Vector2(0, 0);
		} else {
			if (rb2d.velocity.x > 0) {
				looking = 1;
			} else if (rb2d.velocity.x < 0) {
				looking = -1;
			}

			LevelConfigManager.looking = looking;

			gameObject.transform.localScale = looking > 0 ? new Vector3 (1, 1, 1) : new Vector3 (-1, 1, 1);
            if(gameObject.CompareTag("Enemy"))
            {
                Canvas healthCanvas = gameObject.GetComponentInChildren<Canvas>();
                healthCanvas.transform.localScale = new Vector3(looking, 1, 1);
                Text textLabel = healthCanvas.GetComponentInChildren<Text>();
				textLabel.fontSize = 20;
				textLabel.alignment = TextAnchor.MiddleCenter;
				string healthLabel = "";
				for (int i = 0; i < health; i++) { 
					healthLabel += "︎❤";
				}
				textLabel.text = "<color='red'>" + healthLabel + "</color>";
		    }

		float xPosition = rb2d.transform.position.x;
            if (gameObject.CompareTag("Player"))
            {
                if (xPosition > rightXBound)
                {
                    xPosition = rightXBound;
                    transform.position = new Vector3(xPosition, -1.15f, 0);
					if (LevelConfigManager.EnemiesDefeated >= LevelConfigManager.EnemiesToDefeat && GameObject.FindGameObjectsWithTag ("Enemy").Length == 0 && !LevelConfigManager.GameOver)
                    {
                        UIScript uiScript = GameObject.Find("UIScript").GetComponent<UIScript>();
                        uiScript.BeginTransition();
                    }
                }
                else if (xPosition < leftXBound)
                {
                    xPosition = leftXBound;
                    transform.position = new Vector3(xPosition, -1.15f, 0);
                }
            }
		}



        if(damageFlashFramesCounter > 1)
        {
            damageFlashFramesCounter -= 1;
            sprend.color = Color.red;
        }else if(damageFlashFramesCounter - 1 == 0)
        {
            damageFlashFramesCounter -= 1;
            sprend.color = Color.white;
        }
	}

    public void MoveEntity(float translation)
    {
		if (!staggering && !death) {
			float xPosition = rb2d.transform.position.x;

			if (ah.anim.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") ||
			    ah.anim.GetCurrentAnimatorStateInfo (0).IsTag ("Parry")) {
				anim.SetFloat ("speed", 0);
				rb2d.velocity = new Vector2 (0, 0);
			} else {
				anim.SetFloat ("speed", Mathf.Abs (translation * speed));
				rb2d.velocity = new Vector2 (translation * speed, 0);
				if (gameObject.tag == "Player") {

				}
			}
		} else {
			rb2d.velocity = new Vector2 (0, 0);

		}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

		if (this.gameObject.tag == "Enemy" && collision.GetComponentInParent<EntityManager> ().gameObject.tag == "Enemy") {
			return;
		}
        if (collision.gameObject.tag == "HurtBox")
        {
            StanceManager other_sm = collision.gameObject.GetComponentInParent<StanceManager>();

            if (ph.isParryFramesActive) {
                // Stagger enemy and cause player to attack immediately
                collision.gameObject.GetComponentInParent<EntityManager>().StaggerEntity();

                // End the parry
                ph.CancelParry();

                // Attack
                anim.SetBool("isAttacking", true); // Need to do it this way because it seems that the animator doesn't leave parry 
                                                   // state fast enough to use request attack
                //ah.RequestAttack();
            }
            else
            {

                // If stances do not match or the other entity is staggered
                if (sm.currentStance != other_sm.currentStance || staggering)
                {
					
                    //Debug.Log(gameObject);
                    //Debug.Log("DEALING DAMGE TO OTHER");
					if (this.gameObject.tag == "Player") {
						string enemyName = other_sm.name.Replace ("(Clone)", "");
						LevelConfigManager.dataManager.enemyHit (enemyName, LevelConfigManager.Level, Time.time);
					}
					if (health > collision.gameObject.GetComponentInParent<EntityManager>().damage)
                    {
                        damageFlashFramesCounter = damageFlashFrames;

						if (staggering && collision.GetComponentInParent<EntityManager>().gameObject.tag != "Enemy") {
							health -= 2 * collision.gameObject.GetComponentInParent<EntityManager> ().damage;
						} else {
							health -= collision.gameObject.GetComponentInParent<EntityManager> ().damage;
						}

						if (health == 0) {
							StartDeath ();
						}
					}
                    else
                    {
                        StartDeath();
                    }
                }
                else
                {

                    StaggerEntity();
                    collision.gameObject.GetComponentInParent<EntityManager>().StaggerEntity();
                }
            }

        }
    }

    public void StartDeath() {
        //Debug.Log(gameObject + "StartDeath");
		if (this.gameObject.tag == "Enemy") {
			Canvas healthCanvas = gameObject.GetComponentInChildren<Canvas> ();
			healthCanvas.transform.localScale = new Vector3 (looking, 1, 1);
			Text textLabel = healthCanvas.GetComponentInChildren<Text> ();
			/*textLabel.fontSize = 20;
			textLabel.alignment = TextAnchor.MiddleCenter;
			string healthLabel = "";
			for (int i = 0; i < health; i++) { 
				healthLabel += "︎❤";
			}*/
			textLabel.text = ""; //"<color='red'>" + healthLabel + "</color>";*
		}

        death = true;
        anim.SetBool("isDying", true);
        anim.speed = 1;
		rb2d.velocity = new Vector2(0, 0);
    }


    public void DestroyEntity()
    {
        Debug.Log(gameObject + "DestroyEntity");
        //UIScript ui = GameObject.Find("UIScript").GetComponent("UIScript") as UIScript;
        if (this.gameObject.tag == "Player")
        {
            //SpawnScript spawn = GameObject.Find("SpawnScript").GetComponent("SpawnScript") as SpawnScript;
            //spawn.gameOver = true;

            //ui.gameOver = true;
            //ui.Timer.text = "Game Over";
			LevelConfigManager.playerHealth = 0;
			LevelConfigManager.dataManager.levelComplete(LevelConfigManager.Level, Time.time, LevelConfigManager.EnemiesDefeated, LevelConfigManager.EnemiesSpawned, "game_over"); //Store Data for level
			LevelConfigManager.Timer.text = "Game Over";
			LevelConfigManager.GameOver = true;
			AudioClip gameOverMusic = GameObject.Find ("UIScript").GetComponent<UIScript> ().gameOverMusic;
			AudioSource musicSource = GameObject.Find ("musicSource").GetComponent<AudioSource> ();
			musicSource.clip = gameOverMusic;
			musicSource.Play ();

			Object.Destroy (this.gameObject);
			/*GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in enemies)
			{
				Object.Destroy(enemy);
			}
			Object.Destroy (this.gameObject);*/
        }
		else if (this.gameObject.tag == "Enemy") {
			Object.Destroy(this.gameObject);
			LevelConfigManager.EnemiesDefeated = LevelConfigManager.EnemiesDefeated + 1;
		}
    }

	public void StaggerEntity(){
		ah.CancelAttack();
        ph.CancelParry();

        anim.SetBool("isStaggering", true);
        sprend.color = Color.yellow;
		staggering = true;

        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiStunSpeed;
        }
        else if (gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiStunSpeed;
        }
    }

    public void EndStagger()
    {
        //Debug.Log("End Stagger");
        staggering = false;
        anim.SetBool("isStaggering", false);
        sprend.color = Color.white;
        anim.speed = 1;
    }

	IEnumerator waiter() {
		yield return new WaitForSeconds (1);
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies)
		{
			Object.Destroy(enemy);
		}
	}
}
