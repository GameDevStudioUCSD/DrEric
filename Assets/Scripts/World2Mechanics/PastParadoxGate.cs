using UnityEngine;
using System.Collections;

public class PastParadoxGate : Triggerable {
	
	public bool isOpen;
	//public float speed = .01f;
	//float isMoving;
	//float height;
	public ParadoxCounter counter;
	public GameObject doorClosed;
	public GameObject doorOpen;

	// Use this for initialization
	void Start () {
		if (isOpen)
		{
			doorOpen.SetActive(true);
			doorClosed.SetActive(false);
			isOpen = true;
		}
		else
		{
			doorOpen.SetActive(false);
			doorClosed.SetActive(true);
			isOpen = false;
		}
	}

	public override void Trigger() {
		if (isOpen) {
			Close ();
		} else {
			Open ();
		}
	}

	public override void Trigger(bool b) {
		if (b) {
			Open ();
		} else {
			Close ();
		}
	}

	public void Open() {
		if (!isOpen)
			counter.Decrement ();
		doorOpen.SetActive(true);
		doorClosed.SetActive(false);
		isOpen = true;


	}

	public void Close() {
		if (isOpen)
			counter.Increment ();
		doorOpen.SetActive(false);
		doorClosed.SetActive(true);
		isOpen = false;
	}
}
