using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Filename: VictoryController.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: 8/2/2015 \n
 * Description: A very simple controller script that currently will only 
 *              instantiate a copy of the victory screen when another object
 *              collides with this one.
 */
public class VictoryController : MonoBehaviour {
    /** The victory screen prefab */
    public GameObject victoryScreen;
    /** Spawns a victory screen when an object collides with this */

	//TESTING PERSISTENT DATA
	private int count;
	public Text countOut;

	void Start() {
		count = PlayerPrefs.GetInt ("Count", 0);
	}
	//END TEST

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(victoryScreen);
        Invoke("nextLevel", 2.0f);

		//TESTING PERSISTENT DATA

		Debug.Log (count);
		countOut.text = "" + count;
		saveCheckpoint ();

		//END TEST
            if (collision.gameObject.tag == "Player"){
            Instantiate(victoryScreen);
            Invoke("nextLevel", 2.0f);
        }
    }

    void nextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

	void saveCheckpoint()
	{
		//TESTING PERSISTENT DATA

		PlayerPrefs.SetInt("Count", count + 1); 

		//This function will write to disk potentially causing a small hiccup,
		//therefore it is not recommended to call during actual gameplay.
		// - Unity API Ref
		PlayerPrefs.Save ();

	}
}
