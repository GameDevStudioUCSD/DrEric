using UnityEngine;

/**
 * Filename: BallController.cs
 * Author: Michael Gonzalez
 * Contributing Authors: Daniel Griffiths
 * Date Drafted: 8/2/2015
 * Description: A simple ball controller script. The idea is to have all 
 *              ball control scripting needs here. By our game's nature, Dr. 
 *              Eric can simply be thought of as a ball.
 */
public class BallController : MonoBehaviour {
    public AudioClip landSound;
    public SpriteRenderer sprite;
    public float landTolerance = 1.1f; //landing error tolerance
    public float spawnGracePeriod = 1.0f; //TODO: this currently does nothing

    public enum State { SPAWNING, IDLE, STUCK, LANDING }
    public State state = State.SPAWNING;
	public int numParticlesOnCollision = 5; //How many smehckels appaer when you hit a wall

    private int jumps = 0; //times jumped since last landed
    private float lastHit; //time last landed
    private const float bounceBufferPeriod = .4f; //min time between "landings"
    private float resetTimer = 0;
    private const float resetTimeout = 0.1f;
    private const float resetSpeedTolerance = 0.1f; //max speed for timeout

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private RespawnController respawner;
    private SquidLauncher squid;
	private ParticleSystem pSys;

    /**
     * Description: Sets up a reference to the ball's AudioSource and other
     *              relevant objects
     */
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        respawner = GameObject.Find("Respawner/Spawner").GetComponent<RespawnController>();
        squid = GameObject.Find("Player Holder/Squid Launcher").GetComponent<SquidLauncher>();
		pSys = GetComponent<ParticleSystem> ();
    }

    /**
     * Description: Kills a player that is out of bounds and prevents paralysis
     *              if the player is out of jumps on the ground or if gravity is
     *              zero
     */
    void Update()
    {
        if (OutOfBounds())
            respawner.kill();
        if (TimeoutLanding())
            Land();
        if (Physics2D.gravity == Vector2.zero)
            Land();
		pSys.startColor = new Color (Random.Range (0.5f, 1.0f), Random.Range (0.5f, 1.0f), Random.Range (0.5f, 1.0f));		
    }

    /**
     * Description: This method plays the landing sound when the player hits a
     *              2D collider, as well as resets the player's jumps
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
		pSys.Emit(numParticlesOnCollision);
        if (audioSource != null)
            audioSource.PlayOneShot(landSound, 1f);
        if (HasLanded())
            Land();
        lastHit = Time.time;
    }

    /**
     * Description: Accessor for the jumps field. jumps is private so it won't
     *              appear as a field in Unity
     */
    public int GetJumps()
    {
        return jumps;
    }

    /**
     * Description: Mutator used by FlingObject to track the number of times
     *              the player has jumped since they last touched the ground
     */
    public void IncrementJumps()
    {
        jumps++;
        if (jumps >= squid.maxJumps)
            state = State.LANDING;
    }

    /**
     * Description: Checks if the player has landed on the ground, in which
     *              case their jumps should be reset
     */
    public bool HasLanded()
    {
        if (Time.time - lastHit < bounceBufferPeriod)
            return false;
        else return transform.parent.GetComponent<PlayerHolder>().CheckGround();
    }

    /**
     * Description: Prevents the player from becoming paralyzed if they manage
     *              to use up all their jumps without leaving the ground.
     *              Returns true only if the player has been (roughly) immobile
     *              for a certain length of time
     */
    public bool TimeoutLanding()
    {
        if (rb.velocity.magnitude < resetSpeedTolerance &&
            squid.state == SquidLauncher.State.NORMAL)
        {
            if (Time.time - resetTimer > resetTimeout)
                return true;
            else if (resetTimer == 0)
                resetTimer = Time.time;
            return false;
        }
        else
        {
            resetTimer = 0;
            return false;
        }
    }

    /**
     * Description: Resets the player's jumps and state
     */
    public void Land()
    {
        state = State.IDLE;
        jumps = 0;
        resetTimer = 0;
        lastHit = Time.time;
    }

    /**
     * Description: Checks if the player has gone far outside the map.
     *              Hardcoded 1000 because current levels are less than 100
     *              wide
     */
    public bool OutOfBounds()
    {
        return (respawner.player.transform.position.x < -1000 ||
            respawner.player.transform.position.x > 1000 ||
            respawner.player.transform.position.y < -1000 ||
            respawner.player.transform.position.y > 1000);
    }

    /**
     * Description: Changes the player's tag, disabling the Deadly script
     */
    public void MakeInvincible()
    {
        gameObject.tag = "Invincible Player";
    }

    /**
     * Description: Changes the player's tag, enabling the Deadly script
     */
    public void AllowMortality()
    {
        gameObject.tag = "Player";
        sprite.color = new Color(255, 255, 255, 255);
    }
}
