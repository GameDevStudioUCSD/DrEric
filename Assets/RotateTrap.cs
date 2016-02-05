using UnityEngine;
using System.Collections;

public class RotateTrap : MonoBehaviour {

	public float speed = 0.5f;

	int realDir;
	
	public enum Direction {cw, ccw};
	public Direction dir;
	public float limit = 90f;
	//float curr = 0;

	public GameObject hinge;
	public Switch activator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void DoRotate(Direction dir, bool trapping) {
		int realDir = 1;
		if (dir == Direction.ccw) {
			realDir = -1;
		}
		if (!trapping) {
			realDir *= -1;
		}
		

		this.transform.RotateAround (hinge.transform.position, Vector3.back, speed * realDir);
	}

	public void Trap() {
		StartCoroutine("DoTrap");
	}

	IEnumerator DoTrap() {
		for (float curr = 0; curr < limit; curr += speed) {
			DoRotate (dir, true);
			yield return null;
		}
		yield return new WaitForSeconds (1);
		for (float curr = limit; curr > 0; curr -= speed) {
			DoRotate (dir, false);
			yield return null;
		}
		activator.SetPressed (false);
	}
}
