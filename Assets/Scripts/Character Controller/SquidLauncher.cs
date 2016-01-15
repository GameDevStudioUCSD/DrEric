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
public class SquidLauncher : MonoBehaviour {
    public float grabRange = 2;
    public float rotationSpeed = 5;

    public float rotationOffset = 0;

    public int maxJumps = 2;
    public float maxGrabTime = 3; //seconds

    private int jumpCount = 0;
    private float grabTime = 0;
    private bool alreadyGrabbed = false;

    private GameObject DrEric = null;
    private OrientWithGravity orient;
	private Transform idleSprite;
	private Transform launchingSprite;
    private Transform directionPointer;

	public Sprite launchSprite0;
	public Sprite launchSprite1;
	public Sprite launchSprite2;
	public Sprite launchSprite3;
	public Sprite launchSprite4;
	public Sprite launchSprite5;
	public Sprite launchSprite6;

	private Vector2 initialVector;
	private Vector2 deltaVector;
    private Vector2 centerOfScreen;

    private enum State {NORMAL, GRABBING, GRABBED, RELEASING};
    private State state = State.NORMAL;
	private int spriteCounter = 0;
    
    private Vector2 startPos;
    private float launcherXOffset = -.03f;
    private Quaternion destRotation;
    private float maxSpeed;

    /*
     * Initialization. Identifies sprites for manual animations and initializes physics.
     */
    void Start ()
    {
        destRotation = transform.rotation;
        idleSprite = transform.Find ("Idle Sprite");
		launchingSprite = transform.Find ("Launching Sprite");
        centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        startPos = transform.position;
        orient = GetComponent<OrientWithGravity>();
    }
    void CalculateMaxSpeed()
    {
        FlingObject fo = DrEric.GetComponent<FlingObject>();
        float x = fo.maxXSpeed;
        float y = fo.maxYSpeed;
        maxSpeed = new Vector2(x, y).magnitude;
        Debug.Log(maxSpeed);
    }
    void Update() {
        //Unless the squid is currently grabbing DrEric, it should orient itself to gravity.
        if (state != State.GRABBED)
            orient.CheckOrientation();
        // If we're realeasing, then we need to return to the idle animation
        if (state == State.RELEASING)
        {
            idleSprite.GetComponent<SpriteRenderer>().enabled = true;
            launchingSprite.GetComponent<SpriteRenderer>().enabled = false;
            state = State.NORMAL;
        }

        //Identifies DrEric, which is not a constant object. Checks every frame until found.
        if (DrEric == null)
        {
            if (state == State.GRABBING || state == State.GRABBED) //Releases grip if DrEric is destroyed while grabbed
                state = State.RELEASING;
            //error suprression
            try {
                DrEric = GameObject.Find(Names.PLAYERHOLDER).transform.Find(Names.DRERIC).gameObject;                                                                               //TODO: Remove magic string
                CalculateMaxSpeed();
            }
            catch
            {}
            if (DrEric != null) GetComponent<FollowObject>().followTarget = DrEric;
            else GetComponent<FollowObject>().followTarget = transform.parent.gameObject;
        }

        else
        {
            Vector2 drEricPos = new Vector2(DrEric.transform.position.x, DrEric.transform.position.y);

            //Check prevents launching while in Launcher, which should override standard movement
            
            AnimateSprite();
            if (DrEric != null && state == State.GRABBED)
            {
                Rotate(); //rotation around DrEric while grabbed
                if (Time.time >= grabTime + maxGrabTime)
                    Launch(initialVector); //release without applying force
            }
            if (Input.GetMouseButtonUp(0))
            {
                alreadyGrabbed = false;
                //if grabbing animation has concluded, launch
                if (state == State.GRABBED)
                    Launch(Input.mousePosition);
                //stop grabbing animation partway through
                else
                    state = State.RELEASING;
            }
        }
    }

    void OnMouseOver() {
        if (DrEric != null && !(DrEric.transform.parent.tag == "Launcher"))
        {
            //Grabs if not currently grabbing or grabbed, and if within range, when mouse is pressed
            if ((state == State.NORMAL || state == State.RELEASING) && Input.GetMouseButton(0) && IsGrabbable())
            {
                initialVector = Input.mousePosition; //Initial click point used for movement calculations
                AnimateGrab();
            }
            
        }
        
    }
	
    /*
     * Checks if the squid's grabbing point (bottom of fully-extended tentacles) is within grabRange of DrEric and the
     * maximum number of jumps has not been exceeded.
     *
     * @return true if DrEric is in range
     */
	private bool IsGrabbable() {
        if (Vector3.Distance(transform.position, DrEric.transform.position) <= grabRange && !alreadyGrabbed)
        {
            //if (jumpCount < maxJumps)
                return true;
        }
        return false;
	}
	
    /*
     * Begins squid grabbing animation
     * TODO: Will probably be changed or removed with proper animations
     */
	private void AnimateGrab() {
		idleSprite.GetComponent<SpriteRenderer> ().enabled = false;
		launchingSprite.GetComponent<SpriteRenderer> ().enabled = true;
        state = State.GRABBING;
	}

    private void GrabDrEric()
    {
        DrEric.GetComponent<Rigidbody2D>().gravityScale = 0;
        DrEric.GetComponent<Rigidbody2D>().angularVelocity = 0;
        DrEric.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        state = State.GRABBED;
        grabTime = Time.time;
        alreadyGrabbed = true;
    }

    /*
     * Manually switches sprites for grabbing and releasing animation. Modifies state.
     * TODO: Will be replaced with proper animations; will need to handle state modifications.
     */
    private void SetSpriteCounter()
    {
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, Input.mousePosition);
        float magnitude = deltaVector.magnitude;
        int numOfSprites = 6;
        for (int i = 1; i <= numOfSprites; i++)
        {
            if (magnitude >= (i * maxSpeed) / numOfSprites)
            {
                spriteCounter = i;
            }
        }
    }
    private void AnimateSprite()
    {
        SetSpriteCounter();
        if (state == State.GRABBING || state == State.GRABBED)
        {
            SpriteRenderer sprite = launchingSprite.GetComponent<SpriteRenderer>();
            switch (spriteCounter)
            {
                case 1:
                    sprite.sprite = launchSprite1;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, .64f, 0);
                    break;
                case 2:
                    sprite.sprite = launchSprite2;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, 1.13f, 0);
                    break;
                case 3:
                    sprite.sprite = launchSprite3;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, 1.62f, 0);
                    break;
                case 4:
                    sprite.sprite = launchSprite4;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, 2.41f, 0);
                    break;
                case 5:
                    sprite.sprite = launchSprite5;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, 3.39f, 0);
                    break;
                case 6:
                    sprite.sprite = launchSprite6;
                    launchingSprite.localPosition = new Vector3(launcherXOffset, 4.20f, 0);
                    break;
            }
            GrabDrEric();
        }
        
    }

    /*
     * Rotates squid around DrEric. Squid is opposite the direction of the deltaVector, with its head pointing in
     * the direction of travel. Quick orbit by linear interpolation determined by rotationSpeed.
     */
    private void Rotate()
    {
        initialVector = centerOfScreen; //TODO OPTIMIZE
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, Input.mousePosition); //TODO

        float angle = Mathf.Atan2(deltaVector.y, deltaVector.x) * Mathf.Rad2Deg + rotationOffset;
        float gravityOffset = Mathf.Atan2(Physics2D.gravity.y, Physics2D.gravity.x) * Mathf.Rad2Deg;
        destRotation = Quaternion.Euler(0, 0, angle + gravityOffset);

        if (transform.rotation != destRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, Time.deltaTime * rotationSpeed);
        }
    }
	
    /*
     * Squid releases DrEric with the direction and speed indicated by deltaVector. Upon release, squid will rotate to
     * face gravity again.
     * TODO: SquidLauncher should absorb FlingObject completely, not call its methods.
     *
     * @param finalVector the vector where the mouse is released
     */
	private void Launch(Vector2 finalVector)
    {
        DrEric.GetComponent<Rigidbody2D>().gravityScale = 1;
        DrEric.GetComponent<FlingObject>().Fling (deltaVector);
        state = State.RELEASING;
        jumpCount++;
	}

    /*
     * Determines dynamically if the player has collided with the ground, which will cause jumpCount to reset to 0
     *
     * TODO: Not currently implemented
     */
    void CheckGround()
    {
        throw new System.Exception("Not implemented");
    }

    public GameObject getDrEric()
    {
        return DrEric;
    }
}
