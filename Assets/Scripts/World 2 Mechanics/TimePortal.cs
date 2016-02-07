using UnityEngine;
using System.Collections;

public class TimePortal : MonoBehaviour {
	public GameObject Destination;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject);
		if (other.tag == "Player" )
		{
			other.transform.parent.position = Destination.transform.position;

		}
	}
}
