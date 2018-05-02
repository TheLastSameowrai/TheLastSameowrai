using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoEnemyController : MonoBehaviour
{

    public AttackHandler ah;

    public StanceManager sm;

    public EntityManager em;

    public GameObject target;

    private float distFromPlayer = 2f;

    private float translation = 0;

    private float MaxDistance = 5f;



    // Use this for initialization
    void Start()
    {
        ah = gameObject.GetComponent<AttackHandler>();
        sm = gameObject.GetComponent<StanceManager>();
        em = gameObject.GetComponent<EntityManager>();
		RandStance ();
        //InvokeRepeating("RandStance", 0, 3);
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        Rigidbody2D playerBody = target.GetComponent<Rigidbody2D>();
        Rigidbody2D enemyBody = em.GetComponent<Rigidbody2D>();
        Vector2 playerVel = playerBody.velocity;

        Vector2 moveDirection = playerBody.position - enemyBody.position;
        //print("MoveDir");
        //print(moveDirection);
        //print(Vector2.Distance(playerBody.position, enemyBody.position));
        //var dist = Vector2.Distance(playerBody.position, enemyBody.position);
        //if( moveDirection.x > distFromPlayer || moveDirection.x < -distFromPlayer) 
        //{
            
        //    print(translation);
        //    em.MoveEntity(translation);
        //} else
        
        //{
        //    ah.RequestAttack();
        //}
        //Vector2 moveDirectionNorm = moveDirection.normalized;
        //print("MoveDirNorm");
        //print(moveDirectionNorm);
        //float translation = moveDirectionNorm.x;

        if ((moveDirection.x > -distFromPlayer && moveDirection.x < 0 && em.looking == -1) || (moveDirection.x < distFromPlayer && moveDirection.x > 0 && em.looking == 1))
        {
            ah.RequestAttack();
        }
        else
        {
            translation = moveDirection.x / MaxDistance;
            em.MoveEntity(translation);
        }

    }

    void RandStance()
    {
        Array vals = Enum.GetValues(typeof(StanceManager.Stance));
        System.Random random = new System.Random();
        StanceManager.Stance randStance = (StanceManager.Stance)vals.GetValue(random.Next(vals.Length));

        sm.ChangeStance(randStance);
    }
}
