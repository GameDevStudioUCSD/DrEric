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
        if(other.gameObject.tag == "Player")
        {
            // Where the collision happened
            ContactPoint2D contactPoint = other.contacts[0];
            Vector2 collisionPoint = contactPoint.point;

            GameObject newSplatter = (GameObject)Instantiate(splatter, new Vector3(collisionPoint.x, collisionPoint.y, 0.0f), Quaternion.identity);
            float angle = Random.RandomRange(0.0f, 359.0f);
            newSplatter.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), angle);
        }
    }
}
