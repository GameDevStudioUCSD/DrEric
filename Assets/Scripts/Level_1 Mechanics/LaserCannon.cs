using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour
{

    /** State definitions for Platform.cs */
    public enum STATE { WAITING, SEARCHING, BLOATING, FIRING, RETURNING }
    /** This is the starting vector of this game object*/
    public Vector2 startVector = new Vector2(1, 1);
    /** This is the end vector of the object's path */
    public Vector2 endVector = new Vector2(2, 1);
    /** This is the amount of time in seconds that it takes the game object to 
      * move from startVector to endVector */
    [Tooltip("This is the amount of time in seconds that it takes the game object to move from startVector to endVector")]
    public float movementTime = 2;
	public bool hasPlayedSound = false;
    public bool firing = false;
    public float Bloattime = 5;//time it takes to bloat
    public float bloatScaleInc = .01F;//bloat scale per tic
    public float FiringTime = 1;//time firing
    // Private variables
    private float startTime = 0;
    public STATE state = STATE.WAITING;
    private RhythmController rhythmController;
    private Vector2 currentVector;
    private Vector3 originalScale;
    private RespawnController respawner;
    private float speed = 1;

    void Start()
    {//initialize stuff
        rhythmController = RhythmController.GetController();
        originalScale = transform.localScale;
        transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>().enabled = false;
        respawner = RespawnController.GetRespawnController();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != null && other.tag == "Player")
        {
            if (state == STATE.SEARCHING)//stop and start firing when player is found
            {
                startTime = Time.time;
                AudioSource source = GetComponent<AudioSource>();
                source.pitch = speed;
                source.Play();
                state = STATE.BLOATING;
            }
            else if (state == STATE.FIRING)//kill when firing
            {
                respawner.kill();
            }
        }
    }
    void Update()
    {
        switch (state)//determine behavior
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.SEARCHING:
                Searching();
                break;
            case STATE.BLOATING:
                Bloating();
                break;
            case STATE.FIRING:
                Firing();
                break;
            case STATE.RETURNING:
                Returning();
                break;
        }
    }

    void Waiting()//wait until boss1 tells cannons to fire
    {
        if (firing == true)
        {
            state = STATE.SEARCHING;
            startTime = Time.time;
        }
    }

    void Searching()//move right until player found
    {
        float lerpVal = (Time.time - startTime) / (movementTime);
        transform.position = Vector2.Lerp(startVector, endVector, lerpVal);
        if(Time.time - startTime > movementTime)
        {
            startTime = Time.time;
            Vector2 swap = endVector;
            endVector = startVector;
            startVector = swap;
        }
    }

    void Bloating()
    {
		//to prevent it from playing the sound a billion times
        currentVector = transform.position;
        if (Time.time - startTime <= Bloattime/speed)//bloat to charge up lazor
        {
            transform.localScale += new Vector3(0, bloatScaleInc, 0);
        }
        else
        {
            transform.localScale = originalScale;//go back to original scale
            startTime = Time.time;
            transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>().enabled = true;
			//So that the noise can play next time
            state = STATE.FIRING;
        }

    }

    void Firing()//no animation, kill frames activated
    {
        SpriteRenderer lazerSprite = transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>();
        if (Time.time - startTime <= FiringTime/speed)
        {
            lazerSprite.enabled = true;
        }
        else
        {
            startTime = Time.time;
            state = STATE.RETURNING;
            lazerSprite.enabled = false;
        }
    }

    void Returning()//go back to original position
    {
        if (Time.time - startTime <= movementTime)
        {
            float lerpVal = (Time.time - startTime) / (movementTime / rhythmController.GetPitch());
            transform.position = Vector2.Lerp(currentVector, startVector, lerpVal);

        }
        else
        {
            transform.position = Vector2.Lerp(currentVector, startVector, 1);
            firing = false;
            state = STATE.WAITING;
        }
    }
    public void Fire(int speed)
    {
        firing = true;
        this.speed = 1.0f + ((speed-1) * .4f);
    }
}