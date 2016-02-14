using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	int hitCount = 0;
	public int hitsBeforeRetreat;
	private enum State {IDLE, AGGRAVATED, RETREAT};
	private State state;
	public float maxTimeAggravated = 15f;
	public DialogBox dialog;
	float timeCounter;
    Vector3 startingposition;
	
	int aggravateFrameCounter = 0;
	
	// Use this for initialization
	void Start () {
		state = State.IDLE;
        startingposition = transform.position;
	}
	
	void Update () {
		if (state == State.IDLE)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 0);
		}
		else if (state == State.AGGRAVATED)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 1);
			if ((Time.time - timeCounter) > maxTimeAggravated)
			{
				dialog.SetText("I'm calming down now. Don't hit me again.");
				dialog.gameObject.SetActive(true);
				state = State.IDLE;
			}
		}
		else if (state == State.RETREAT)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 2);
			this.GetComponent<Platform>().enabled = true;
            Platform platform = GetComponent<Platform>();
            if (transform.position != platform.endVector) platform.state = Platform.State.LERPING;
		}
	}

    public void reset()
    {
        state = State.IDLE;
        hitCount = 0;
        transform.position = startingposition;
        this.GetComponent<Platform>().state = Platform.State.WAITING;
        this.GetComponent<Platform>().enabled = false;
    }
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (state == State.IDLE) {
			hitCount++;
			switch (hitCount)
			{
                case 1:
                    dialog.SetText("Ouch! That hurt! You better not hit me (2) more times!");
                break;
				case 2:
				dialog.SetText("Stop that! You better not hit me (1) more time(s)!");
				break;
				
				case 3:
				dialog.SetText("That's it! You've hit me a total of (3) times! I'm leaving!");
				break;
			}
			dialog.gameObject.SetActive(true);
			timeCounter = Time.time;
			if (hitCount < hitsBeforeRetreat)
			{
				state = State.AGGRAVATED;
			}
			else
			{
				state = State.RETREAT;
			}
		}
	}
	
}