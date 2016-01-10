using UnityEngine;
using System.Collections;

public class AccelerationLauncher : MonoBehaviour
{
    /** Filename: AccelerationLauncher.cs \n
     * Author: James Allen \n
     * Contributing authors: ^ \n 
     * Date Drafted: 11/9/2015 \n
     * Description: launcher things \n
     */

    public Vector2 acceleration;
    public float forceScalar = .1f;
    public float maxVelocity = 50;
    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")//|| 
        //other.gameObject.tag == "")
        {
            rigidBody = other.GetComponent<Rigidbody2D>();
            float forceScalar = this.forceScalar;
            if(rigidBody.velocity.magnitude < maxVelocity)
                forceScalar = this.forceScalar * rigidBody.velocity.magnitude;
            else
                forceScalar =  rigidBody.velocity.magnitude;
            rigidBody.velocity = Vector2.zero;
            if (rigidBody == null)
            {
                Debug.LogError("Null Reference Exception: No 2D Rigidbody found on " + this);
            }
            else
            {

                rigidBody.AddForce(forceScalar * acceleration.normalized, ForceMode2D.Impulse);
            }

        }
    }

}
