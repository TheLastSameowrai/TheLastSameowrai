using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {

    public Rigidbody2D rb2d;

    public Animator anim;

    private EntityManager em;

    public float windUpSpeedScale = 1;

    public float attackSpeedScale = 1;
    
	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponent<Animator>();

        em = gameObject.GetComponent<EntityManager>();

	}
	
	// Update is called once per frame
	void Update () {


	}

    public void RequestAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetBool("isAttacking", true);
        }
    }

    // Called when attack telegraph is called (at the start of the attack)
    void StartWindup()
    {
        Debug.Log("Entity start windup");
        Debug.Log(gameObject);
        rb2d.velocity = new Vector2(0, 0);
        anim.speed = windUpSpeedScale;
    }
       
    // Called when the attack itself starts
    void StartAttack()
    {
        Debug.Log("Entity start attack");
        Debug.Log(gameObject);
        int dir = em.looking;
        anim.speed = attackSpeedScale;
    }

    // Called when the attack is done
    void EndAttack()
    {
        Debug.Log("Entity end attack");
        Debug.Log(gameObject);

        rb2d.velocity = new Vector2(0, 0);

        anim.SetBool("isAttacking", false);
        anim.speed = 1;
        
    }
}
