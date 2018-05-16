using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {

    public Rigidbody2D rb2d;

    public Animator anim;

    private EntityManager em;
    
	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponentInChildren<Animator>();

        em = gameObject.GetComponent<EntityManager>();

	}
	
	// Update is called once per frame
	void Update () {


	}

    public void RequestAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") &&
           !anim.GetCurrentAnimatorStateInfo(0).IsTag("Parry") && 
           !em.staggering &&
           !anim.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            anim.SetBool("isAttacking", true);
        }
    }

    // Called when attack telegraph is called (at the start of the attack)
    public void StartWindup()
    {
        //Debug.Log("Entity start windup");
       
        rb2d.velocity = new Vector2(0, 0);

        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiWindupSpeed;
        }
        else if(gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiWindupSpeed;
        }
        
    }
       
    // Called when the attack itself starts
    public void StartAttack()
    {
        //Debug.Log("Entity start attack");
        
        
        int dir = em.looking;
        
        if (gameObject.tag == "Enemy")
        {
			anim.speed = em.attackSpeed;
        }
        else if (gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiAttackSpeed;
        }
    }

    public void StartCoolDown()
    {
        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiCoolDownSpeed;
        }
        else if (gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiCoolDownSpeed;
        }
    }

    // Called when the attack is done
    public void EndAttack()
    {
        //Debug.Log("Entity end attack");
        

        //rb2d.velocity = new Vector2(0, 0);

        anim.SetBool("isAttacking", false);
        anim.speed = 1;
        
    }

    public void CancelAttack()
    {
        //Debug.Log("Cancel Attack");
        anim.SetBool("isAttacking", false);
        anim.speed = 1;
    }
}
