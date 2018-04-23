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



    // Use this for initialization
    void Start()
    {
        ah = gameObject.GetComponent<AttackHandler>();
        sm = gameObject.GetComponent<StanceManager>();
        em = gameObject.GetComponent<EntityManager>();
       
        //InvokeRepeating("RandStance", 0, 3);
        RandStance();
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        Rigidbody2D playerBody = target.GetComponent<Rigidbody2D>();
        Rigidbody2D enemyBody = em.GetComponent<Rigidbody2D>();
        Vector2 moveDirection = playerBody.position - enemyBody.position;
        print("MoveDir");
        print(moveDirection);
        Vector2 moveDirectionNorm = moveDirection.normalized;
        print("MoveDirNorm");
        print(moveDirectionNorm);
        float translation = moveDirectionNorm.x;

        
        if(Math.Abs(moveDirection.x) < 4.0) {
            ah.RequestAttack();
        } else
        {
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
