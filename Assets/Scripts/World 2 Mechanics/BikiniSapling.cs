using UnityEngine;
using System.Collections;

public class BikiniSapling : MonoBehaviour {

	public BikiniTree presentTree;
	public bool saplingAlive;
	private float prevX;

	// Use this for initialization
	void Start () {
		prevX = this.transform.position.y;
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
}
