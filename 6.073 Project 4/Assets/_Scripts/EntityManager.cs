using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (this.gameObject.tag == "Player") {
			LevelConfigManager.playerHealth = health;
		}
		if (LevelConfigManager.GameOver) {
			StartCoroutine (waiter ());
		}

		if (staggering) {
            // removed push back for parry riposte

		} else {
			if (rb2d.velocity.x > 0) {
				looking = 1;
			} else if (rb2d.velocity.x < 0) {
				looking = -1;
			}

			gameObject.transform.localScale = looking > 0 ? new Vector3 (1, 1, 1) : new Vector3 (-1, 1, 1);
		}

		float xPosition = rb2d.transform.position.x;
		if (!gameObject.CompareTag ("Enemy")) {
			if (xPosition > rightXBound) {
				xPosition = rightXBound;
				transform.position = new Vector3 (xPosition, -1.15f, 0);
			} else if (xPosition < leftXBound) {
				xPosition = leftXBound;
				transform.position = new Vector3 (xPosition, -1.15f, 0);
			}
		}
	}

    public void MoveEntity(float translation)
    {
		if (!staggering) {
			float xPosition = rb2d.transform.position.x;
			if (!gameObject.CompareTag ("Enemy")) {
				if (xPosition > rightXBound) {
					xPosition = rightXBound;
					transform.position = new Vector3 (xPosition, -1.15f, 0);
				} else if (xPosition < leftXBound) {
					xPosition = leftXBound;
					transform.position = new Vector3 (xPosition, -1.15f, 0);
				}
			}


			if (ah.anim.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") ||
                ah.anim.GetCurrentAnimatorStateInfo(0).IsTag("Parry")) {
				anim.SetFloat ("speed", 0);
				rb2d.velocity = new Vector2 (0, 0);
			} else {
				anim.SetFloat ("speed", Mathf.Abs (translation * speed));
				rb2d.velocity = new Vector2 (translation * speed, 0);
				if (gameObject.tag == "Player") {

				}
			}
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
                collision.gameObject.GetComponentInParent<EntityManager>().StaggerEntity();
            }
            else
            {
                if (sm.currentStance != other_sm.currentStance)
                {
					if (health > collision.gameObject.GetComponentInParent<EntityManager>().damage)
                    {
                        health -= collision.gameObject.GetComponentInParent<EntityManager>().damage;
                    }
                    else
                    {
                        DestroyEntity();
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

    public void DestroyEntity()
    {
        //UIScript ui = GameObject.Find("UIScript").GetComponent("UIScript") as UIScript;
        if (this.gameObject.tag == "Player")
        {
            //SpawnScript spawn = GameObject.Find("SpawnScript").GetComponent("SpawnScript") as SpawnScript;
            //spawn.gameOver = true;

            //ui.gameOver = true;
            //ui.Timer.text = "Game Over";
			LevelConfigManager.playerHealth = 0;
            LevelConfigManager.dataManager.levelComplete("game_over");
            LevelConfigManager.dataManager.storeData();
			LevelConfigManager.Timer.text = "Game Over";
			LevelConfigManager.GameOver = true;

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
		ah.EndAttack ();
        ph.EndParry();

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
        Debug.Log("End Stagger");
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
