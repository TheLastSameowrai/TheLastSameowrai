using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {

    public Rigidbody2D rb2d;

    public Animator anim;

    private bool isAttacking;
    
	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponent<Animator>();

        isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("INPUT SPACE");

            if (!isAttacking)
            {
                Debug.Log("NOT IS ATTACKING");
                anim.SetBool("isAttacking", true);
                isAttacking = true;
            }
        }

	}

    void StartWindup()
    {
        Debug.Log("StartWindup");
    }

    void StartAttack()
    {
        Debug.Log("StartAttack");
        rb2d.velocity = new Vector2(2, 0);
    }

    void EndAttack()
    {
        Debug.Log("EndAttack");
        rb2d.velocity = new Vector2(0, 0);

        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
