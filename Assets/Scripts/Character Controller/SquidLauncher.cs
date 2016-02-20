using UnityEngine;
using System.Collections;
/**
 * Filename: SquidLauncher.cs \n
 * Author: Daniel Griffiths \n
 * Contributing Authors: None \n
 * Date Drafted: November 14, 2015 \n
 * Description: This script is used by the squid launcher, which is responsible
 *              for sending DrEric flying around the map.
 */
public class SquidLauncher : MonoBehaviour
{
    public int maxJumps = 2; //max times allowed to jump without landing
    public float maxGrabTime = 3; //max seconds DrEric can be held

    public float grabRange = 2; //max distance from player to allow grab
    public float rotationSpeed = 5; //speed of rotation around held DrEric
    public float rotationOffset = 0; //aesthetic change in direction

    public Camera activeCamera;

    private float grabTime = 0; //time held
    private bool alreadyGrabbed = false; //prevents trying to grab while held

    //sprites with extended tentacles
    public Sprite launchSprite0;
    public Sprite launchSprite1;
    public Sprite launchSprite2;
    public Sprite launchSprite3;
    public Sprite launchSprite4;
    public Sprite launchSprite5;
    public Sprite launchSprite6;

    public enum State { NORMAL, GRABBED };
    public State state = State.NORMAL;
    private int grabSprite = 0; //grabbing sprite for current vector

    private const float xOffset = -.03f; //compensates for sprite assymetry
    private float maxSpeed; //calculated from FlingObject

    private GameObject drEric = null;
    private OrientWithGravity orient;
    private Transform idleSprite;
    private Transform launchingSprite;
    private Quaternion destRotation;

    private Vector2 initialVector;
    private Vector2 deltaVector;

    /**
     * Description: This method identifies sprites for manual animations and
     *              initializes physics
     */
    void Start()
    {
        idleSprite = transform.Find("Idle Sprite");
        launchingSprite = transform.Find("Launching Sprite");
        destRotation = transform.rotation;
        orient = GetComponent<OrientWithGravity>();
    }

    /**
     * Description: Orients with gravity, finds DrEric, animates grabbing
     *              sprites, rotates, and launches or releases DrEric
     */
    void Update()
    {
        UpdateAnimation();
        //Rotates to match gravity when not holding DrEric
        if (state != State.GRABBED)
            orient.CheckOrientation();

        if (drEric != null)
        {
            if (state == State.GRABBED)
            {
                AnimateSprite();
                Rotate(); //rotation around DrEric while grabbed
            }

            if (Input.GetMouseButtonUp(0))
            {
                alreadyGrabbed = false;
                //if grabbing animation has concluded, launch
                if (state == State.GRABBED)
                    Launch();
                //stop grabbing animation partway through
                else
                    state = State.NORMAL;
            }
        }
        else
        {
            state = State.NORMAL;
            FindDrEric();
        }
    }


    void UpdateAnimation()
    {
        switch(state)
        {
            case State.GRABBED:
                idleSprite.GetComponent<SpriteRenderer>().enabled = false;
                launchingSprite.GetComponent<SpriteRenderer>().enabled = true;
                break;
            default:
                idleSprite.GetComponent<SpriteRenderer>().enabled = true;
                launchingSprite.GetComponent<SpriteRenderer>().enabled = false;
                break;

        }
    }

    /**
     * Description: This method finds DrEric after he spawns or respawns. Also
     *              calls CalculateMaxSpeed() and sets follow target when found
     */
    void FindDrEric()
    {
        try
        {
            drEric = GameObject.Find(Names.PLAYERHOLDER).transform.Find(
                Names.DRERIC).gameObject;
            CalculateMaxSpeed();
            GetComponent<FollowObject>().followTarget = drEric;
        }
        catch //fails to find DrEric while dead
        {
            //follow player holder instead
            GetComponent<FollowObject>().followTarget =
                transform.parent.gameObject;
        }
    }

    /**
     * Description: This method finds the maximum allowable speed for DrEric
     *              using the values set in his FlingObject component
     */
    void CalculateMaxSpeed()
    {
        FlingObject fo = drEric.GetComponent<FlingObject>();
        float x = fo.maxXSpeed;
        float y = fo.maxYSpeed;
        maxSpeed = new Vector2(x, y).magnitude;
    }

    /**
     * Description: This method responds to mouse input by grabbing DrEric,
     *              when appropriate
     */
    void OnMouseOver()
    {
        if (drEric != null && !(drEric.transform.parent.tag == "Launcher"))
        {
            //Grabs if not currently grabbing or grabbed and within range, when
            //  mouse is pressed
            if ((state == State.NORMAL)
                && IsGrabbable() && Input.GetMouseButton(0))
            {
                //Initial click point used for movement calculations
                initialVector = Input.mousePosition;
                GrabDrEric();
            }
        }
    }

    /**
     * Description: Checks if DrEric can currently be grabbed. Checks that the
     *              squid's grabbing point (bottom of fully-extended tentacles)
     *              is within grabRange of DrEric and that the maximum number
     *              of jumps has not been exceeded.
     */
    private bool IsGrabbable()
    {
        return (!alreadyGrabbed && Vector3.Distance(transform.position,
            drEric.transform.position) <= grabRange
            && drEric.GetComponent<BallController>().GetJumps() < maxJumps);
    }

    /**
     * Description: Grabs DrEric by changing state, zeroing his velocity, and
     *              disabling the effects of gravity
     */
    private void GrabDrEric()
    {
        alreadyGrabbed = true;
        state = State.GRABBED;
        grabTime = Time.time;

        grabSprite = 1;
        AnimateSprite();

        drEric.GetComponent<Rigidbody2D>().gravityScale = 0;
        drEric.GetComponent<Rigidbody2D>().angularVelocity = 0;
        drEric.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        deltaVector = drEric.GetComponent<FlingObject>().CalculateDelta(
            initialVector, Input.mousePosition);
    }

    /**
     * Description: Determines which grabbing sprite is correct for the current
     *              distance pulled back
     */
    private void SetGrabSprite()
    {
        deltaVector = drEric.GetComponent<FlingObject>().CalculateDelta(
            initialVector, Input.mousePosition);
        float magnitude = deltaVector.magnitude;
        int numOfSprites = 6;
        for (int i = 1; i <= numOfSprites; i++)
        {
            if (magnitude >= (i * maxSpeed) / numOfSprites)
                grabSprite = i;
        }
    }

    /**
     * Description: Sets the current sprite from the grabbing animation based
     *              on distance pulled back
     */
    private void AnimateSprite()
    {
        SetGrabSprite();
        SpriteRenderer sprite = launchingSprite.GetComponent<SpriteRenderer>();
        switch (grabSprite)
        {
            case 1:
                sprite.sprite = launchSprite1;
                launchingSprite.localPosition = new Vector3(xOffset, .64f, 0);
                break;
            case 2:
                sprite.sprite = launchSprite2;
                launchingSprite.localPosition = new Vector3(xOffset, 1.13f, 0);
                break;
            case 3:
                sprite.sprite = launchSprite3;
                launchingSprite.localPosition = new Vector3(xOffset, 1.62f, 0);
                break;
            case 4:
                sprite.sprite = launchSprite4;
                launchingSprite.localPosition = new Vector3(xOffset, 2.41f, 0);
                break;
            case 5:
                sprite.sprite = launchSprite5;
                launchingSprite.localPosition = new Vector3(xOffset, 3.39f, 0);
                break;
            case 6:
                sprite.sprite = launchSprite6;
                launchingSprite.localPosition = new Vector3(xOffset, 4.20f, 0);
                break;
        }
    }

    /**
     * Description: Rotates squid around DrEric. Squid points in the direction
     *              of the deltaVector. Orbits by linear interpolation at
     *              rotationSpeed
     */
    private void Rotate()
    {
        initialVector = activeCamera.WorldToScreenPoint(transform.position);
        deltaVector = drEric.GetComponent<FlingObject>().CalculateDelta(
            initialVector, Input.mousePosition);

        float angle = Mathf.Atan2(deltaVector.y, deltaVector.x) * Mathf.Rad2Deg
            + rotationOffset;
        float gravityOffset = Mathf.Atan2(Physics2D.gravity.y,
            Physics2D.gravity.x) * Mathf.Rad2Deg;
        if (Physics2D.gravity.magnitude == 0)
            gravityOffset = -90;
        destRotation = Quaternion.Euler(0, 0, angle + gravityOffset);

        if (transform.rotation != destRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                destRotation, Time.deltaTime * rotationSpeed);
        }
    }

    /**
     * Description: Releases DrEric with the direction and speed indicated by
     *              deltaVector. CalculateDelta is run every frame by
     *              SetGrabSprite(), so doesn't need to be called here
     */
    private void Launch()
    {
        drEric.GetComponent<Rigidbody2D>().gravityScale = 1;
        drEric.GetComponent<FlingObject>().Fling(deltaVector);
        state = State.NORMAL;
    }

    /**
     * Description: Accessor for DrEric used by PlayerHolder
     */
    public GameObject getDrEric()
    {
        return drEric;
    }
}
