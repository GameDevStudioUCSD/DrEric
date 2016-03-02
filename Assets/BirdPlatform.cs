using UnityEngine;
using System.Collections;

public class BirdPlatform : MonoBehaviour {
	Vector3 birdPosition;
	//The rate at which the platform will fall
	public float fallRate;
	//When the platform will reset
	public float resetDelay;

	private bool isFalling;



	// Use this for initialization
	void Start () {
		//save the initial position of the platform
		birdPosition = transform.position;
		//starts off not falling;
		isFalling = false;
	}
	void ResetPosition(){
		//stops falling
		Debug.Log("pos reset");
		isFalling = false;
		//resets postion
		transform.position = birdPosition;
	}
	void OnCollisionEnter2D(Collision2D coll){
		//only on player collision once per cycle
		if (coll.gameObject.tag =="Player" && !(isFalling)){
			//let the platform start falling
			isFalling = true;
			Debug.Log ("hi");
			//will reset after a certain time
			Invoke("ResetPosition", resetDelay);
		}
	}
	// Update is called once per frame
	void Update () {
		if(isFalling) {
			transform.position += Vector3.down * fallRate;	
		}
	}
}
