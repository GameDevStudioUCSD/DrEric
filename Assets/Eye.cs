using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

	public MotherSquid squid;
	bool open;

	// Use this for initialization
	void Start () {
		open = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			open = false;
			squid.checkIfOpen ();
		}
	}

	public bool isOpen() {
		return open;
	}
}
