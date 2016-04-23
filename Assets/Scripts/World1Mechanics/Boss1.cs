using UnityEngine;
using System.Collections;

/* James Allen
 * Boss 1 Script: code for boss and interaction with switches
 * nov 20ish
 */
public class Boss1 : MonoBehaviour
{
    public enum Direction { DOWN, UP, LEFT, RIGHT }
    enum STATE { LERPING_AWAY, LERPING_BACK, IDLE, FIRELASER }
    public enum GRAVITY { RIGHT, UP, DOWN, LEFT }

    //check if switches have been pressed
    public bool topSwitch, leftSwitch, rightSwitch, bottomSwitch;
    public float moveFactor = 3f;
    private Switch activeSwitch;


    private bool needToPickASwitch;
    private string TOPSWITCH = "TopSwitch";
    private string BOTTOMSWITCH = "BottomSwitch";
    private string RIGHTSWITCH = "RightSwitch";
    private string LEFTSWITCH = "LeftSwitch";
    public LaserCannon topCannon, bottomCannon;
    public GameObject topSprite, botSprite, leftSprite, rightSprite;
    public VictoryController victorycontroller;
    public AudioClip hurtSound;


    private RhythmController rhythmController;
    private float startTime;
    private SpriteRenderer spriterenderer;
    private BoxCollider boxcollider2d;
    private float endTime = 3;
    private Direction currentDirection;
    private STATE state;
    private Vector2 startVector = Vector2.down;//default value
    private Vector2 endVector = Vector2.down;
    private int lasercounter = 0;//makes lazer cycle between top and bottom.
    public GRAVITY gravity;
    public GameObject RightGummyBears;
    public GameObject LeftGummyBears;
    public GameObject UpGummyBears;
    public GameObject DownGummyBears;
    private Switch currentSwitch = null;

    // Use this for initialization
    void Start()
    {
        topSwitch = false;
        leftSwitch = false;
        rightSwitch = false;
        bottomSwitch = false;
        needToPickASwitch = true;
        state = STATE.IDLE;
        startVector = transform.position;
        startTime = Time.time - 1;
        victorycontroller.GetComponentInParent<SpriteRenderer>().enabled = false;
        victorycontroller.GetComponent<CircleCollider2D>().enabled = false;
        gravity = GRAVITY.DOWN;
        rhythmController = RhythmController.GetController();
    }

    public void respawn()
    {
        topSwitch = false;
        leftSwitch = false;
        rightSwitch = false;
        bottomSwitch = false;
        needToPickASwitch = true;
        state = STATE.IDLE;
        transform.position = startVector;
        victorycontroller.GetComponentInParent<SpriteRenderer>().enabled = false;
        victorycontroller.GetComponent<BoxCollider2D>().enabled = false;
        gravity = GRAVITY.DOWN;
    }

    // Update is called once per frame
    void Update()
    {//right now it just lerps in a random direction every 2 seconds
        if (needToPickASwitch) PickASwitch();//neccesary because pickaswitch cannot be called in start
        if (state == STATE.LERPING_BACK || state == STATE.LERPING_AWAY) Lerp();
        if (state == STATE.IDLE && Time.time - startTime > endTime)
        {
            BodySlam((Direction)Random.Range(0, 4 ));
        }


        if(GetNumberOfSwitchesHit() == 3 )
        {
            FireLasers();
        }
        if (Physics2D.gravity == new Vector2(0, 9.8f))
        {
            gravity = GRAVITY.UP;
        }
        else if (Physics2D.gravity == new Vector2(0, -9.8f))
        {
            gravity = GRAVITY.DOWN;
        }
        else if (Physics2D.gravity == new Vector2(9.8f, 0))
        {
            gravity = GRAVITY.RIGHT;
        }
        else if (Physics2D.gravity == new Vector2(-9.8f, 0))
        {
            gravity = GRAVITY.LEFT;
        }

        string Switchtag = activeSwitch.gameObject.name;
        //if (Switchtag == TOPSWITCH)  Debug.Log("TOPSWITHVC" + Physics2D.gravity + gravity);
        // Gummy bear logic
        if (Switchtag == TOPSWITCH && gravity != GRAVITY.UP)
        { UpGummyBears.SetActive(true); }
        else { UpGummyBears.SetActive(false); }
        if (Switchtag == BOTTOMSWITCH && gravity != GRAVITY.DOWN)
        { DownGummyBears.SetActive(true); }
        else { DownGummyBears.SetActive(false); }
        if (Switchtag == RIGHTSWITCH && gravity != GRAVITY.RIGHT)
        { RightGummyBears.SetActive(true); }
        else { RightGummyBears.SetActive(false); }
        if (Switchtag == LEFTSWITCH && gravity != GRAVITY.LEFT)
        { LeftGummyBears.SetActive(true); }
        else { LeftGummyBears.SetActive(false); }

        if (Switchtag == TOPSWITCH) topSprite.SetActive(true);
        if (Switchtag == BOTTOMSWITCH) botSprite.SetActive(true);
        if (Switchtag == RIGHTSWITCH) rightSprite.SetActive(true);
        if (Switchtag == LEFTSWITCH) leftSprite.SetActive(true);

    }

    void DeactivateAllSwitches()
    {
        topSprite.SetActive(false);
        botSprite.SetActive(false);
        leftSprite.SetActive(false);
        rightSprite.SetActive(false);
    }
    void Lerp()//lerp function
    {
        if (state == STATE.LERPING_AWAY)//if boss is going away from center
        {
            if (Time.time - startTime < endTime)//lerp
            {
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
                //Debug.Log(Lerpval);
                transform.position = Vector2.Lerp(startVector, endVector, Lerpval);
            }
            else
            {//once boss is done going away, it lerps towards center
                startTime = Time.time;
                state = STATE.LERPING_BACK;
            }
        }
        else if (state == STATE.LERPING_BACK)//when boss is lerping to center
        {
            if (Time.time - startTime < endTime)
            {
                //Debug.Log(Time.time - startTime);
                float Lerpval = (Time.time - startTime) / endTime;
                transform.position = Vector2.Lerp(endVector, startVector, Lerpval);
            }
            else
            {//idle once done going to center
                startTime = Time.time;
                state = STATE.IDLE;
            }
        }
    }

    void BodySlam(Direction direction)
    {//initializes lerp and other variables
        startTime = Time.time;
        float moveFactor = this.moveFactor * (1+ .5f * GetNumberOfSwitchesHit());
        Vector2 tempVector = Vector2.down;
        if (direction == Direction.DOWN)
        {
            currentDirection = Direction.DOWN;
            tempVector = Vector2.down * moveFactor;
        }
        if (direction == Direction.UP)
        {
            currentDirection = Direction.UP;
            tempVector = Vector2.up * moveFactor;
        }
        if (direction == Direction.LEFT)
        {
            currentDirection = Direction.LEFT;
            tempVector = Vector2.left * moveFactor;
        }
        if (direction == Direction.RIGHT)
        {
            currentDirection = Direction.RIGHT;
            tempVector = Vector2.right * moveFactor;
        }
        //set end vector based on direction
        endVector = startVector + tempVector;
        state = STATE.LERPING_AWAY;

    }

    void FireLasers()
    {
        if (lasercounter % 2 == 0)
        {
            if (!topCannon.firing )
            {
                topCannon.Fire(GetNumberOfSwitchesHit());
            }
        }
        else
        {
            if (!bottomCannon.firing )
            {
                bottomCannon.Fire(GetNumberOfSwitchesHit());
            }
        }
        lasercounter++;
    }

    //methods called when switch is pressed
    public void FlipRightSwitch()
    {
        rightSwitch = true;
        currentSwitch = transform.FindChild(RIGHTSWITCH).gameObject.GetComponent<Switch>();
        currentSwitch.isEnabled = false;
        FireLasers();
        PickASwitch();
    }
    public void FlipTopSwitch()
    {
        topSwitch = true;
        currentSwitch = transform.FindChild(TOPSWITCH).gameObject.GetComponent<Switch>();
        currentSwitch.isEnabled = false;
        FireLasers();
        PickASwitch();
    }
    public void FlipLeftSwitch()
    {
        leftSwitch = true;
        currentSwitch = transform.FindChild(LEFTSWITCH).gameObject.GetComponent<Switch>();
        currentSwitch.isEnabled = false;
        FireLasers();
        PickASwitch();
    }
    public void FlipBottomSwitch()
    {
        bottomSwitch = true;
        currentSwitch = transform.FindChild(BOTTOMSWITCH).gameObject.GetComponent<Switch>();
        currentSwitch.isEnabled = false;
        FireLasers();
        PickASwitch();
    }

    void Death()
    {
        victorycontroller.GetComponentInParent<SpriteRenderer>().enabled = true;
        victorycontroller.GetComponent<BoxCollider2D>().enabled = true;
        rhythmController.StopSong();
        GameObject deathObj = new GameObject("DeathNoise");
        AudioSource deathSource = deathObj.AddComponent<AudioSource>();
        deathSource.clip = GetComponent<AudioSource>().clip;
        deathSource.Play();
        Physics2D.gravity = Vector2.down;
        Physics2D.gravity = Vector2.zero;
        Destroy(this.gameObject);
    }

    void PickASwitch()
    {
        if(this.currentSwitch != null)
            this.currentSwitch.gameObject.SetActive(false);
        //check for death
        if (topSwitch && leftSwitch && rightSwitch && bottomSwitch)
        {
            Death();
        }
        if (GetNumberOfSwitchesHit() == 0)
            rhythmController.PlaySong(5);
        if (GetNumberOfSwitchesHit() < 3 && GetNumberOfSwitchesHit() > 0)
            GetComponent<AudioSource>().PlayOneShot(hurtSound);
        if (GetNumberOfSwitchesHit() == 2)
            rhythmController.SwitchToChannel(2);
        // Check if should roar
        if (GetNumberOfSwitchesHit() == 3)
        {
            rhythmController.PlaySong(6);
            GetComponent<AudioSource>().Play();
        }
        //assign all unpressed switches to list
        ArrayList switchList = new ArrayList();
        if (!topSwitch) switchList.Add(transform.FindChild(TOPSWITCH));
        if (!bottomSwitch) switchList.Add(transform.FindChild(BOTTOMSWITCH));
        if (!rightSwitch) switchList.Add(transform.FindChild(RIGHTSWITCH));
        if (!leftSwitch) switchList.Add(transform.FindChild(LEFTSWITCH));
        //disable all the switches and make them visible
        for (int i = 0; i < switchList.Count; i++)
        {
            Transform currenttransform = (Transform)switchList[i];
            Switch currentswitch = currenttransform.gameObject.GetComponent<Switch>();
            //Debug.Log(currenttransform.gameObject);
            currentswitch.isEnabled = false;
            spriterenderer = currentswitch.GetComponentInChildren<SpriteRenderer>();
            spriterenderer.enabled = false;
            boxcollider2d = currentswitch.GetComponentInChildren<BoxCollider>();
            boxcollider2d.enabled = false;
        }
        //choose a switch at random 
        int size = switchList.Count;
        if (size == 0) return;
        int index = Random.Range(0, size);

        //select and enable a switch
        DeactivateAllSwitches();
        Transform currentTransform = (Transform)switchList[index];
        Switch currentSwitch = currentTransform.gameObject.GetComponent<Switch>();
        currentSwitch.gameObject.SetActive(true);
        Debug.Log(currentTransform.gameObject + " ENABLED");
        currentSwitch.isEnabled = true;
        //make 
        for (int i = 0; i < switchList.Count; i++)
        {
            Transform currenttransform2 = (Transform)switchList[i];
            Switch currentswitch2 = currenttransform2.gameObject.GetComponent<Switch>();
            //Debug.Log(currenttransform.gameObject);
            if (currentswitch2 == currentSwitch)
            {
                spriterenderer = currentswitch2.GetComponentInChildren<SpriteRenderer>();
                spriterenderer.enabled = true;
                boxcollider2d = currentswitch2.GetComponentInChildren<BoxCollider>();
                boxcollider2d.enabled = true;
                activeSwitch = currentswitch2;
            }
        }

        needToPickASwitch = false;
    }


    int GetNumberOfSwitchesHit()
    {
        int retVal = 0;
        if (leftSwitch)
            retVal++;
        if (rightSwitch)
            retVal++;
        if (topSwitch)
            retVal++;
        if (bottomSwitch)
            retVal++;
        return retVal;
    }
}
