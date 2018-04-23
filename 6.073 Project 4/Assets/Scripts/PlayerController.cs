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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ah.RequestAttack();
            attackFlag = true;
        }

        // Check for stance change input

        if (Input.GetKey(KeyCode.W))
        {
            sm.ChangeStance(StanceManager.Stance.HIGH);
            aud.clip = highSound;
        }else if (Input.GetKey(KeyCode.S))
        {
            sm.ChangeStance(StanceManager.Stance.LOW);
            aud.clip = lowSound;
        }
        else
        {
            sm.ChangeStance(StanceManager.Stance.MID);
            aud.clip = midSound;
        }

        if (attackFlag) aud.Play();
    }
}
