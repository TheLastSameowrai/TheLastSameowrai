﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StanceManager : MonoBehaviour {

    public enum Stance {LOW, MID, HIGH};

    public Stance currentStance;

    public SpriteRenderer sprend; // This is used to change the color of sprite for demo - will change animation set later

	// Use this for initialization
	void Start () {
        sprend = gameObject.GetComponent<SpriteRenderer>();

        currentStance = Stance.MID;
	}
	
	// Update is called once per frame
	void Update () {
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

	public void StanceUp(){
		switch (currentStance)
		{
		case Stance.HIGH:
			break;
		case Stance.MID:
			currentStance = Stance.HIGH;
			break;
		case Stance.LOW:
			currentStance = Stance.MID;
			break;
		default:
			break;
		}
	}

	public void StanceDown(){
		switch (currentStance)
		{
		case Stance.HIGH:
			currentStance = Stance.MID;
			break;
		case Stance.MID:
			currentStance = Stance.LOW;
			break;
		case Stance.LOW:
			break;
		default:
			break;
		}
	}
}
