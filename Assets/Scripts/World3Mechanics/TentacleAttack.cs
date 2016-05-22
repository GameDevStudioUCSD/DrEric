using UnityEngine;
using System.Collections;

public class TentacleAttack : MonoBehaviour
{
	private float startTime = 0;

	public enum State { ATTACKING, RETREATING, LOOKING, STALLING, STOP };
	public PlayerHolder drEric;
	public RespawnController respawner;
	public Collider2D tentacle;
	public Vector3 startVector = new Vector2(0, 0);
	public Vector3 endVector = new Vector2(0, 0);
	public State state;
	public float movementTime = 2;
	public float waitTime = 1;

	public State startState = State.STOP;

	void Start()
    {
        startTime = Time.time;
        state = startState;
        startVector = this.transform.position;
    }

	void Update () {
        switch(state){
            case State.LOOKING:
                Looking();
                break;
            case State.ATTACKING:
                Lerping(false);
                break;
            case State.STALLING:
            	Stalling();
            	break;
            case State.RETREATING:
            	Lerping(true);
            	break;
        }
	}

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != null && other.tag == "Player")
        {
			respawner.kill();
        }
    }

	void Looking()
    {
    	endVector = drEric.transform.position;
		Debug.Log(this.transform.eulerAngles);
		//this.transform.eulerAngles = new Vector3(0,0,(90 * Mathf.Atan2(endVector.x - startVector.x, endVector.y - startVector.y) / Mathf.PI));

		// Opposite of the direction tentacle is moving
		Vector3 oppDirection = startVector - endVector;

		float angle = Mathf.Atan2(oppDirection.y, oppDirection.x) * Mathf.Rad2Deg;

		this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		Debug.Log(this.transform.eulerAngles);
        if(Time.time - startTime > waitTime)
        {
            startTime = Time.time;
            state = State.ATTACKING;
        }
    }

	void Stalling()
    {
        if(Time.time - startTime > waitTime)
        {
            startTime = Time.time;
            state = State.RETREATING;
        }
    }

	void Lerping(bool retreating)
    {
    	Vector3 beginCoord = startVector;
    	Vector3 endCoord = endVector;
    	if (retreating)
    	{
			beginCoord = endVector;
    		endCoord = startVector;
    	}
        if(Time.time - startTime > movementTime)
        {
        	if (state == State.ATTACKING)
        	{
        		startTime = Time.time;
            	state = State.STALLING;
            }
            if (state == State.RETREATING)
            {
				startTime = Time.time;
            	state = State.STOP;
            }
        }
        else
        {
            float lerpVal = (Time.time - startTime) / movementTime;
            transform.position = Vector3.Lerp(beginCoord, endCoord, lerpVal);
        }
    }
}