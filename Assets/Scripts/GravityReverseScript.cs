using UnityEngine;
using System.Collections;

public class GravityReverseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Trigger: ");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger: ");
            Physics2D.gravity = new Vector2(0,9);
        }
    }
}
