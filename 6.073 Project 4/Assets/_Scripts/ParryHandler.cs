using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHandler : MonoBehaviour {

    public Rigidbody2D rb2d;

    public Animator anim;

    private EntityManager em;

    private SpriteRenderer sprnd;

    public bool isParryFramesActive;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponentInChildren<Animator>();

        em = gameObject.GetComponent<EntityManager>();

        sprnd = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestParry()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsTag("Parry") && 
            !em.staggering &&
            !anim.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            anim.SetBool("isParrying", true);
        }
    }

    public void StartParryWindup ()
    {
        //Debug.Log("Parry Windup");
        rb2d.velocity = new Vector2(0, 0);
        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiParryWindupSpeed;
        }else if(gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiParryWindupSpeed;
        }
    }

    public void StartParry()
    {
        isParryFramesActive = true;
        Debug.Log("Parry Start");
        sprnd.color = Color.green;
        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiParrySpeed;
        }
        else if (gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiParrySpeed;
        }
    }

    public void StartParryCooldown()
    {
        isParryFramesActive = false;
        //Debug.Log("Parry Cooldown");
        sprnd.color = Color.white;
        if (gameObject.tag == "Enemy")
        {
            anim.speed = LevelConfigManager.doguraiParryCooldownSpeed;
        }
        else if (gameObject.tag == "Player")
        {
            anim.speed = LevelConfigManager.sameowraiParryCooldownSpeed;
        }
    }

    public void EndParry()
    {
        //Debug.Log("Parry End");
        anim.SetBool("isParrying", false);
        anim.speed = 1;
    }

    public void CancelParry()
    {
        //Debug.Log("Parry Cancel");
        isParryFramesActive = false;
        sprnd.color = Color.white;
        anim.SetBool("isParrying", false);
        anim.speed = 1;
    }
}
