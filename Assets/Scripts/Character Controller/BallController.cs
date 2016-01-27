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
    public enum State { SPAWNING, IDLE, STUCK, LAUNCHING, LANDING }
    public AudioClip landSound;
    public AudioSource audioSource;
    public State state = State.SPAWNING;
    public Platform controllingPlatform;
    public float landingTolerance = 1.1f;
    public float spwanGracePeriod = 1.0f;
    public SpriteRenderer sprite;
    private int jumps = 0;
    private Rigidbody2D rb;
    private float lastHit;
    private float bounceBufferPeriod = .4f;
    private RespawnController respawner;
    private SquidLauncher squid;

    /**
     * Description: This method currently sets up a reference to the ball's 
     *              AudioSource
     */
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        respawner = GameObject.Find("Respawner/Spawner").GetComponent<RespawnController>();
        squid = GameObject.Find("Player Holder/Squid Launcher").GetComponent<SquidLauncher>();
    }
    /**
     * Description: This method currently will only play the landing sound when
     *              a ball hits a 2D collider.
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null)
            audioSource.PlayOneShot(landSound, 1f);
        if (state == State.LAUNCHING)
            state = State.LANDING;
        lastHit = Time.time;
    }
    void Update()
    {
        switch (state)
        {
            case State.SPAWNING:
                MakeInvincible();
                Invoke("AllowMortality", spwanGracePeriod);
                state = State.IDLE;
                break;
            case State.IDLE:
                ResetJumps();
                break;
            case State.STUCK:
                ResetJumps();
                break;
            case State.LAUNCHING:
                controllingPlatform = null;
                break;
            case State.LANDING:
                HasLanded();
                break;
        }
        //out of bounds kill
        if (transform.position.magnitude > 250)
            respawner.kill();
        if (Physics2D.gravity == Vector2.zero)
            state = State.IDLE;
        
    }
    public void MakeInvincible()
    {
        gameObject.tag = "Untagged";
    }
    public void AllowMortality()
    {
        gameObject.tag = "Player";
        sprite.color = new Color(255, 255, 255, 255);
    }
    public void HasLanded()
    {
        if (Time.time - lastHit < bounceBufferPeriod)
            return;
        Vector3 gravity = Physics2D.gravity;
        Vector3 velocityProjG = Vector3.Project(rb.velocity, gravity);
        if ((velocityProjG+gravity).magnitude < landingTolerance * gravity.magnitude)
        {
            state = State.IDLE;
        }
    }

    public void IncrementJumps()
    {
        jumps++;
        if (jumps >= squid.maxJumps)
            state = State.LAUNCHING;
    }
    public void ResetJumps()
    {
        jumps = 0;
    }

    public int GetJumps()
    {
        return jumps;
    }
    
}
