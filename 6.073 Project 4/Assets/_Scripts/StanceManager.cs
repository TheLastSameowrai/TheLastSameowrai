using System.Collections;
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
        anim = gameObject.GetComponentInChildren<Animator>();
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
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 0);
                break;
            case Stance.MID:
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(0, 0);
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
				LevelConfigManager.dataManager.stanceChange ("HIGH", Time.time);
                currentStance = Stance.HIGH;
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(0, 0);
                break;
            case Stance.LOW:
				LevelConfigManager.dataManager.stanceChange ("MID", Time.time);
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
				LevelConfigManager.dataManager.stanceChange ("MID", Time.time);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(0, 0);
                currentStance = Stance.MID;
                break;
            case Stance.MID:
				LevelConfigManager.dataManager.stanceChange ("LOW", Time.time);
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
