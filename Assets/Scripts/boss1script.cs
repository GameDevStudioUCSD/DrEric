using UnityEngine;
using System.Collections;

public class boss1script : MonoBehaviour {
    public enum Direction { DOWN, UP, LEFT, RIGHT}
	public bool topSwitch, leftSwitch, rightSwitch, bottomSwitch;
    private bool needToPickASwitch;
	// Use this for initialization
	void Start () {
		topSwitch = false;
		leftSwitch = false;
		rightSwitch = false;
		bottomSwitch = false;
        needToPickASwitch = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!needToPickASwitch)
        {
            PickASwitch();
        }
    }

	void BodySlam(Direction direction){
        if (direction == Direction.DOWN)
        {
            for (int x = 0; x < 100; x++)
            {
                transform.position = transform.position + Vector3.down;
            }
            for (int x = 0; x < 100; x++)
            {
                transform.position = transform.position - Vector3.down;
            }
        }
	}

	void Attack2() {

	}

	public void FlipRightSwitch(){
		rightSwitch = true;
        SwitchScript currentswitch = transform.FindChild("RightSwitch").gameObject.GetComponent<SwitchScript>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipTopSwitch(){
		topSwitch = true;
        SwitchScript currentswitch = transform.FindChild("TopSwitch").gameObject.GetComponent<SwitchScript>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipLeftSwitch(){
		leftSwitch = true;
        SwitchScript currentswitch = transform.FindChild("LeftSwitch").gameObject.GetComponent<SwitchScript>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }
	public void FlipBottomSwitch(){
		bottomSwitch = true;
        SwitchScript currentswitch = transform.FindChild("BottomSwitch").gameObject.GetComponent<SwitchScript>();
        currentswitch.isEnabled = false;
        PickASwitch();
    }

	void Death() {
		Destroy(this.gameObject);
	}

	void PickASwitch()
    {

        //check for death
        if (topSwitch && leftSwitch && rightSwitch && bottomSwitch)
        {
            Death();
        }
        //assign all unpressed switches to list
        ArrayList list = new ArrayList();
        if (!topSwitch) list.Add(transform.FindChild("TopSwitch"));
        if (!bottomSwitch) list.Add(transform.FindChild("BottomSwitch"));
        if (!rightSwitch) list.Add(transform.FindChild("LeftSwitch"));
        if (!leftSwitch) list.Add(transform.FindChild("RightSwitch"));
        //disable all the switches
        for (int forloopindex = 0; forloopindex < list.Count; forloopindex++)
        {
            Transform currenttransform = (Transform)list[forloopindex];
            SwitchScript currentswitch =  currenttransform.gameObject.GetComponent<SwitchScript>();
            Debug.Log(currenttransform.gameObject);
            currentswitch.isEnabled = false;
        }

        //choose a switch at random 
        int size = list.Count;
        int index = Random.Range(0, size);

        //select and enable a switch
        Transform currentTransform = (Transform)list[index];
        SwitchScript currentSwitch = currentTransform.gameObject.GetComponent<SwitchScript>();
        Debug.Log(currentTransform.gameObject + " ENABLED");
        currentSwitch.isEnabled = true;

        needToPickASwitch = true;
    }
}
