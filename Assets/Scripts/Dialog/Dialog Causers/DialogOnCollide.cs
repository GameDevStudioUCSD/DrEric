using UnityEngine;
using System.Collections;

public class DialogOnCollide : MonoBehaviour {

	public HasDialog dBox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			dBox.showDialog ();
		}
	}
}
