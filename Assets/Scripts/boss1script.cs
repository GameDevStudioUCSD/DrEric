using UnityEngine;
using System.Collections;

public class boss1script : MonoBehaviour {

	public bool topSwitch, leftSwitch, rightSwitch, bottomSwitch;
	// Use this for initialization
	void Start () {
		topSwitch = false;
		leftSwitch = false;
		rightSwitch = false;
		bottomSwitch = false;
		PickASwitch();
	}
	
	// Update is called once per frame
	void Update () {
		if (topSwitch && leftSwitch && rightSwitch && bottomSwitch){
			Death();
		}

	}

	void Attack1(){

	}

	void Attack2() {

	}

	public void FlipRightSwitch(){
		rightSwitch = true;
	}
	public void FlipTopSwitch(){
		topSwitch = true;
	}
	public void FlipLeftSwitch(){
		leftSwitch = true;
	}
	public void FlipBottomSwitch(){
		bottomSwitch = true;
	}

	void Death() {

		Destroy(this.gameObject);
	}

	void PickASwitch() {
        ArrayList list = new ArrayList();
        if (!topSwitch) list.Add(transform.FindChild("TopSwitch"));
        if (!bottomSwitch) list.Add(transform.FindChild("BottomSwitch"));
        if (!rightSwitch) list.Add(transform.FindChild("LeftSwitch"));
        if (!leftSwitch) list.Add(transform.FindChild("RightSwitch"));
        
    }
}
