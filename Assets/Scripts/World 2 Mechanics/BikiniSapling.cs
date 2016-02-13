using UnityEngine;
using System.Collections;

public class BikiniSapling : MonoBehaviour {

	public BikiniTree presentTree;
	public bool saplingHydrated = false;
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
		this.GetComponent<BoxCollider2D>().enabled = false;
		this.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void HydrateSapling()
	{
		this.GetComponent<BoxCollider2D>().enabled = true;
		this.GetComponent<Rigidbody2D>().isKinematic = false;
	}
}
