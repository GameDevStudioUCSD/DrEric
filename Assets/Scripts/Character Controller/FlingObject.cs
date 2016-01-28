using UnityEngine;
using System.Collections;
/**
 * Filename: FlingObject.cs
 * Author: Michael Gonzalez
 * Contributing Authors: Daniel Griffiths
 * Date Drafted: ??? (Sometime during finals Spring Quarter '15)
 * Description: This script translate specific user mouse movement into an 
 *              impulse vector that flings the parent object through 2D space.
 *              This impulse vector is constructed by recording where the user 
 *              clicks and releases their mouse.
 */
public class FlingObject : MonoBehaviour {
    public float impulseScalar = -0.03f; //scale applied to impulse vector
    public float maxXSpeed = 25; //caps speed along x-axis
    public float maxYSpeed = 25; //caps speed along y-axis

    public bool isReverseControl; //fling opposite direction if true
    
    public bool isDebugging = false; //controls display of debug messages

    private Vector2 initialVector, finalVector, deltaVector;
    private Rigidbody2D rigidBody;
    private BallController playerController;

    /** 
     * Description: Initializes reference variables
     */
	void Start () {
        isReverseControl = false;
        playerController = GetComponent<BallController>();
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            Debug.LogError("Null Reference Exception: No 2D Rigidbody found " +
                "on " + this);
        }
	}

	/**
     * Description: Calculates the launch speed and direction appropriate for
     *              the input provided by the mouse
     */
	public Vector2 CalculateDelta(Vector2 initialVector, Vector2 finalVector) {
        // Calculate the delta vector
		deltaVector = finalVector - initialVector;
        deltaVector.x = -1 * deltaVector.x;
        deltaVector.y = -1 * deltaVector.y;
        deltaVector *= impulseScalar;

        if (isReverseControl)
			deltaVector = -deltaVector; //responds to ReverseControl method
		
		// Cap the x and y speeds by their max speeds
		if (deltaVector.x > maxXSpeed)
			deltaVector.x = maxXSpeed;
		if (deltaVector.x < -maxXSpeed)
			deltaVector.x = -maxXSpeed;
		if (deltaVector.y > maxYSpeed)
			deltaVector.y = maxYSpeed;
		if (deltaVector.y < -maxYSpeed)
			deltaVector.y = -maxYSpeed;

		return deltaVector;
	}

    /**
     * Description: Applies the calculated vector as a force to the parent
     *              object. It also plays the jumping sound effect
     */
    public void Fling(Vector2 deltaVector)
    {
        //Calculates the difference between current and natural gravity
        float gravityRotation = -(Mathf.Atan2(Physics2D.gravity.y,
            -Physics2D.gravity.x) + Mathf.PI/2);
        float sinAngle = Mathf.Sin(gravityRotation);
        float cosAngle = Mathf.Cos(gravityRotation);
        
        //Adjusts the provided vector for gravity
        float rotatedX = ((deltaVector.x * cosAngle) -
            (deltaVector.y * sinAngle));
        float rotatedY = ((deltaVector.x * sinAngle) +
            (deltaVector.y * cosAngle));
        Vector2 rotatedVector = new Vector2(rotatedX, rotatedY);
        if (Physics2D.gravity == Vector2.zero)
            rotatedVector = deltaVector;

        //Fling
        rigidBody.AddForce(rotatedVector, ForceMode2D.Impulse);
        playerController.IncrementJumps();

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        testAnimation();
    }

    /**
     * Description: Reverses the fling direction relative to mouse movement
     */
    public void reverseControl()
    {
        isReverseControl = !isReverseControl;
        if (isDebugging)
        {
            Debug.Log("Reverse-Control status changed to:" + isReverseControl);
        }
        
    }

    /**
     * Description: Tests an Animator, currently non-existent
     *
     * TODO: Remove?
     */
    private void testAnimation()
    {
        foreach(Transform t in transform)
        {
            Animator animator = t.gameObject.GetComponent<Animator>();
            if(animator != null)
                animator.SetBool("IsFlying", !animator.GetBool("IsFlying"));
        }
    }

}
