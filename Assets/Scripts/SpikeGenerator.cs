using UnityEngine;
using System.Collections;

public class SpikeGenerator : MonoBehaviour {
	
	public GameObject spike;

	// Use this for initialization
	void Start () {
		Vector3 size = Vector3.Scale (this.transform.localScale, gameObject.GetComponent<BoxCollider2D>().size);
		Vector3 spikeSize = Vector3.Scale (this.spike.transform.localScale, this.spike.gameObject.GetComponent<BoxCollider2D>().size);
		float spikeWidth = spikeSize.x;
		float left = transform.position.x - (size.x / 2);
		float top = transform.position.y + (size.y / 2);
		int spikeCount = (int) (size.x / spikeWidth);

		for (int i = 0; i < spikeCount; i++) {
			GameObject.Instantiate (spike, new Vector3(left + (i + 0.5f) * spikeWidth, top + spikeSize.y / 2, this.transform.position.z), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
