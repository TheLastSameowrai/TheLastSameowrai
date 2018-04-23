using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {

    public Rigidbody2D rb2d;

    public Animator anim;

    public bool isAttacking;

    private EntityManager em;
    
	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponent<Animator>();

        em = gameObject.GetComponent<EntityManager>();


        isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {


	}

    public void RequestAttack()
    {
        if (!isAttacking)
        {
            anim.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    // Called when attack telegraph is called (at the start of the attack)
    void StartWindup()
    {
        
    }
       
    // Called when the attack itself starts
    void StartAttack()
    {
        int dir = em.looking;
        rb2d.velocity = new Vector2(dir*2, 0);
    }

    // Called when the attack is done
    void EndAttack()
    {
        
        rb2d.velocity = new Vector2(0, 0);

        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
