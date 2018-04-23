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

    // Use this for initialization
    void Start () {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        hurtbox = gameObject.transform.Find("HitDetector").GetComponent<BoxCollider2D>();
        sm = gameObject.GetComponent<StanceManager>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        sprend = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();

        ah = gameObject.GetComponent<AttackHandler>();
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

        sprend.flipX = looking > 0 ? false : true;
        
	}

    public void MoveEntity(float translation)
    {
        if (ah.isAttacking)
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
        // TODO
        print("DESTROYING");
        gameObject.SetActive(false);
    }
}
