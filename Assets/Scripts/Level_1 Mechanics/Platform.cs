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
    enum State { LERPING, WAITING }
    /** This is the starting vector of this game object*/
    public Vector2 startVector = new Vector2(1, 1);
    /** This is the end vector of the object's path */
    public Vector2 endVector = new Vector2(2, 1);
    /** This is the amount of time in seconds that it takes the game object to 
      * move from startVector to endVector */
    [Tooltip("This is the amount of time in seconds that it takes the game object to move from startVector to endVector")]
    public float movementTime = 2;
    /** This is the amount of time in seconds that the platform waits upon 
      * reaching startVector or endVector. */ 
    [Tooltip("This is the amount of time in seconds that the platform waits upon reaching startVector or endVector")]
    public float waitTime = 1;
    /** If true, then this platform will be sticky */
    public bool isSticky = true;
    /** This vector offsets Dr. Eric's placement when caught inside the 
      * collider */
    public Vector3 stickyPlacementOffset = Vector2.zero;
    /** This is the speed Dr. Eric gravitates towards this platform */
    public float stickSpeed = 1;

    // Private variables
    private float startTime = 0;
    private State state = State.WAITING;
    private BallController playerController;
    private Transform drEricTrans;
    private RhythmController rhythmController;
	
    void Start()
    {
        rhythmController = RhythmController.GetController();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // This method only matters if this object is a sticky platform
        if (!isSticky)
            return;
        GameObject colObj = other.gameObject;
        if(colObj.tag == "Player" )
        {
            // Stop the player's movements via physics
            Rigidbody2D playerPhysics = colObj.GetComponent<Rigidbody2D>();
            playerPhysics.velocity = Vector3.zero;
            // Set Dr Eric's state
            playerController = colObj.GetComponent<BallController>();
            playerController.state = BallController.State.STUCK;
            playerController.controllingPlatform = this;
            // Save Dr Eric's transform
            drEricTrans = colObj.transform;
        }
    }
	void Update () {
        MoveDrEric();
        switch(state){
            case State.WAITING:
                Waiting();
                break;
            case State.LERPING:
                Lerping();
                break;
        }
	}

    /** This method sticks Dr. Eric to the platform via lerping */
    private void MoveDrEric()
    {
        if(playerController != null && playerController.controllingPlatform == this)
        {
            Vector2 currPosVect = drEricTrans.position;
            Vector2 destPosVect = transform.position + stickyPlacementOffset;
            drEricTrans.position = Vector2.Lerp(currPosVect, destPosVect, stickSpeed * Time.deltaTime);
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
            Vector2 swapVector = startVector;
            startVector = endVector;
            endVector = swapVector;
            state = State.WAITING;
        }
        else
        {
            float lerpVal = (Time.time - startTime) / (movementTime/rhythmController.GetPitch());
            transform.position = Vector2.Lerp(startVector, endVector, lerpVal);
        }
    }
}
