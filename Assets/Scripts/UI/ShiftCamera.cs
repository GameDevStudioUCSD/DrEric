using UnityEngine;
using System.Collections;

public class ShiftCamera : MonoBehaviour {

	public int MAX_LEVEL = 4;
	public bool state = false;
	public float offset = 100;
	private int levelState = 1;

	//private ArrayList<Animation> animations = new ArrayList<Animation>;

	public void ShiftRight()
	{
		//Debug.Log ("shifting right");
		//Camera.main.transform.Translate (offset, 0, 0);
		Animation animation = GetComponent<Animation>();
		if (levelState < MAX_LEVEL) {
			animation.Play ("levelTransitionRight" + levelState);
			levelState++;
			Debug.Log ("Shit");
		} else {
			Debug.Log ("No shit");
		}
		//state = false;
	}

	public void ShiftLeft()
	{
		Animation animation = GetComponent<Animation>();
		if (levelState > 1) {
			animation.Play ("levelTransitionLeft" + levelState);
			levelState--;
			Debug.Log ("Shit");
		} else {
			Debug.Log ("No shit");
		}
	}

}
