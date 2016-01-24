using UnityEngine;
using System.Collections;
/**
 * Filename: FlingObject.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: Daniel Griffiths \n
 * Date Drafted: ??? (Sometime during finals Spring Quarter '15) \n
 * Description: This script translate specific user mouse movement into an 
 *              impulse vector that flings the parent object through 2D space.
 *              This impulse vector is constructed by recording where the user 
 *              clicks and releases their mouse.
 */
public class FlingObject : MonoBehaviour {
    /** This field scales the impulse vector */
    public float impulseScalar = -0.03f;
    /** This caps the absolute max speed the object may fling in the x 
     *  direction */
    public float maxXSpeed = 25;
    /** This caps the absolute max speed the object may fling in the y 
     *  direction */
    public float maxYSpeed = 25;
    /** Set true to see debugging information */
    public bool isDebugging = false;
    private Vector2 initialVector, finalVector, deltaVector;
    private Rigidbody2D rigidBody;
    private BallController playerController;
    public bool isReverseControl;
    /** Saves a reference to the object's 2D Rigidbody.\n
     *  Will throw a NullReferenceExcepetion if this method cannot find a 2D
     *  Rigidbody. */
	void Start () {
        isReverseControl = false;
        rigidBody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<BallController>();
        if (rigidBody == null)
        {
            Debug.LogError("Null Reference Exception: No 2D Rigidbody found on " + this);
        }
	}
	/** Constructs the impulse vector */
	public Vector2 CalculateDelta(Vector2 initialVector, Vector2 finalVector) {
        // Calculate the delta vector
		deltaVector = finalVector - initialVector;
        deltaVector.x = -1 * deltaVector.x;
        deltaVector.y = -1 * deltaVector.y;
		
		if (isReverseControl)
		{
			deltaVector = -deltaVector;         //use jointly with ReverseControl method
		}
		
		deltaVector *= impulseScalar;
		// Cap the x and y speeds by their max speeds
		if (deltaVector.x > maxXSpeed)
			deltaVector.x = maxXSpeed;
		if (deltaVector.x < -1 * maxXSpeed)
			deltaVector.x = -1 * maxXSpeed;
		if (deltaVector.y > maxYSpeed)
			deltaVector.y = maxYSpeed;
		if (deltaVector.y < -1 * maxYSpeed)
			deltaVector.y = -1 * maxYSpeed;
		return deltaVector;
	}
    /** Applies vector as a force to the parent object. This method also will
     *  play a jump sound! */
    public void Fling(Vector2 deltaVector)
    {
        float ericObjectiveAngle = Mathf.Atan2(Physics2D.gravity.y, -Physics2D.gravity.x) * Mathf.Rad2Deg + 90;
        float rotationAngle = -1 * ericObjectiveAngle * Mathf.Deg2Rad;
        float sinAngle = Mathf.Sin(rotationAngle);
        float cosAngle = Mathf.Cos(rotationAngle);
        float rotatedX = ((deltaVector.x * cosAngle) - (deltaVector.y * sinAngle));
        float rotatedY = ((deltaVector.x * sinAngle) + (deltaVector.y * cosAngle));
        Vector2 rotatedVector = new Vector2(rotatedX, rotatedY);
        rigidBody.AddForce(rotatedVector, ForceMode2D.Impulse);
        // To make this method more abstract, this has to be moved...
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        testAnimation();
        playerController.Jump();
    }
    private void testAnimation()
    {
        foreach(Transform t in transform)
        {
            Animator animator = t.gameObject.GetComponent<Animator>();
            if(animator != null)
                animator.SetBool("IsFlying", !animator.GetBool("IsFlying"));
        }
    }


    /*
    * reverse the control by changing the boolean variable isReverseControl,
    * which switches deltaVector to -deltaVector upon fling calculation,
    * and achieves the effect of reversing the control
    */
    public void reverseControl()
    {
        isReverseControl = !isReverseControl;
        if (isDebugging)
        {
            Debug.Log("Reverse-Control status changed to:" + isReverseControl);
        }
        
    }

}
