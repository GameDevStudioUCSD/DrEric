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
    public enum State { IDLE, STUCK, LAUNCHING, LANDING }
    public AudioClip landSound;
    public AudioSource audio;
    public State state = State.IDLE;
    public Platform controllingPlatform;
    private OrientWithGravity orientor;
    private Rigidbody2D rb;
    private float lastHit;
    private float bounceBufferPeriod = .2f;
    
    /**
     * Description: This method currently sets up a reference to the ball's 
     *              AudioSource
     */
    void Start()
    {
        orientor = GetComponent<OrientWithGravity>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    /**
     * Description: This method currently will only play the landing sound when
     *              a ball hits a 2D collider.
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(audio != null)
            audio.PlayOneShot(landSound, 1f);
        state = State.LANDING;
        lastHit = Time.time;
    }
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                //orientor.CheckOrientation();
                break;
            case State.LAUNCHING:
                controllingPlatform = null;
                //orientor.CheckOrientation();
                break;
            case State.LANDING:
                hasLanded();
                break;
        }
    }
    void hasLanded()
    {
        if (Time.time - lastHit < bounceBufferPeriod)
            return;
        Vector3 gravity = Physics2D.gravity;
        Vector3 velocityProjG = Vector3.Project(rb.velocity, gravity);
        Vector3 condition = (velocityProjG + gravity);
        if (condition.magnitude < gravity.magnitude)
        {
            Debug.Log("You just landed!");
            state = State.IDLE;
        }
    }
    
}
