using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    public BoxCollider2D hitbox;

    public BoxCollider2D hurtbox;

    public StanceManager sm;

	// Use this for initialization
	void Start () {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        hurtbox = gameObject.transform.Find("HitDetector").GetComponent<BoxCollider2D>();
        sm = gameObject.GetComponent<StanceManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HurtBox")
        {
            StanceManager other_sm = collision.gameObject.GetComponentInParent<StanceManager>();
            if(sm.currentStance != other_sm.currentStance)
            {
                DestroyEntity();
            }
            
        }
    }

    public void DestroyEntity()
    {
        // TODO
        gameObject.SetActive(false);
    }
}
