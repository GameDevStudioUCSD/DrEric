using UnityEngine;
using System.Collections;

public class OrientWithGravity : MonoBehaviour {
    private Vector2 gravity;
    private Quaternion destRotation;
    public float rotationScaler = 5;
    // Use this for initialization
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
