using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {
    public enum Direction { DOWN, UP, LEFT, RIGHT}
    enum STATE { LERPING_AWAY, LERPING_BACK,IDLE,NOTHING}

	public bool topSwitch, leftSwitch, rightSwitch, bottomSwitch;
    public float moveFactor = 3f;


    private bool needToPickASwitch;
    public string TOPSWITCH = "TopSwitch";
    public string BOTTOMSWITCH = "BottomSwitch";
    public string RIGHTSWITCH = "RightSwitch";
    public string LEFTSWITCH = "LeftSwitch";


    private float startTime;
    private float endTime = 1;
    private Direction currentDirection;
    private STATE state;
    private Vector2 startVector = Vector2.down;//default value
    private Vector2 endVector = Vector2.down;
    
	// Use this for initialization
	void Start () {
		topSwitch = false;
		leftSwitch = false;
		rightSwitch = false;
		bottomSwitch = false;
        needToPickASwitch = true;
        state = STATE.IDLE;
        startVector = transform.position;
        startTime = Time.time+1;
    }
	
	// Update is called once per frame
	void Update () {
        if (needToPickASwitch) PickASwitch();
        if (state == STATE.LERPING_BACK || state == STATE.LERPING_AWAY) Lerp(currentDirection);
        if (state == STATE.IDLE && Time.time - startTime > endTime)
        {
            BodySlam((Direction)Random.Range(0,3));
        }
    }

    void Lerp(Direction direction)
    {
        if (state == STATE.LERPING_AWAY)
        {
            if (Time.time - startTime < endTime)
            {
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
                //Debug.Log(Lerpval);
                transform.position = Vector2.Lerp(startVector, endVector, Lerpval);
            }
            else
            {
                startTime = Time.time;
                state = STATE.LERPING_BACK;
            }
        }
        else if (state == STATE.LERPING_BACK)
        {
            if (Time.time - startTime < endTime)
            {
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
                transform.position = Vector2.Lerp(endVector, startVector, Lerpval);
            }
            else
            {
                startTime = Time.time;
                state = STATE.IDLE;
            }
        }
    }

	void BodySlam(Direction direction){
        startTime = Time.time;
        Vector2 tempVector = Vector2.down;
        if (direction == Direction.DOWN)
        {
            currentDirection = Direction.DOWN;
            tempVector = Vector2.down * moveFactor;
        }
        if (direction == Direction.UP)
        {
            currentDirection = Direction.UP;
            tempVector = Vector2.up * moveFactor;
        }
        if (direction == Direction.LEFT)
        {
            currentDirection = Direction.LEFT;
            tempVector = Vector2.left * moveFactor;
        }
        if (direction == Direction.RIGHT)
        {
            currentDirection = Direction.RIGHT;
            tempVector = Vector2.right * moveFactor;
        }

        endVector = startVector + tempVector;
        state = STATE.LERPING_AWAY;

	}

	void Attack2() {

	}

	public void FlipRightSwitch(){
		rightSwitch = true;
        Switch currentswitch = transform.FindChild(RIGHTSWITCH).gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipTopSwitch(){
		topSwitch = true;
        Switch currentswitch = transform.FindChild(TOPSWITCH).gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipLeftSwitch(){
		leftSwitch = true;
        Switch currentswitch = transform.FindChild(LEFTSWITCH).gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipBottomSwitch(){
		bottomSwitch = true;
        Switch currentswitch = transform.FindChild(BOTTOMSWITCH).gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }

	void Death() {
		Destroy(this.gameObject);
	}

	void PickASwitch()
    {

        //check for death
        if (topSwitch && leftSwitch && rightSwitch && bottomSwitch)
        {
            Death();
        }
        //assign all unpressed switches to list
        ArrayList list = new ArrayList();
        if (!topSwitch) list.Add(transform.FindChild(TOPSWITCH));
        if (!bottomSwitch) list.Add(transform.FindChild(BOTTOMSWITCH));
        if (!rightSwitch) list.Add(transform.FindChild(RIGHTSWITCH));
        if (!leftSwitch) list.Add(transform.FindChild(LEFTSWITCH));
        //disable all the switches
        for (int forloopindex = 0; forloopindex < list.Count; forloopindex++)
        {
            Transform currenttransform = (Transform)list[forloopindex];
            Switch currentswitch =  currenttransform.gameObject.GetComponent<Switch>();
            //Debug.Log(currenttransform.gameObject);
            currentswitch.isEnabled = false;
        }
        //choose a switch at random 
        int size = list.Count;
        int index = Random.Range(0, size);

        //select and enable a switch
        Transform currentTransform = (Transform)list[index];
        Switch currentSwitch = currentTransform.gameObject.GetComponent<Switch>();
        Debug.Log(currentTransform.gameObject + " ENABLED");
        currentSwitch.isEnabled = true;

        needToPickASwitch = false;
    }
}
