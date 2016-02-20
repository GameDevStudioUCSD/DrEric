using UnityEngine;
using System.Collections;

public class BikiniSapling : MonoBehaviour {

	public BikiniTree presentTree;
	public bool saplingHydrated = false;
	public bool playerOnTop = false;
	private float prevX;

	// Use this for initialization
	void Start () {
		prevX = this.transform.position.x;
		if (saplingHydrated)
			HydrateSapling();
		else
			DehydrateSapling();
	}
	
	// Update is called once per frame
	void Update () {
		if (presentTree != null && presentTree.treeAlive)
		{
			float currX = this.transform.position.x;
			presentTree.shiftX(currX - prevX);
			prevX = currX;
		}
	}

	void DehydrateSapling()
	{
		if (presentTree != null)
			presentTree.killTree();
		this.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void HydrateSapling()
	{
		if (presentTree != null)
			presentTree.plantTree();
		this.GetComponent<Rigidbody2D>().isKinematic = false;
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
