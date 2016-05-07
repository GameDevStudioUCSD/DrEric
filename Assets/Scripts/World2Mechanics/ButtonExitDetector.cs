using UnityEngine;
using System.Collections;

public class ButtonExitDetector : MonoBehaviour {

	public PushAndHoldButton button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D other) {
		button.endPress ();
	}
}
