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
    public enum State { IDLE, STUCK, LANDING }
    public AudioClip landSound;
    public AudioSource audioSource;
    public State state = State.IDLE;
    public Platform controllingPlatform;
    public float landingTolerance = 1.1f;
    private int jumps = 0;
    private Rigidbody2D rb;
    private float lastHit;
    private float bounceBufferPeriod = .4f;
    private RespawnController respawner;
    private SquidLauncher squid;
    int resets = 0;
    float resetTimer = 0;
    float resetTimeout = 0.1f;

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
        HasLanded();
        lastHit = Time.time;
    }
    void Update()
    {
        if (rb.velocity.magnitude < 0.1 && squid.state == SquidLauncher.State.NORMAL)
        {
            if (resetTimer == 0)
                resetTimer = Time.time;
            if (Time.time - resetTimer > resetTimeout)
                Land();
        }
        else resetTimer = 0;

        //out of bounds kill
        if (respawner.player.transform.position.x < -1000 ||
            respawner.player.transform.position.x > 1000 ||
            respawner.player.transform.position.y < -1000 ||
            respawner.player.transform.position.y > 1000)
            respawner.kill();
    }
    public void HasLanded()
    {
        Debug.Log(Time.time - lastHit);
        if (Time.time - lastHit < bounceBufferPeriod)
            return;
        Vector3 gravity = Physics2D.gravity;
        Vector3 velocityProjG = Vector3.Project(rb.velocity, gravity);
        if ((velocityProjG+gravity).magnitude < landingTolerance * gravity.magnitude)
            Land();
    }

    public void Land()
    {
        state = State.IDLE;
        jumps = 0;
        resetTimer = 0;
    }

    public void Jump()
    {
        jumps++;
        if (jumps >= squid.maxJumps)
            state = State.LANDING;
    }

    public int GetJumps()
    {
        return jumps;
    }
    
}
