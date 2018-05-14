using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
   
    private AttackHandler ah;
    private ParryHandler ph;
    private StanceManager sm;
    private EntityManager em;

    private AudioSource aud;

    public AudioClip lowSound, midSound, highSound, blockSound;

    // Use this for initialization
    void Start () {
        ah = gameObject.GetComponent<AttackHandler>();
        ph = gameObject.GetComponent<ParryHandler>();
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
        bool parryFlag = false;
		if (!ah.anim.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && 
            ! LevelConfigManager.Paused &&
            !ah.anim.GetCurrentAnimatorStateInfo(0).IsTag("Parry")) {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ph.RequestParry();
                aud.clip = blockSound;
                parryFlag = true;
            }
			if (Input.GetKeyDown (KeyCode.Space)) {
				ah.RequestAttack();
                switch(sm.currentStance) {
                    case StanceManager.Stance.LOW:  aud.clip = lowSound;    break;
                    case StanceManager.Stance.MID:  aud.clip = midSound;    break;
                    case StanceManager.Stance.HIGH: aud.clip = highSound;   break;
                }
				attackFlag = true;
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				sm.StanceUp();
			} else if (Input.GetKeyDown(KeyCode.S))
			{
                sm.StanceDown();
            }

			if (attackFlag || parryFlag) aud.Play();
		}

		if (transform.position.x - Camera.main.transform.position.x > 2) {
			float difference = transform.position.x - Camera.main.transform.position.x - 2;
			Camera.main.transform.Translate (difference, 0, 0);

		} else if (transform.position.x - Camera.main.transform.position.x < -2) {
			float difference = transform.position.x - Camera.main.transform.position.x + 2;
			Camera.main.transform.Translate (difference, 0, 0);
		}
		if (Camera.main.transform.position.x <= -1.1) {
			Camera.main.transform.SetPositionAndRotation (new Vector3 (-1.1f, 0, -10), Quaternion.identity);
		}
		if (Camera.main.transform.position.x >= 1.1) {
			Camera.main.transform.SetPositionAndRotation(new Vector3(1.1f, 0, -10), Quaternion.identity);
		}

		GameObject.FindGameObjectWithTag ("UIScript").transform.SetPositionAndRotation (Camera.main.transform.position, Camera.main.transform.rotation);

   
    }
}
