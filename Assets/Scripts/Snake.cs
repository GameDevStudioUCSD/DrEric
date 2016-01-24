using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	int hitCount = 0;
	public int hitsBeforeRetreat = 0;
	private enum State {IDLE, AGGRAVATED, RETREAT};
	private State state;
	public int maxTimeAggravated = 0;
	int durationTimeAggravated;
	
	// Use this for initialization
	void Start () {
		state = State.IDLE;
	}
	
	void Update () {
		if (state == State.IDLE)
		{
		
		}
		else if (state == State.AGGRAVATED)
		{
			//durationTimeAggravated
		}
		else if (state == State.RETREAT)
		{
		
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		hitCount++;
		Debug.Log ("I GOT HIT " + hitCount + " times");
		state = State.AGGRAVATED;
		durationTimeAggravated = 0;
	}
}
