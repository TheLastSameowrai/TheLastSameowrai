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

    public float speed;

    public int looking; // 1 if looking right, -1 if looking left

	private float rightXBound = 8.0f;
	private float leftXBound = -8.0f;

    private float hurtBoxXPosition;

    // Use this for initialization
    void Start () {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        hurtbox = gameObject.transform.Find("HitDetector").GetComponent<BoxCollider2D>();
        sm = gameObject.GetComponent<StanceManager>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        sprend = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();

        ah = gameObject.GetComponent<AttackHandler>();

        hurtBoxXPosition = hurtbox.transform.position.x;
	}

	
	// Update is called once per frame
	void Update () {
		if(rb2d.velocity.x > 0)
        {
            looking = 1;
        }else if(rb2d.velocity.x < 0)
        {
            looking = -1;
        }

        gameObject.transform.localScale = looking > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        
	}

    public void MoveEntity(float translation)
    {
		float xPosition = rb2d.transform.position.x;
		if (xPosition > rightXBound) 
		{
			xPosition = rightXBound;
			transform.position = new Vector3 (xPosition, 0, 0);
		}
		else if (xPosition < leftXBound) {
			xPosition = leftXBound;
			transform.position = new Vector3 (xPosition, 0, 0);
		}

        if (ah.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetFloat("speed", 0);
            rb2d.velocity = new Vector2(0, 0);
        }
        else
        {
            anim.SetFloat("speed", Mathf.Abs(translation * speed));
            rb2d.velocity = new Vector2(translation * speed, 0);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HurtBox")
        {
            StanceManager other_sm = collision.gameObject.GetComponentInParent<StanceManager>();
            if(sm.currentStance != other_sm.currentStance)
            {
                DestroyEntity();
            }
            
        }
    }

    public void DestroyEntity()
    {
        UIScript ui = GameObject.Find("UIScript").GetComponent("UIScript") as UIScript;
        if (this.gameObject.tag == "Player")
        {
            SpawnScript spawn = GameObject.Find("SpawnScript").GetComponent("SpawnScript") as SpawnScript;
            spawn.gameOver = true;

            ui.gameOver = true;
            ui.Timer.text = "Game Over";

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Object.Destroy(enemy);
            }
        }
        Object.Destroy(this.gameObject);
    }
}
