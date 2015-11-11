using UnityEngine;
using System.Collections;
using System;

public class StickyPlatform : MonoBehaviour {
    enum State { LERPING, WAITING }
    public Vector2 startVector = new Vector2(1, 1);
    public Vector2 endVector = new Vector2(2, 1);
    public float timeFromStartToEnd = 2;
    public float waitInPlaceForNSeconds = 1;
    public bool isSticky = true;
    public Vector3 stickyPlacementOffset = Vector2.zero;

    private float startTime = 0;
    // This is the speed Dr. Eric gravitates towards this platform
    public float stickSpeed = 1;
    private State state = State.WAITING;
    private BallController playerController;
    private Transform drEricTrans;
	// Use this for initialization
	void Start () {
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
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
        handleDrEric();
        switch(state){
            case State.WAITING:
                Waiting();
                break;
            case State.LERPING:
                Lerping();
                break;
        }
	}

    private void handleDrEric()
    {
        if(playerController != null && playerController.controllingPlatform == this)
        {
            Vector2 currPosVect = drEricTrans.position;
            Vector2 destPosVect = transform.position + stickyPlacementOffset;
            drEricTrans.position = Vector2.Lerp(currPosVect, destPosVect, stickSpeed * Time.deltaTime);
        }
    }

    void Waiting()
    {
        if(Time.time - startTime > waitInPlaceForNSeconds)
        {
            startTime = Time.time;
            state = State.LERPING;
        }
    }
    void Lerping()
    {
        if(Time.time - startTime > timeFromStartToEnd)
        {
            startTime = Time.time;
            Vector2 swapVector = startVector;
            startVector = endVector;
            endVector = swapVector;
            state = State.WAITING;
        }
        else
        {
            Lerp();
        }
    }
    void Lerp()
    {
        float lerpVal = (Time.time - startTime) / (timeFromStartToEnd );
        transform.position = Vector2.Lerp(startVector, endVector, lerpVal);
    }
}
