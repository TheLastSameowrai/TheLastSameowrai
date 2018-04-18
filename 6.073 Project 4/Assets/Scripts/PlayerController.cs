﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb2d;

    public AttackHandler ah;

    public StanceManager sm;

    // Use this for initialization
    void Start () {
        ah = gameObject.GetComponent<AttackHandler>();
        sm = gameObject.GetComponent<StanceManager>();
	}
	
	// Update is called at a fixed rate
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ah.RequestAttack();
        }

        // Check for stance change input

        if (Input.GetKey(KeyCode.W))
        {
            sm.ChangeStance(StanceManager.Stance.HIGH);
        }else if (Input.GetKey(KeyCode.S))
        {
            sm.ChangeStance(StanceManager.Stance.LOW);
        }
        else
        {
            sm.ChangeStance(StanceManager.Stance.MID);
        }
	}
}
