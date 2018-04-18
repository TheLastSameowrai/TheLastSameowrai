using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemoEnemyController : MonoBehaviour
{

    public Rigidbody2D rb2d;

    public AttackHandler ah;

    public StanceManager sm;

    public EntityManager em;

    public EntityManager player;



    // Use this for initialization
    void Start()
    {
        ah = gameObject.GetComponent<AttackHandler>();
        sm = gameObject.GetComponent<StanceManager>();
        em = gameObject.GetComponent<EntityManager>();

        InvokeRepeating("RandStance", 0, 3);
    }

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        
    }

    void RandStance()
    {
        Array vals = Enum.GetValues(typeof(StanceManager.Stance));
        System.Random random = new System.Random();
        StanceManager.Stance randStance = (StanceManager.Stance)vals.GetValue(random.Next(vals.Length));

        sm.ChangeStance(randStance);
    }
}
