using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {

    private RespawnController respawner;
    /** State definitions for Platform.cs */
    enum STATE {WAITING, SEARCHING, BLOATING, FIRING, RETURNING }
    /** This is the starting vector of this game object*/
    public Vector2 startVector = new Vector2(1, 1);
    /** This is the end vector of the object's path */
    public Vector2 endVector = new Vector2(2, 1);
    /** This is the amount of time in seconds that it takes the game object to 
      * move from startVector to endVector */
    [Tooltip("This is the amount of time in seconds that it takes the game object to move from startVector to endVector")]
    public float movementTime = 2;

    // Private variables
    private float startTime = 0;
    private State state = State.WAITING;
    private RhythmController rhythmController;

    void Start()
    {
        respawner = GameObject.Find("Respawner").GetComponent<RespawnController>();
        rhythmController = RhythmController.GetController();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (state = STATE.FIRING)
        {
            if (other.CompareTag("Player"))
            {
                respawner.kill();
            }
        }
    }
    void Update()
    {
        MoveDrEric();
        switch (state)
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.SEARCHING:
                Lerping();
                break;
            case STATE.BLOATING:
                break;
            case STATE.FIRING:
                break;
            case STATE.RETURNING:
                break;
        }
    }


    /** This method uses the lerp function to smoothly move the platform 
     *  between the startVector and endVector in movementTime seconds*/
    void Lerping()
    {
        if (Time.time - startTime > movementTime / rhythmController.GetPitch())
        {
            startTime = Time.time;
            Vector2 swapVector = startVector;
            startVector = endVector;
            endVector = swapVector;
            state = State.WAITING;
        }
        else
        {
            float lerpVal = (Time.time - startTime) / (movementTime / rhythmController.GetPitch());
            transform.position = Vector2.Lerp(startVector, endVector, lerpVal);
        }
    }
}
