using UnityEngine;
using System.Collections;

/**
 * Filename: OrientWithGravity.cs \n
 * Author: Steve Wang \n
 * Contributing Authors: Michael Gonzalez \n
 * Date Drafted: 11/16/2015 \n
 * Description: This script will orient a GameObject with the direction of 
 *              gravity. You need to grab a reference to this script and call
 *              CheckOrientation() inside of the update script of some 
 *              controlling script.  
 */
public class OrientWithGravity : MonoBehaviour {
    private Vector2 gravity;
    private Quaternion destRotation;
    public float rotationScaler = 5;
    [Tooltip("If this value is true, then this script will try to orient the GameObject with gravity on each update frame")]
    public bool callOnUpdate = false;
    void Start () {
        gravity = 9.81f * Vector2.down;
        destRotation = transform.rotation;
    }
	
    public void CheckOrientation()
    {
        if (Physics2D.gravity != gravity)
        {
            // rotates object in relation to gravity
            gravity = Physics2D.gravity;
            float x = gravity.x;
            float y = gravity.y;

            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg * -1;
            destRotation = Quaternion.Euler(angle - 90, 90, 0);

        }
        if (transform.rotation != destRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, Time.deltaTime * rotationScaler);
        }
    }
}
