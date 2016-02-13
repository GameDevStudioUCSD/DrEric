using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CountdownTimer : MonoBehaviour {

    public float timerVal = 24;
    public GameObject tensObj;
    public GameObject onesObj;
    public GameObject tenthObj;
    public GameObject hundrethObj;
    public int tensPlace;
    public int onesPlace;
    public int rand1;
    public int rand2;
    public enum State { START, IDLE, COUNTING, FINISHING, FINISHED }
    public State state = State.START;
    public UnityEvent fireOnFinish;
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
            case State.FINISHING:
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
            state = State.FINISHING;
            return;
        }
        CalculateTime(currTime);
        SetDigits();
    }
    void Finish()
    {
        tensPlace = onesPlace = rand1 = rand2 = 0;
        SetDigits();
        fireOnFinish.Invoke();
        state = State.FINISHED;
    }
    void CalculateTime( float currTime )
    {
        tensPlace = (int)(currTime / 10);
        onesPlace = (int)(currTime % 10);
        //rand1 = (int)((currTime % 100)*10);
        //rand2 = (int)((currTime % 1000)*100);
        rand1 = Random.Range(0, 9);
        rand2 = Random.Range(0, 9);
    }
    void SetDigits()
    {
        tensObj.GetComponent<Animator>().SetInteger("Digit", tensPlace);
        onesObj.GetComponent<Animator>().SetInteger("Digit", onesPlace);
        tenthObj.GetComponent<Animator>().SetInteger("Digit", rand1);
        hundrethObj.GetComponent<Animator>().SetInteger("Digit", rand2);
    }
}
