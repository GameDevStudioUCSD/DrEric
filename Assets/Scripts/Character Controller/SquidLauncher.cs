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

	private bool grabbing = false;
	private bool grabbed = false;
	private bool releasing = false;
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

	void Update () {
        //Unless the squid is currently grabbing DrEric, it should orient itself to gravity.
        if (!grabbed)
            CheckOrientation();

        //Identifies DrEric, which is not a constant object. Checks every frame until found.
        if (DrEric == null)
        {
            grabbing = false;
            grabbed = false;
            releasing = true;
            DrEric = GameObject.Find(Names.DRERIC);
        }
        else
        {
            moveToward(); //Squid always seeks DrEric

            //Check prevents launching while in Launcher or other objects which should override standard movement
            if (DrEric.transform.parent == null)
            {
                if (!grabbing && !grabbed && Input.GetMouseButton(0) && canGrab()) //Grabs anytime mouse is pressed while within range
                {
                    initialVector = Input.mousePosition; //Initial click point used for movement calculations
                    grab();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //if grabbing animation has concluded, launch
                    if (grabbed)
                        launch();
                    //stop grabbing animation partway through
                    else
                    {
                        grabbing = false;
                        releasing = true;
                    }
                }
            }
            animateSprite();
            if (DrEric != null && grabbed)
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
	private void grab() {
		idleSprite.GetComponent<SpriteRenderer> ().enabled = false;
		launchingSprite.GetComponent<SpriteRenderer> ().enabled = true;
		grabbing = true;
	}

    /*
     * Manually switches sprites for grabbing and releasing animation. Toggles grab variable.
     * TODO: Will be replaced with proper animations; will need to handle grabbed bool
     */
    private void animateSprite()
    {
        if (grabbing)
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
                grabbing = false;
                grabbed = true;
            }
        }
        if (releasing)
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
                releasing = false;
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
        finalVector = Input.mousePosition;
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, finalVector);
        DrEric.GetComponent<FlingObject>().Fling (deltaVector);
		grabbed = false;
		releasing = true;
	}
}
