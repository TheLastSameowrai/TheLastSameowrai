using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolderScript : MonoBehaviour {

	public AttackHandler ah;

	// Use this for initialization
	void Start () {
		ah = gameObject.GetComponentInParent<AttackHandler> ();
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
}
