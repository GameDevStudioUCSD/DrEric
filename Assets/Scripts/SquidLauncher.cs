using UnityEngine;
using System.Collections;
/**
 * Filename: Launcher.cs \n
 * Author: Daniel Griffiths \n
 * Contributing Authors: None \n
 * Date Drafted: November 14, 2015 \n
 * Description: This script is used by the squid launcher, which is responsible
 *              for sending DrEric flying around the map.
 * 
 *              Preliminary/Incomplete
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

    void Start ()
    {
        gravity = 9.81f * Vector2.down;
        destRotation = transform.rotation;
        idleSprite = transform.Find ("Idle Sprite");
		launchingSprite = transform.Find ("Launching Sprite");
    }

	void Update () {
        if (!grabbed)
            CheckOrientation();
        if (DrEric == null)
        {
            DrEric = GameObject.Find("DrEric(Clone)");
        }
        else
        {
            moveToward();

            if (DrEric.transform.parent == null)
            {
                if (!grabbed && Input.GetMouseButton(0) && canGrab())
                {
                    initialVector = Input.mousePosition;
                    grab();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (grabbed)
                        launch();
                    else
                    {
                        grabbing = false;
                        releasing = true;
                    }
                }
            }
            animateSprite();
            if (grabbed)
            {
                rotate();
            }
        }
    }

    void CheckOrientation()
    {
        // rotates object in relation to gravity
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

    void moveToward() {
        transform.position = Vector3.MoveTowards(transform.position,
		                                         DrEric.transform.position,
		                                         movementSpeed * Time.deltaTime);
	}
	
	bool canGrab() {
        if (Vector3.Distance(transform.position, DrEric.transform.position) <= grabRange)
            return true;
        else
            return false;
	}
	
	void grab() {
		idleSprite.GetComponent<SpriteRenderer> ().enabled = false;
		launchingSprite.GetComponent<SpriteRenderer> ().enabled = true;
		grabbing = true;
	}

    void animateSprite()
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

    void rotate()
    {
        finalVector = Input.mousePosition;
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, finalVector);

        float angle = Mathf.Atan2(deltaVector.y, deltaVector.x) * Mathf.Rad2Deg;
        float gravityOffset = Mathf.Atan2(gravity.y, gravity.x) * Mathf.Rad2Deg;
        destRotation = Quaternion.Euler(0, 0, angle + gravityOffset);

        if (transform.rotation != destRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, destRotation, Time.deltaTime * 10);
        }
    }
	
	void launch()
    {
        finalVector = Input.mousePosition;
        deltaVector = DrEric.GetComponent<FlingObject>().CalculateDelta(initialVector, finalVector);
        DrEric.GetComponent<FlingObject>().Fling (deltaVector);
		grabbed = false;
		releasing = true;
	}
}
