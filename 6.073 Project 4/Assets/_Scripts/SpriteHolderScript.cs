using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolderScript : MonoBehaviour {

	public AttackHandler ah;

    public ParryHandler ph;

    public EntityManager em;

	// Use this for initialization
	void Start () {
		ah = gameObject.GetComponentInParent<AttackHandler> ();
        ph = gameObject.GetComponentInParent<ParryHandler>();
        em = gameObject.GetComponentInParent<EntityManager>();
        gameObject.GetComponent<Animator> ().feetPivotActive = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Animator> ().feetPivotActive = 1f;
	}

	void StartWindup() {
		ah.StartWindup ();
	}

	void StartAttack() {
		ah.StartAttack ();
	}

	void StartCoolDown() {
		ah.StartCoolDown ();
	}

	void EndAttack() {
		ah.EndAttack ();
	}

    void StartParryWindup()
    {
        ph.StartParryWindup();
    }

    void StartParry()
    {
        ph.StartParry();
    }

    void StartParryCooldown()
    {
        ph.StartParryCooldown();
    }

    void EndParry()
    {
        ph.EndParry();
    }

    void EndStagger()
    {
        em.EndStagger();
    }
}
