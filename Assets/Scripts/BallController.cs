using UnityEngine;
using System.Collections;

/**
 * Filename: BallController.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: 8/2/2015 \n
 * Description: A simple ball controller script. The idea is to have all 
 *              ball control scripting needs here. By our game's nature, Dr. 
 *              Eric can simply be thought of as a ball. Currently, this script
 *              only plays Dr. Eric's landing sound when he collides with a 2D
 *              object collider.
 */
public class BallController : MonoBehaviour {
    public enum State { IDLE, STUCK, LAUNCHING }
    public AudioClip landSound;
    public AudioSource audio;
    public State state = State.IDLE;
    public Platform controllingPlatform;
    private Vector2 gravity;
    private Quaternion destRotation;
    public float rotationScaler = 5;
    /**
     * Description: This method currently sets up a reference to the ball's 
     *              AudioSource
     */
    void Start()
    {
        gravity = 9.81f * Vector2.down;
        destRotation = transform.rotation;
        audio = GetComponent<AudioSource>();
    }
    /**
     * Description: This method currently will only play the landing sound when
     *              a ball hits a 2D collider.
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(audio != null)
            audio.PlayOneShot(landSound, 1f);
    }
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                CheckOrientation();
                break;
            case State.LAUNCHING:
                controllingPlatform = null;
                CheckOrientation();
                break;
        }
    }
    void CheckOrientation()
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
