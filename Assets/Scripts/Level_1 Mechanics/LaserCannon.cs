using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour
{

    /** State definitions for Platform.cs */
    enum STATE { WAITING, SEARCHING, BLOATING, FIRING, RETURNING }
    /** This is the starting vector of this game object*/
    public Vector2 startVector = new Vector2(1, 1);
    /** This is the end vector of the object's path */
    public Vector2 endVector = new Vector2(2, 1);
    /** This is the amount of time in seconds that it takes the game object to 
      * move from startVector to endVector */
    [Tooltip("This is the amount of time in seconds that it takes the game object to move from startVector to endVector")]
    public float movementTime = 2;
    public bool firing = false;
    public float Bloattime = 5;//time it takes to bloat
    public float bloatScaleInc = .01F;//bloat scale per tic
    public float FiringTime = 1;//time firing
    // Private variables
    private float startTime = 0;
    private STATE state = STATE.WAITING;
    private RhythmController rhythmController;
    private Vector2 currentVector;
    private Vector3 originalScale;
    public RespawnController respawner;

    void Start()
    {//initialize stuff
        rhythmController = RhythmController.GetController();
        originalScale = transform.localScale;
        transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == STATE.SEARCHING)//stop and start firing when player is found
            {
                startTime = Time.time;
                state = STATE.BLOATING;
            }
            else if (state == STATE.FIRING)//kill when firing
            {
                respawner.kill();
                Debug.Log("DIE");
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
        if (lerpVal == 1)//if player isn't found, go back
        {
            currentVector = transform.position;
            state = STATE.RETURNING;
        }
    }

    void Bloating()
    {
        currentVector = transform.position;
        if (Time.time - startTime <= Bloattime)//bloat to charge up lazor
        {
            transform.localScale += new Vector3(0, bloatScaleInc, 0);
        }
        else
        {
            transform.localScale = originalScale;//go back to original scale
            startTime = Time.time;
            transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>().enabled = true;
            state = STATE.FIRING;
        }

    }

    void Firing()//no animation, kill frames activated
    {
        if (Time.time - startTime <= FiringTime)
        {
        }
        else
        {
            startTime = Time.time;
            state = STATE.RETURNING;
            transform.FindChild("creambeamattack").GetComponent<SpriteRenderer>().enabled = false;
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
}