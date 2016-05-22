using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherSquid : MonoBehaviour {

	public List<Eye> eyes;
	public enum SquidState {
		Attacking, Recovering, Neutral
	};
	public SquidState state {get; set;}

	// Use this for initialization
	void Start () {
		state = SquidState.Neutral;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == SquidState.Neutral) {
			state = SquidState.Attacking;
			StartCoroutine (attackSequence (eyesOpen ()));
		}
	}

	public void getHit() {
		int eyeCount = eyesOpen ();
		if (eyeCount == 0) {
			Debug.Log ("ded");
		} else {
			state = SquidState.Attacking;
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

	IEnumerator armAttack() {
		Debug.Log ("AAAAARM");
		yield return null;
	}

	IEnumerator rest() {
		state = SquidState.Recovering;
		Debug.Log ("REEEEEST");
		yield return new WaitForSeconds (5f);
		state = SquidState.Neutral;
	}

	IEnumerator attackSequence(int count) {
		StartCoroutine(fireMissles (count));
		//Debug.Log ("WAIT " + (count / 2 + 2));
		yield return new WaitForSeconds (count / 2 + 2);
		StartCoroutine(fireMissles (count));
		//Debug.Log ("WAIT" + (count / 2 + 2));
		yield return new WaitForSeconds (count / 2 + 2);
		StartCoroutine(armAttack ());
		yield return new WaitForSeconds (count / 2 + 2);
		StartCoroutine (rest());
	}

	IEnumerator fireMissles(int count) {
		state = SquidState.Attacking;
		for (int i = 0; i < count; i++) {
			fireMissle ();
			yield return new WaitForSeconds (0.5f);
		}
	}

	void fireMissle() {}
}
		