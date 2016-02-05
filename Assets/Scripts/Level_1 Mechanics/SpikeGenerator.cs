using UnityEngine;
using System.Collections;

public class SpikeGenerator : MonoBehaviour {
	
	public GameObject spike;

	// Use this for initialization
	void Start () {
		Vector3 size = Vector3.Scale (this.transform.localScale, gameObject.GetComponent<BoxCollider2D>().size);
		Vector3 spikeSize = Vector3.Scale (this.spike.transform.localScale, this.spike.gameObject.GetComponent<BoxCollider2D>().size);
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;
		float spikeWidth = spikeSize.x;
		float left = transform.position.x - (size.x / 2);
		float top = transform.position.y + (size.y / 2);
		int spikeCount = (int) (size.x / spikeWidth);

		for (int i = 0; i < spikeCount; i++) {
			GameObject cspike = (GameObject)GameObject.Instantiate (spike, new Vector3(left + (i + 0.5f) * spikeWidth, top + spikeSize.y, this.transform.position.z), transform.rotation);
            cspike.transform.parent = transform;
		}
        transform.rotation = rotation;
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
