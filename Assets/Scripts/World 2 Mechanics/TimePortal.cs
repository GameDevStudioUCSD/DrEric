using UnityEngine;
using System.Collections;

public class TimePortal : MonoBehaviour {
	public GameObject destination;
	public BikiniSapling platform;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" )
		{
			if (platform.playerOnTop == true)
			{
				float yPos = platform.presentTree.transform.position.y;
			}
			other.transform.parent.position = destination.transform.position;

		}
	}
}
