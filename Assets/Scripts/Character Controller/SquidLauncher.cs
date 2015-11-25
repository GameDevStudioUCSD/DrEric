using UnityEngine;
using System.Collections;
/**
 * Filename: Launcher.cs \n
 * Author: Daniel Griffiths \n
 * Contributing Authors: None \n
 * Date Drafted: November 14, 2015 \n
 * Description: This script is used by the squid launcher, which is responsible
 *              for sending DrEric flying around the map.
 */
public class SquidLauncher : MonoBehaviour {
	public float movementSpeed = 10;
    public float grabRange = 2;
    public float rotationSpeed = 5;

    private GameObject DrEric = null;
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
	private Vector2 finalVector;
	private Vector2 deltaVector;

    private enum State {NORMAL, GRABBING, GRABBED, RELEASING};
    private State state = State.NORMAL;
	private int spriteCounter = 0;

    private Vector2 gravity;
    private Quaternion destRotation;

    /*
     * Initialization. Identifies sprites for manual animations and initializes physics.
     */
    void Start ()
    {
        gravity = 9.81f * Vector2.down;
        destRotation = transform.rotation;
        idleSprite = transform.Find ("Idle Sprite");
		launchingSprite = transform.Find ("Launching Sprite");
    }

    void Update() {
        //Unless the squid is currently grabbing DrEric, it should orient itself to gravity.
        if (state != State.GRABBED)
            CheckOrientation();

        //Identifies DrEric, which is not a constant object. Checks every frame until found.
        if (DrEric == null)
        {
            if (state == State.GRABBING || state == State.GRABBED) //Releases grip if DrEric is destroyed while grabbed
                state = State.RELEASING;
            DrEric = GameObject.Find("DrEric(Clone)"); //TODO: Remove magic string
        }
        else
        {
            moveToward(); //Squid always seeks DrEric

            //Check prevents launching while in Launcher or other objects which should override standard movement
            if (DrEric.transform.parent == null)
            {
                //Grabs if not currently grabbing or grabbed, and if within range, when mouse is pressed
                if ((state == State.NORMAL || state == State.RELEASING) && Input.GetMouseButton(0) && canGrab())
                {
                    initialVector = Input.mousePosition; //Initial click point used for movement calculations
                    reach();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //if grabbing animation has concluded, launch
                    if (state == State.GRABBED)
                        launch();
                    //stop grabbing animation partway through
                    else
                        state = State.RELEASING;
                }
            }
            animateSprite();
            if (DrEric != null && state == State.GRABBED)
            {
                rotate(); //rotation around DrEric while grabbed
            }
        }
    }

    /*
     * Rotates squid to face the direction of gravity by linear interpolation.
     */
    private void CheckOrientation()
    {
        //Rotates object in relation to gravity
        gravity = Physics2D.gravity;
        float x = gravity.x;
        float y = gravity.y;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg * -1;
        destRotation = Quaternion.Euler(0, 0, 90 - angle);
        if (transform.rotation != destRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, Time.deltaTime * rotationSpeed);
        }
    }
    /*
     * Constant movement toward DrEric. MoveTowards is similar to linear interpolation, but uses a constant speed.
     * Done in two dimensions so the SquidLauncher remains on top.
     */
    private void moveToward() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 ericPosition = new Vector2(DrEric.transform.position.x, DrEric.transform.position.y);

        transform.position = Vector2.MoveTowards(position,
		                                         ericPosition,
		                                         movementSpeed * Time.deltaTime);
	}
	
    /*
     * Checks if the squid's grabbing point (bottom of fully-extended tentacles) is within grabRange of DrEric
     *
     * @return true if DrEric is in range
     */
	private bool canGrab() {
        if (Vector3.Distance(transform.position, DrEric.transform.position) <= grabRange)
            return true;
        else
            return false;
	}
	
    /*
     * Begins squid grabbing animation
     * TODO: Will probably be changed or removed with proper animations
     */
	private void reach() {
		idleSprite.GetComponent<SpriteRenderer> ().enabled = false;
		launchingSprite.GetComponent<SpriteRenderer> ().enabled = true;
        state = State.GRABBING;
	}

    private void grab()
    {
        DrEric.GetComponent<Rigidbody2D>().gravityScale = 0;
        DrEric.GetComponent<Rigidbody2D>().angularVelocity = 0;
        DrEric.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        state = State.GRABBED;
    }

    /*
     * Manually switches sprites for grabbing and releasing animation. Modifies state.
     * TODO: Will be replaced with proper animations; will need to handle state modifications.
     */
    private void animateSprite()
    {
        if (state == State.GRABBING)
        {
            SpriteRenderer sprite = launchingSprite.GetComponent<SpriteRenderer>();
            if (spriteCounter == 0)
            {
                sprite.sprite = launchSprite1;
                spriteCounter++;
            }
            else if (spriteCounter == 1)
            {
                sprite.sprite = launchSprite2;
                spriteCounter++;
            }
            else if (spriteCounter == 2)
            {
                sprite.sprite = launchSprite3;
                spriteCounter++;
            }
            else if (spriteCounter == 3)
            {
                sprite.sprite = launchSprite4;
                spriteCounter++;
            }
            else if (spriteCounter == 4)
            {
                sprite.sprite = launchSprite5;
                spriteCounter++;
            }
            else if (spriteCounter == 5)
            {
                sprite.sprite = launchSprite6;
                spriteCounter++;
            }
            else if (spriteCounter == 6)
            {
                grab();
            }
        }
        if (state == State.RELEASING)
        {
            SpriteRenderer sprite = launchingSprite.GetComponent<SpriteRenderer>();
            if (spriteCounter == 6)
            {
                sprite.sprite = launchSprite5;
                spriteCounter--;
            }
            else if (spriteCounter == 5)
            {
                sprite.sprite = launchSprite4;
                spriteCounter--;
            }
            else if (spriteCounter == 4)
            {
                sprite.sprite = launchSprite3;
                spriteCounter--;
            }
            else if (spriteCounter == 3)
            {
                sprite.sprite = launchSprite2;
                spriteCounter--;
            }
            else if (spriteCounter == 2)
            {
                sprite.sprite = launchSprite1;
                spriteCounter--;
            }
            else if (spriteCounter == 1)
            {
                sprite.sprite = launchSprite0;
                spriteCounter--;
            }
            else if (spriteCounter == 0)
            {
                launchingSprite.GetComponent<SpriteRenderer>().enabled = false;
                idleSprite.GetComponent<SpriteRenderer>().enabled = true;
                state = State.NORMAL;
            }
        }
    }

    /*
     * Rotates squid around DrEric. Squid is opposite the direction of the deltaVector, with its head pointing in
     * the direction of travel. Quick orbit by linear interpolation determined by rotationSpeed.
     */
    private void rotate()
    {
        finalVector = Input.mousePosition;
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, finalVector); //TODO

        float angle = Mathf.Atan2(deltaVector.y, deltaVector.x) * Mathf.Rad2Deg;
        float gravityOffset = Mathf.Atan2(gravity.y, gravity.x) * Mathf.Rad2Deg;
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
     */
	private void launch()
    {
        DrEric.GetComponent<Rigidbody2D>().gravityScale = 1;
        finalVector = Input.mousePosition;
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, finalVector);
        DrEric.GetComponent<FlingObject>().Fling (deltaVector);
        state = State.RELEASING;
	}
}
