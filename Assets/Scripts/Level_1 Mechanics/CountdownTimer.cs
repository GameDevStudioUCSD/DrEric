using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {

    public float timerVal = 24;
    enum State { IDLE, COUNTING, FINISHED }
    private State state = State.IDLE;
    private float endTime = 0;
	void Start () {
	
	}
	
	void Update () {
        GetComponent<Animator>().SetInteger("Digit", Random.Range(0, 3));
        switch( state )
        {
            case State.IDLE:
                Idle();
                break;
            case State.COUNTING:
                Count();
                break;
            case State.FINISHED:
                Finish();
                break;
        }
	}
    public void StartCounting()
    {
        endTime = Time.time + timerVal;
        state = State.COUNTING;
    }
    void Idle()
    {
    }
    void Count()
    {
    }
    void Finish()
    {
    }
}
