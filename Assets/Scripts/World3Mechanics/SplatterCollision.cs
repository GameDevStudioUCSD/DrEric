using UnityEngine;
using System.Collections;

public class SplatterCollision : MonoBehaviour {


    public GameObject splatter;

    // Parent object to put all the splatters under
    private GameObject splatterParent;
    private string splatterParentName = "WallSplatters";

	// Use this for initialization
	void Start () {

        // Check if the parent object already exists
        splatterParent = GameObject.Find(splatterParentName);	
        if(splatterParent == null)
        {
            splatterParent = new GameObject(splatterParentName);
        }
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

            // Organize the splatter by moving it underneath a parent object
            newSplatter.transform.SetParent(splatterParent.transform);
        }
    }
}
