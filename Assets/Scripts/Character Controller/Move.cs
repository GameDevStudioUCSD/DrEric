using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	/**
	 * Speed for moving the player character.
	 */
	public int moveSpeed;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {

			this.transform.Translate(Vector3.up * Time.deltaTime * this.moveSpeed);
		} else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) &&
		           !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
			
			this.transform.Translate(Vector3.left * Time.deltaTime * this.moveSpeed * -1);
		} else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
		           Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
			
			Vector3 move = new Vector3(
				UnityEngine.Random.Range(-5, 5),
				UnityEngine.Random.Range(-5, 15),
				UnityEngine.Random.Range(-5, 5)
			);
			this.transform.Translate(move * Time.deltaTime * this.moveSpeed);
		} else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
		           !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {

			this.transform.Translate(Vector3.right * Time.deltaTime * this.moveSpeed * -1);
		}
	}
}
