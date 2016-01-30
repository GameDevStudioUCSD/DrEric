using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

	public GameObject go;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Player")
			Rotate ();
	}

	void Rotate() {
		Quaternion from = Quaternion.Euler (0, 0, 1);
		Quaternion to = Quaternion.Euler (0, 0, -1);
		go.transform.rotation = Quaternion.Lerp(from, to, 1.0f);

	}

}
