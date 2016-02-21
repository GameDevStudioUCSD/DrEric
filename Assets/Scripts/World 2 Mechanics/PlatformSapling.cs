﻿using UnityEngine;
using System.Collections;

public class PlatformSapling : MonoBehaviour {

	public PlatformTree presentTree;
	public bool saplingHydrated = false;
	public bool playerOnTop = false;
	private float prevX;
    private float prevY;

	public float height;

	// Use this for initialization
	void Start () {
		prevX = this.transform.position.x;
        prevY = this.transform.position.y;
		if (saplingHydrated)
			HydrateSapling();
		else
			DehydrateSapling();
	}
	
	// Update is called once per frame
	void Update () {
		if (presentTree != null /*&& presentTree.treeAlive*/)
		{
			float currX = this.transform.position.x;
            float currY = this.transform.position.y;
			presentTree.shiftX(currX - prevX);
            presentTree.shiftY(currY - prevY);
			prevX = currX;
            prevY = currY;
		}
	}

	void DehydrateSapling()
	{
		if (presentTree != null)
			presentTree.killTree();
        this.GetComponent<Rigidbody2D>().constraints =
            RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void HydrateSapling()
	{
		if (presentTree != null)
			presentTree.plantTree();
		this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.GetComponent<Rigidbody2D>().constraints =
            RigidbodyConstraints2D.FreezePositionY
            | RigidbodyConstraints2D.FreezeRotation;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Water") {
			HydrateSapling();
		}

		if (other.tag == "Player") {
			playerOnTop = true;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Water") {
			HydrateSapling();
		}

		if (other.tag == "Player") {
			playerOnTop = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Water") {
			DehydrateSapling();
		}

		if (other.tag == "Player") {
			playerOnTop = false;
		}
	}
}