using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolderScript : MonoBehaviour {

	public AttackHandler ah;

	// Use this for initialization
	void Start () {
		ah = gameObject.GetComponentInParent<AttackHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
