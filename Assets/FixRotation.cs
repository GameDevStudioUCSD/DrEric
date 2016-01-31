using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

	public GameObject go;

	bool rotating = false;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Player")
			rotating = true;
	}

	void Update()
	{
		if (rotating)
			Rotate ();
	}

	void Rotate() {
		go.transform.rotation = Quaternion.Slerp (go.transform.rotation, Quaternion.Euler(go.transform.rotation.x,
													go.transform.rotation.y, -90f), Time.deltaTime / 0.1f);

	}

}
