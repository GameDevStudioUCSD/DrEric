using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour {

    enum State { Resting, Chasing }
    public float restRate = 1.5f;
    public float restDistance = 5;
    PIDController pid;
    float lastAttack = 0;
    State state = State.Chasing;

	void Start () {
        pid = GetComponent<PIDController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time - lastAttack > restRate )
        {
            switch(state)
            {
                case State.Resting:
                    state = State.Chasing;
                    pid.trackingType = PIDController.TrackingType.Transform;
                    break;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name != Names.DRERIC)
            return;
        Vector3 otherPos = c.gameObject.GetComponent<Transform>().position;
        Vector3 restPos = restDistance * (transform.position - otherPos).normalized;
        pid.destinationVector = transform.position + restPos;
        pid.trackingType = PIDController.TrackingType.Vector;
        state = State.Resting;
        lastAttack = Time.time;
    }
}
