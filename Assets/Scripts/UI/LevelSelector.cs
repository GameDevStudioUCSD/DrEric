using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//lalalalalalalaaaa
// NOT USED ATM
public class LevelSelector: MonoBehaviour { 
    //changed target type from transform to vector3 and added new in front of vector3
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
        //changed 3rd parameter of MoveTowards from target.position to target
		transform.position = Vector3.MoveTowards(transform.position, target, step);
	}
}