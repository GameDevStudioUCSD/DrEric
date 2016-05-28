using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

	public MotherSquid squid;
	bool open;
	public 

	// Use this for initialization
	void Start () {
		open = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && squid.state == MotherSquid.SquidState.Recovering) {
			open = false;
			squid.getHit ();
			this.gameObject.SetActive(false);
		}
	}

	public bool isOpen() {
		return open;
	}
}
