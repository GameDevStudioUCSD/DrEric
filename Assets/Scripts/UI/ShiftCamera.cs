using UnityEngine;
using System.Collections;

public class ShiftCamera : MonoBehaviour {

	public float offset = 100;
	

	public void ShiftRight()
	{
		//Debug.Log ("shifting right");
		//Camera.main.transform.Translate (offset, 0, 0);
		gameObject.GetComponent<Animation>().Play();
	}

	public void ShiftLeft()
	{
		Debug.Log ("shifting left");
		Camera.main.transform.Translate (-offset, 0, 0);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
