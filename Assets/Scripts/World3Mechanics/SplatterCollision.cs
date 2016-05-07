using UnityEngine;
using System.Collections;

public class SplatterCollision : MonoBehaviour {


    public GameObject splatter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        // Where the collision happened
        ContactPoint2D contactPoint = other.contacts[0];

        // TODO: Instantiate splatter
    }
}
