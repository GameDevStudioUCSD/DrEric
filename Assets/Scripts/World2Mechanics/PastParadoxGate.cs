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

	public void Open() {
		doorOpen.SetActive(true);
		doorClosed.SetActive(false);
		isOpen = true;

		counter.Decrement ();
	}

	public void Close() {
		doorOpen.SetActive(false);
		doorClosed.SetActive(true);
		isOpen = false;

		counter.Increment ();
	}
}
