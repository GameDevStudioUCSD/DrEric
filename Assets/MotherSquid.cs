using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherSquid : MonoBehaviour {

	public List<Eye> eyes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkIfOpen() {
		foreach (Eye e in eyes) {
			if (e.isOpen ())
				return;
		}
		Debug.Log ("ded");
	}
}
		