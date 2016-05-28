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

    public float sensitivity = 2.4f;

    public float grabRange = 2; //max distance from player to allow grab
    public float rotationSpeed = 5; //speed of rotation around held DrEric
    public float rotationOffset = 0; //aesthetic change in direction
    public bool pastSprites = false; //use past sprites if true

    public Camera activeCamera;

    private float grabTime = 0; //time held
    private bool alreadyGrabbed = false; //prevents trying to grab while held

    //sprites with extended tentacles
    public Sprite launchPresentSprite0;
	public Sprite launchPresentSprite1;
	public Sprite launchPresentSprite2;
	public Sprite launchPresentSprite3;
	public Sprite launchPresentSprite4;
	public Sprite launchPresentSprite5;
	public Sprite launchPresentSprite6;

	public Sprite launchPastSprite0;
	public Sprite launchPastSprite1;
	public Sprite launchPastSprite2;
	public Sprite launchPastSprite3;
	public Sprite launchPastSprite4;
	public Sprite launchPastSprite5;
	public Sprite launchPastSprite6;

    public enum State { NORMAL, GRABBED };
    public State state = State.NORMAL;
    private int grabSprite = 0; //grabbing sprite for current vector
    
    private float maxSpeed; //calculated from FlingObject

    private GameObject drEric = null;
    private RespawnController spawner = null;
    private OrientWithGravity orient;
    private Transform idleSprite;
    private Transform pastSprite;
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
        pastSprite = transform.Find("Idle Sprite (Past)");
        launchingSprite = transform.Find("Launching Sprite");
        destRotation = transform.rotation;
        orient = GetComponent<OrientWithGravity>();
        spawner = RespawnController.GetRespawnController();
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

        FindDrEric();
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
        }
    }


    void UpdateAnimation()
    {
        switch(state)
        {
            case State.GRABBED:
                idleSprite.GetComponent<SpriteRenderer>().enabled = false;
                pastSprite.GetComponent<SpriteRenderer>().enabled = false;
                launchingSprite.GetComponent<SpriteRenderer>().enabled = true;
                break;
            default:
                if (pastSprites)
                {
                    idleSprite.GetComponent<SpriteRenderer>().enabled = false;
                    pastSprite.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    idleSprite.GetComponent<SpriteRenderer>().enabled = true;
                    pastSprite.GetComponent<SpriteRenderer>().enabled = false;
                }
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
        drEric = spawner.GetDrEric();
        if (drEric != null)
        {
            CalculateMaxSpeed();
            GetComponent<FollowObject>().followTarget = drEric;
        }
        else //fails to find DrEric while dead
        {
            //follow player holder instead
            GetComponent<FollowObject>().followTarget =
                RespawnController.GetRespawnController().gameObject;
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
        float magnitude = sensitivity * deltaVector.magnitude;
        int numOfSprites = 6;
        grabSprite = (int)(magnitude * numOfSprites / maxSpeed);
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
                sprite.sprite = pastSprites ? launchPastSprite1 : launchPresentSprite1;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 1.54f : .64f, 0);
                launchingSprite.transform.localScale = new Vector3(pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
                break;
            case 2:
                sprite.sprite = pastSprites ? launchPastSprite2 : launchPresentSprite2;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 2.03f : 1.13f, 0);
                launchingSprite.transform.localScale = new Vector3 (pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
                break;
            case 3:
                sprite.sprite = pastSprites ? launchPastSprite3 : launchPresentSprite3;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 2.52f : 1.62f, 0);
                launchingSprite.transform.localScale = new Vector3(pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
                break;
            case 4:
                sprite.sprite = pastSprites ? launchPastSprite4 : launchPresentSprite4;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 2.81f : 2.41f, 0);
                launchingSprite.transform.localScale = new Vector3(pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
                break;
            case 5:
                sprite.sprite = pastSprites ? launchPastSprite5 : launchPresentSprite5;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 3.49f : 3.39f, 0);
                launchingSprite.transform.localScale = new Vector3(pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
                break;
            case 6:
                sprite.sprite = pastSprites ? launchPastSprite6 : launchPresentSprite6;
                launchingSprite.localPosition = new Vector3(pastSprites ? -0.5f : 0.1f, pastSprites ? 3.90f : 4.20f, 0);
                launchingSprite.transform.localScale = new Vector3(pastSprites ? 0.4f : 0.58034f, pastSprites ? 0.4f : 0.58034f, 0.29017f);
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
        initialVector = Camera.main.WorldToScreenPoint(transform.position);
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
        deltaVector = grabSprite == 6 ? maxSpeed * deltaVector.normalized : deltaVector; 
        drEric.GetComponent<FlingObject>().Fling(deltaVector);
        state = State.NORMAL;
    }

    /**
     * Description: Releases DrEric on the spot. Used on death if DrEric is not
     *              destroyed.
     */
    public void DropDrEric()
    {
        deltaVector = new Vector2(0, 0);
        Launch();
    }

    /**
     * Description: Accessor for DrEric used by PlayerHolder
     */
    public GameObject getDrEric()
    {
        return drEric;
    }
}
