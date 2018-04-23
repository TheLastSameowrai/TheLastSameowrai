using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
   
    private AttackHandler ah;
    private StanceManager sm;
    private EntityManager em;

    private AudioSource aud;

    public AudioClip lowSound, midSound, highSound;

    // Use this for initialization
    void Start () {
        ah = gameObject.GetComponent<AttackHandler>();
        sm = gameObject.GetComponent<StanceManager>();
        em = gameObject.GetComponent<EntityManager>();
        aud = gameObject.GetComponent<AudioSource>();
        aud.clip = midSound;
	}

	// Update is called at a fixed rate
	void Update () {
		float translation = Input.GetAxis ("Horizontal");

        em.MoveEntity(translation);

        bool attackFlag = false;
		if (!ah.anim.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				ah.RequestAttack ();
				attackFlag = true;
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				sm.StanceUp ();
				aud.clip = highSound;
			}else if (Input.GetKeyDown(KeyCode.S))
			{
				sm.StanceDown ();
				aud.clip = lowSound;
			}
			else
			{
				//aud.clip = midSound;
			}

			if (attackFlag) aud.Play();
		}
   
    }
}
