using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	int hitCount = 0;
	public int hitsBeforeRetreat = 0;
	private enum State {IDLE, AGGRAVATED, CALMING, RETREAT};
	private State state;
	public float maxTimeAggravated = 0f;
	const float FRAMERATE = 0.1f;
	
	int aggravateFrameCounter = 0;
	
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
			state = State.CALMING;
			Invoke ("AnimateAggravated", FRAMERATE);
			Invoke ("RetunToIdle", maxTimeAggravated);
		}
		else if (state == State.CALMING)
		{
		}
		else if (state == State.RETREAT)
		{
			
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (state == State.IDLE) {
			hitCount++;
			Debug.Log ("I GOT HIT " + hitCount + " times");
			if (hitCount < hitsBeforeRetreat)
				state = State.AGGRAVATED;
			else
				state = State.RETREAT;
		}
	}
	
	void ReturnToIdle()
	{
		Debug.Log ("back to idle");
		state = State.IDLE;
	}
	
	void AnimateIdle()
	{
	
	}
	
	void AnimateAggravated()
	{
		switch (aggravateFrameCounter)
		{
			case 0:
				Debug.Log ("#");
				aggravateFrameCounter = 1;
				break;
			case 1: 
			Debug.Log ("##");
				aggravateFrameCounter = 2;
				break;
			case 2:
			Debug.Log ("###");
				aggravateFrameCounter = 3;
				break;
			case 3:
			Debug.Log ("####");
				aggravateFrameCounter = 4;
				break;
			case 4:
			Debug.Log ("#####");
				aggravateFrameCounter = 5;
				break;
			case 5:
			Debug.Log ("####");
				aggravateFrameCounter = 6;
				break;
			case 6:
			Debug.Log ("###");
				aggravateFrameCounter = 7;
				break;
			case 7:
			Debug.Log ("##");
				aggravateFrameCounter = 0;
				break;
		}
	}
	
	void AnimateRetreat()
	{
	
	}
}