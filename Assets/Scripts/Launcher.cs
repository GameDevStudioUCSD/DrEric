using UnityEngine;
using System.Collections;
/**
 * Filename: Launcher.cs \n
 * Author: Daniel Griffiths \n
 * Contributing Authors: Michael Gonzalez \n
 * Date Drafted: October 26, 2015 \n
 * Description: This script is used by a launcher object. The launcher rotates
 *              passively. When DrEric touches the launcher, he becomes attached
 *              to it, and when the mouse is clicked, he is launched in the
 *              direction the launcher currently faces.
 */
public class Launcher : MonoBehaviour {
	public float rotationSpeed;
	public float launchSpeed;

	private Vector2 direction;
	private bool gotDrEric;
	private GameObject drEric = null;
	private Quaternion storedOrientation;
    

    /** unused */
	void Start () {
      
    }

	
	/** Rotates the launcher. When DrEric is held by the launcher and the mouse
	 *  is released, launches him. */
	void Update () {
		transform.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime);
		/** Releases and launches DrEric using his Fling() method */
		if (gotDrEric && Input.GetMouseButtonUp (0)) {
			//Releases DrEric from the launcher
			gotDrEric = false;
			drEric.transform.parent = null;
			drEric.transform.localScale = Vector3.one; //TODO corrects for bug after launch; see OnTriggerEnter2D
			//Restores independent movement
			drEric.GetComponent<Rigidbody2D>().gravityScale = 1;
			drEric.transform.rotation = storedOrientation;
			drEric.GetComponent<FlingObject>().Fling (CalculateLaunch ());
			//Clears stored reference
			drEric = null;
		}
	}

	/** Grabs DrEric on contact */
	void OnTriggerEnter2D(Collider2D other) {
		if (!gotDrEric && other.CompareTag("Player")) {
			Quaternion quar = other.transform.rotation;
			storedOrientation = new Quaternion( quar.x, quar.y, quar.z, quar.w);
			gotDrEric = true;
			drEric = other.gameObject;
			//Takes DrEric into the launcher
			drEric.transform.parent = transform;
			drEric.transform.position = transform.position; //TODO creates bug where DrEric's size changes seemingly randomly while in cannon
			//Stops all of DrEric's independent movements
			drEric.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			drEric.GetComponent<Rigidbody2D>().gravityScale = 0;
			drEric.GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}

	/** Determines the appropriate velocity at which to launch DrEric */
	private Vector2 CalculateLaunch() {
		float x = launchSpeed * Mathf.Cos (transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		float y = launchSpeed * Mathf.Sin (transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		return new Vector2 (x, y);
	}
}
