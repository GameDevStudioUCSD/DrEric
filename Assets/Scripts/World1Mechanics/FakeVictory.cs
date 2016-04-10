using UnityEngine;
using System.Collections;

public class FakeVictory : MonoBehaviour {


    public GameObject victim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            victim = other.gameObject;
            diedie();
        }
    }

    void diedie()
    {
        GameObject drEric = victim.gameObject;
        Transform drEricTransform = drEric.transform;
        Rigidbody2D drEricRigidBody = drEric.GetComponent<Rigidbody2D>();
        ConstantForce2D force = drEric.AddComponent<ConstantForce2D>();
        drEric.GetComponent<BallController>().enabled = false;
        drEricTransform.parent = transform;
        drEricTransform.localPosition = Vector2.zero;
        drEricRigidBody.velocity = Vector2.zero;
        drEricRigidBody.gravityScale = 0;
        force.torque = 100;
    }
}
