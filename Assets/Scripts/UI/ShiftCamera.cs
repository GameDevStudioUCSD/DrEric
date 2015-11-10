using UnityEngine;
using System.Collections;

public class ShiftCamera : MonoBehaviour {

	public bool state = false;
	public float offset = 100;
	private int menuState = 1;
	//private ArrayList<Animation> animations = new ArrayList<Animation>;

	public void ShiftRight()
	{
		//Debug.Log ("shifting right");
		//Camera.main.transform.Translate (offset, 0, 0);
		Animation animation = GetComponent<Animation>();
		animation.Play("levelTransition" + menuState);
		menuState++;
		state = false;
	}

	public void ShiftLeft()
	{
		GetComponent<Animation>().Play();
		menuState--;
	}

}
