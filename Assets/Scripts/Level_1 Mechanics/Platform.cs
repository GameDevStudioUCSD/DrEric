using UnityEngine;
using System.Collections;
using System;

/**
 * Filename: Playform.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A\n
 * Date Drafted: 11/10/2015 \n
 * Description: This script will move an object from point A to point B 
 *              smoothly. It allows developers to specify a "waitTime" where 
 *              the object will stay stationary at either point A or B for that
 *              amount of time. This script will work in general for any game
 *              object but is intended for platforms. It provides a sticky 
 *              feature that will pull Dr. Eric towards the platform if the
 *              player lands inside this object's collider. 
 * 
 */
public class Platform : MonoBehaviour {
    /** State definitions for Platform.cs */
    public enum State { LERPING, WAITING, STOP }
    /** This is the starting vector of this game object*/
    public Vector3 startVector = new Vector2(1, 1);
    /** This is the end vector of the object's path */
    public Vector3 endVector = new Vector2(2, 1);
    /** This is the amount of time in seconds that it takes the game object to 
      * move from startVector to endVector */
    [Tooltip("This is the amount of time in seconds that it takes the game object to move from startVector to endVector")]
    public float movementTime = 2;
    /** This is the amount of time in seconds that the platform waits upon 
      * reaching startVector or endVector. */ 
    [Tooltip("This is the amount of time in seconds that the platform waits upon reaching startVector or endVector")]
    public float waitTime = 1;

    public State startState = State.WAITING;
    public bool oscillate = true;


    // Private variables
    private float startTime = 0;
    public State state ;
    private BallController playerController;
    private RhythmController rhythmController;
	
    void Start()
    {
        startTime = Time.time;
        state = startState;
        rhythmController = RhythmController.GetController();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject colObj = other.gameObject;
        if(colObj.tag == "Player" )
        {
            // Stop the player's movements via physics
            Rigidbody2D playerPhysics = colObj.GetComponent<Rigidbody2D>();
            playerPhysics.velocity = Vector3.zero;
            // Set Dr Eric's state
            playerController = colObj.GetComponent<BallController>();
            // Save Dr Eric's transform
            playerController.HasLanded();
            playerController.gameObject.transform.parent.parent = this.gameObject.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        playerController.gameObject.transform.parent.parent = null;
    }
	void Update () {
        switch(state){
            case State.WAITING:
                Waiting();
                break;
            case State.LERPING:
                Lerping();
                break;

        }
	}

    /** This method is only called during the waiting state of the platform. 
     *  It just waits until waitTime seconds have passed before moving the
     *  platform again */
    void Waiting()
    {
        if(Time.time - startTime > waitTime)
        {
            startTime = Time.time;
            state = State.LERPING;
        }
    }
    /** This method uses the lerp function to smoothly move the platform 
     *  between the startVector and endVector in movementTime seconds*/
    void Lerping()
    {
        if(Time.time - startTime > movementTime/rhythmController.GetPitch())
        {
            startTime = Time.time;
            if (oscillate)
            {
                SwapVector();
                state = State.WAITING;
            }
            else
            {
                state = State.STOP;
            }
        }
        else
        {
            float lerpVal = (Time.time - startTime) / (movementTime/rhythmController.GetPitch());
            transform.position = Vector3.Lerp(startVector, endVector, lerpVal);
        }
    }

    void SwapVector()
    {
        Vector3 swapVector = startVector;
        startVector = endVector;
        endVector = swapVector;
    }
    public void SwapStates()
    {
        startTime = Time.time;
        if (state == State.WAITING)
            state = State.LERPING;
        else if (state == State.LERPING)
            state = State.WAITING;
    }
}
