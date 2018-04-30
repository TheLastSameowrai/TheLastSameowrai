﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StanceManager : MonoBehaviour
{

    public enum Stance { LOW, MID, HIGH };

    public Stance currentStance;

    public SpriteRenderer sprend; // This is used to change the color of sprite for demo - will change animation set later

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        sprend = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        currentStance = Stance.MID;
        anim.SetLayerWeight(2, 0);
        anim.SetLayerWeight(1, 1);
        anim.SetLayerWeight(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStance)
        {
            case Stance.HIGH:
                sprend.color = Color.red;
                break;
            case Stance.MID:
                sprend.color = Color.blue;
                break;
            case Stance.LOW:
                sprend.color = Color.green;
                break;
            default:
                break;
        }
    }

    public void ChangeStance(Stance s)
    {
        currentStance = s;
    }

    public void StanceUp()
    {
        switch (currentStance)
        {
            case Stance.HIGH:
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 0);
                break;
            case Stance.MID:
                currentStance = Stance.HIGH;
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 0);
                break;
            case Stance.LOW:
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(0, 0);
                currentStance = Stance.MID;
                break;
            default:
                break;
        }
    }

    public void StanceDown()
    {
        switch (currentStance)
        {
            case Stance.HIGH:
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(0, 0);
                currentStance = Stance.MID;
                break;
            case Stance.MID:
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 1);
                currentStance = Stance.LOW;
                break;
            case Stance.LOW:
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 1);
                break;
            default:
                break;
        }
    }
}
