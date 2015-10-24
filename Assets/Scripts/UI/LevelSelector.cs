using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector: MonoBehaviour {
    //Changed Transform to Vector3, added new to construc a new Vector3
	public Vector3 target = new Vector3(348f,0f,0f);
	public float speed = 200f;

	public LevelSelector () {

	}

	///	<summary>
	/// This loads a selected level from the level select menu
	/// </summary>
	/// <param name="name">
	/// Name of the level in String format
	/// </param>
	public void loadLevel(Text name) {
		Application.LoadLevel(name.text);
	}

	///	<summary>
	/// Exits the Level Selector and goes back to the Main Menu
	/// </summary>
	public void exitSelector() {

	}

	///	<summary>
	/// Opens an option menu
	/// </summary>
	public void openOptionMenu() {
		
	}

	public void moveCameraRight() {
		float step = speed * Time.deltaTime;
        //Because target type was changed above, .position was removed from target
		transform.position = Vector3.MoveTowards(transform.position, target, step);
	}
}