using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {


    public GameObject dialogBox;
    public GameObject click;
    public GameObject drag;
    public SquidLauncher launcher;
    public float flashRate = 0.5f;
    enum State { Waiting, Click, Drag, Finished }
    State state = State.Waiting;
    float lastFlash;
    Transform dragT;
    Transform squidT;
    float dragX;
    float dragY;
    float dragZ;
    float savedPosMag;

	void Start () {
        lastFlash = Time.time;
        dragT = drag.GetComponent<Transform>();
        squidT = launcher.GetComponent<Transform>();
        savedPosMag = squidT.position.magnitude;
        dragX = dragT.position.x;
        dragY = dragT.position.y;
        dragZ = dragT.position.z;
	}
	
	void Update () {
        switch (state)
        {
            case State.Waiting:
                if (dialogBox.active == false)
                    state = State.Click;
                break;
            case State.Click:
                FlashObject(click);
                if( launcher.state == SquidLauncher.State.GRABBED)
                {
                    click.SetActive(false);
                    drag.SetActive(true);
                    state = State.Drag;
                }
                break;
            case State.Drag:
                dragT.position = new Vector3(dragX, (( dragY) % 300) + 100, dragZ);
                dragY += 3;
                if (savedPosMag - squidT.position.magnitude < -10 || savedPosMag - squidT.position.magnitude > 2)
                {
                    drag.SetActive(false);
                    enabled = false;
                }
                break;
        }
	}

    void FlashObject(GameObject obj)
    {
        if(Time.time - lastFlash > flashRate )
        {
            obj.SetActive(!obj.active);
            lastFlash = Time.time;
        }
    }
}
