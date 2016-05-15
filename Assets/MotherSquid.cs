using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherSquid : MonoBehaviour {

	public List<Eye> eyes;
	public enum SquidState {
		Neutral, Angry
	};
	public SquidState state {get; set;}

	// Use this for initialization
	void Start () {
		state = SquidState.Neutral;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void getHit() {
		int eyeCount = eyesOpen ();
		if (eyeCount == 0) {
			Debug.Log ("ded");
		} else {
			state = SquidState.Angry;
			StartCoroutine(fireMissles (eyeCount));
		}
	}

	public int eyesOpen() {
		int ret = 0;
		foreach (Eye e in eyes) {
			if (e.isOpen ())
				ret++;
		}
		return ret;
	}

	IEnumerator fireMissles(int count) {
		for (int i = 0; i < count; i++) {
			fireMissle ();
			Debug.Log ("MISSILE");
			yield return new WaitForSeconds (0.5f);
		}
		state = SquidState.Neutral;
	}

	void fireMissle() {}
}
		