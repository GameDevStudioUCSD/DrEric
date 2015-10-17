using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector: MonoBehaviour {
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


}