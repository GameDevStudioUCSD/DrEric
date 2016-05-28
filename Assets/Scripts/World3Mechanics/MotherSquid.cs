using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherSquid : MonoBehaviour {

	public List<Eye> eyes;
	public List<TentacleAttack> tentacles;
	public enum SquidState {
		Attacking, Recovering, Neutral, Dead
	};
	public SquidState state {get; set;}

	public SpriteRenderer bossBody;
	public SpriteRenderer deadBody;

	// Use this for initialization
	void Start () {
		state = SquidState.Neutral;
		bossBody.enabled = true;
		deadBody.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == SquidState.Neutral) {
			state = SquidState.Attacking;
			StartCoroutine (attackSequence (eyesOpen ()));
		}
		else if (state == SquidState.Dead) {
			bossBody.enabled = false;
			deadBody.enabled = true;
			for (int i = 0; i < eyes.Count; i++)
			{
				eyes[i].state = Eye.EyeState.gone;
			}
		}
	}

	public void getHit() {
		int eyeCount = eyesOpen ();
		if (eyeCount == eyes.Count) {
			state = SquidState.Dead;
		} else {
			state = SquidState.Attacking;
		}
	}

	public int eyesOpen() {
		int ret = 0;
		foreach (Eye e in eyes) {
			if (e.state == Eye.EyeState.damaged)
				ret++;
		}
		return ret;
	}

	IEnumerator armAttack() {
		int left = Random.Range(0,6);
		int right = Random.Range(0,6);

		while (right == left)
		{
			right = Random.Range(0,6);
		}

		TentacleAttack leftArm = tentacles[left];
		TentacleAttack rightArm = tentacles[right];

		leftArm.startAttack();
		rightArm.startAttack();

		yield return null;
	}

	IEnumerator rest() {
		state = SquidState.Recovering;
		for (int i = 0; i < eyes.Count; i++)
		{
			if (eyes[i].state == Eye.EyeState.closed)
				eyes[i].state = Eye.EyeState.open;
		}
		yield return new WaitForSeconds (6f);
		for (int i = 0; i < eyes.Count; i++)
		{
			if (eyes[i].state == Eye.EyeState.open)
				eyes[i].state = Eye.EyeState.closed;
		}
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
		