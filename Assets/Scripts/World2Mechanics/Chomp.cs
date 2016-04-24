using UnityEngine;
using System.Collections;

public class Chomp : MonoBehaviour {

    public enum State { OPEN, CLOSED, OPENING, CLOSING, TESTING }

    public State state = State.CLOSED;
    public Vector3 openRotation;
    public float chompSpeed = .5f;

    private float startTime = 0;


    Quaternion initialQuarternion, destinationQuarternion;
    public void OpenMouth()
    {
        if (state == State.CLOSED)
            Eat();
    }
    public void CloseMouth()
    {
        if (state == State.OPEN)
            Eat();
    }
	void Start () {
        initialQuarternion = transform.rotation;
        destinationQuarternion = Quaternion.Euler(openRotation);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.rotation.eulerAngles);
        switch( state )
        {
            case State.OPENING:
                Open();
                break;
            case State.CLOSING:
                Close();
                break;
            case State.OPEN:
                break;
            case State.CLOSED:
                break;
        }
	}

    public void Eat()
    {
        if (state == State.OPEN)
            state = State.CLOSING;
        else if (state == State.CLOSED)
            state = State.OPENING;
        startTime = Time.time;
    }

    void Open()
    {
        float lerpVal = (Time.time - startTime) / chompSpeed;
        transform.localRotation = Quaternion.Slerp(initialQuarternion, destinationQuarternion, lerpVal);
        //transform.Rotate(Time.deltaTime * Vector3.right);
        if (Time.time - startTime > chompSpeed)
            state = State.OPEN;
    }
    void Close()
    {
        float lerpVal = (Time.time - startTime) / chompSpeed;
        transform.localRotation = Quaternion.Slerp(destinationQuarternion, initialQuarternion, lerpVal);
        //transform.Rotate(Time.deltaTime * Vector3.left);
        if (Time.time - startTime > chompSpeed)
            state = State.CLOSED;
    }
}
