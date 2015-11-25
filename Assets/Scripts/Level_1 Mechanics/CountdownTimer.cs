using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {

    public float timerVal = 24;
    public int tensPlace;
    public int onesPlace;
    public int rand1;
    public int rand2;
    public enum State { START, IDLE, COUNTING, FINISHED }
    public State state = State.START;
    private float endTime = 0;
	void Start () {
	
	}
	
	void Update () {
        //GetComponent<Animator>().SetInteger("Digit", Random.Range(0, 3));
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
        StartCounting();
    }
    void Count()
    {
        float currTime = endTime - Time.time;
        if(currTime <= 0 )
        {
            state = State.FINISHED;
            return;
        }
        CalculateTime(currTime);
    }
    void Finish()
    {
        tensPlace = onesPlace = rand1 = rand2 = 0;
    }
    void CalculateTime( float currTime )
    {
        tensPlace = (int)(currTime / 10);
        onesPlace = (int)(currTime % 10);
        rand1 = Random.Range(0, 9);
        rand2 = Random.Range(0, 9);
    }
}
