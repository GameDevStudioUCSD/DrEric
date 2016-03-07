using UnityEngine;
using System.Collections;

public class PastParadoxGate : MonoBehaviour {
	
	public bool isOpen;
	public float speed = .01f;
	float isMoving;
	float height;
	public ParadoxCounter counter;

	// Use this for initialization
	void Start () {
		if (isOpen) {
			height = 0;
		} else {
			height = 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving != 0) {
			transform.position += Vector3.up * speed * isMoving;
			height += speed * isMoving;
			if (height <= 0.0) {
				height = 0;
				isMoving = 0;
				isOpen = true;
			} else if (height >= 5.0) {
				height = 5;
				isMoving = 0;
				isOpen = false;
			}
		}
	}

	public void Move() {
		Debug.Log ("Sup");
		if (isOpen) {
			Close ();
		} else {
			Open ();
		}
	}

	public void Open() {
		isMoving = -1;
		counter.Decrement ();
	}

	public void Close() {
		isMoving = 1;
		counter.Increment ();
	}
}
