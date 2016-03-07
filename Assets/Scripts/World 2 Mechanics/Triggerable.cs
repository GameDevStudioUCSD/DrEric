using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Triggerable
/// 
/// Script component that has Trigger function that can be called by other object.
/// For example a button when pressed can call Trigger function in other object with this script.
/// 
/// TODO: Currently is not a true "interface" this needs to be refactored
/// 
/// </summary>

public class Triggerable : MonoBehaviour {

    // What should the obstacle's starting position be?
    public bool startAtLoweredPosition = true;

    // The end positions of raising or lowering
    public Vector2 raisedPosition;
    public Vector2 loweredPosition;

    // Rate at which obstacle should lerp
    public float moveRate = 0.1f;

    // States this obstacle can be in
    enum State { Initial, Raised, Lowered, Raising, Lowering };

    // The current state the obstacle is in
    private State currentState = State.Initial;

    // The position the obstacle is currently at if it is going from lowering to raising or vice versa
    private Vector2 intermediatePosition;

    // Used by lerp to determine how much the object has moved
    private float positionFraction = 0.0f;

	// Use this for initialization
	void Start () {
	    // Initialize state based on whether the obstacle starts at lowered or raised position
        currentState = startAtLoweredPosition ? State.Lowered : State.Raised;
	}
	
	// Update is called once per frame
	void Update () {

        // Do something based on what state the obstacle is in
        switch(currentState)
        {
            case State.Raised:
                break;

            case State.Lowered:
                break;

            case State.Raising:
                Raise();
                break;

            case State.Lowering:
                Lower();
                break;
        }
	
	}

    /*
     * Trigger
     * 
     * Access function for obstacle.  Button will have to know about this function
     * to cause the obstacle to lower/raise.
     */
    public void Trigger()
    {
        // If the obstacle is currently in raised position or rising
        if(currentState == State.Raised || currentState == State.Raising)
        {
            currentState = State.Lowering;
            Debug.Log("Lowering!!");
        }
        // If the obstacle is currently in lowered position or lowering
        else if(currentState == State.Lowered || currentState == State.Lowering)
        {
            currentState = State.Raising;
            Debug.Log("Raising!!!!");
        }

        // Get the intermediate position so lerp doesn't jump around
        intermediatePosition = this.transform.position;

    }

    private void Raise()
    {
        Debug.Log("R");
        if (positionFraction < 1.0)
        {
            positionFraction += Time.deltaTime * moveRate;
            this.transform.position = Vector2.Lerp(intermediatePosition, raisedPosition, positionFraction);
        }
        else
        {
            currentState = State.Raised;
        }
    }

    private void Lower()
    {
        Debug.Log("L");
        if (positionFraction > 0.0)
        {
            positionFraction -= Time.deltaTime * moveRate;
            this.transform.position = Vector2.Lerp(loweredPosition, intermediatePosition, positionFraction);
        }
        else
        {
            currentState = State.Lowered;
        }
    }
}
