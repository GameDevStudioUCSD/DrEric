using UnityEngine;
using System.Collections;

public class ChangeGravity : MonoBehaviour {

    public Vector2 Gravity = (-9.8f) * Vector2.down;
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
            Physics2D.gravity = Gravity;
        }
    }
}
