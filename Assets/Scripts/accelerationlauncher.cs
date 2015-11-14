using UnityEngine;
using System.Collections;

public class accelerationlauncher : MonoBehaviour {
/** Filename: KeyScript.cs \n
 * Author: James Allen \n
 * Contributing authors: ^ \n 
 * Date Drafted: 11/9/2015 \n
 * Description: launcher things \n
 */

 public Vector2 acceleration;
 private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player" )//|| 
        	//other.gameObject.tag == "")
		{	
        	rigidBody = other.GetComponent<Rigidbody2D>();
        	if (rigidBody == null)
        	{
            	Debug.LogError("Null Reference Exception: No 2D Rigidbody found on " + this);
        	}
        	else {

        		rigidBody.AddForce(acceleration, ForceMode2D.Impulse);
        	}
            
        }
    }

}
