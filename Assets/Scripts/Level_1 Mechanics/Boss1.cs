using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {
    public enum Direction { DOWN, UP, LEFT, RIGHT}

	public bool topSwitch, leftSwitch, rightSwitch, bottomSwitch;
    private bool needToPickASwitch;

    public bool currentlyAttacking;

    private bool currentlyLerping;
    private bool goingAway;
    private float startTime;
    private float endTime;
    private Direction currentDirection;
    
	// Use this for initialization
	void Start () {
		topSwitch = false;
		leftSwitch = false;
		rightSwitch = false;
		bottomSwitch = false;
        needToPickASwitch = true;
        goingAway = false;
        endTime = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (needToPickASwitch) PickASwitch();
        if (currentlyLerping) Lerp(currentDirection);
        if (!currentlyAttacking)
        {
            BodySlam(Direction.DOWN);
        }

        
    }

    void Lerp(Direction direction)
    {
        Vector2 startVector = transform.position;
        Vector2 endVector = Vector2.down;//default val
        if (direction == Direction.DOWN)
        {
            endVector = transform.position + .13f * Vector3.down;
        }
        if (goingAway)
        {
            if (Time.time - startTime < endTime)
            {
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
            Debug.Log(Lerpval);
                transform.position = Vector2.Lerp(startVector, endVector, Lerpval);
            }
            else
            {
                startTime = Time.time;
                goingAway = false;
            }
        }
        else if (!goingAway)
        {
            if (Time.time - startTime < endTime)
            {
                //calculate opposite end vector
                Vector2 tempV = endVector - startVector;
                endVector = startVector - tempV;
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
                transform.position = Vector2.Lerp(startVector, endVector, Lerpval);
            }
            else currentlyLerping = false;
        }
    }

	void BodySlam(Direction direction){
        currentlyAttacking = true;
        startTime = Time.time;
        if (direction == Direction.DOWN)
            currentDirection = Direction.DOWN;
        currentlyLerping = true;
        goingAway = true;
	}

	void Attack2() {

	}

	public void FlipRightSwitch(){
		rightSwitch = true;
        Switch currentswitch = transform.FindChild("RightSwitch").gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipTopSwitch(){
		topSwitch = true;
        Switch currentswitch = transform.FindChild("TopSwitch").gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipLeftSwitch(){
		leftSwitch = true;
        Switch currentswitch = transform.FindChild("LeftSwitch").gameObject.GetComponent<Switch>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipBottomSwitch(){
		bottomSwitch = true;
        Switch currentswitch = transform.FindChild("BottomSwitch").gameObject.GetComponent<Switch>();
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
        if (!topSwitch) list.Add(transform.FindChild("TopSwitch"));
        if (!bottomSwitch) list.Add(transform.FindChild("BottomSwitch"));
        if (!rightSwitch) list.Add(transform.FindChild("RightSwitch"));
        if (!leftSwitch) list.Add(transform.FindChild("LeftSwitch"));
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
