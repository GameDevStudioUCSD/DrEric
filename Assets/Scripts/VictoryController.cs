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
public class VictoryController : MonoBehaviour
{
    /** The world where the level is in */
    public World world = World.Space;
    /** The level to load when the player hits this controller */
    public Level nextLevel = Level.One;
    /** The victory screen prefab */
    public GameObject victoryScreen;
    /** Spawns a victory screen when an object collides with this */

	//TESTING PERSISTENT DATA
	private int count;
	public Text countOut;

    bool hasWon = false;
	void Start() {
		count = PlayerPrefs.GetInt ("Count", 0);
	}
	//END TEST

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckWinConditions();
        if (collision.gameObject.tag == "Player")
        {
            if (!hasWon)
                Instantiate(victoryScreen);
            Invoke("LoadNextLevel", 2.0f);
            hasWon = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckWinConditions();
        if (collision.gameObject.tag == "Player")
        {
            if (!hasWon)
            {
                Instantiate(victoryScreen);
                spinEric(collision.gameObject);
            }
            Invoke("LoadNextLevel", 2.0f);
            hasWon = true;
        }
    }

    void CheckWinConditions()
    {
        //Instantiate(victoryScreen);
        //Invoke("LoadNextLevel", 2.0f);

        //TESTING PERSISTENT DATA

        if (countOut != null)
        {
            Debug.Log("This is temporarily fixed.");
            countOut.text = "" + count;
        }
        else
            Debug.LogError("AnhQuan, fix this NullReference exception please");
        saveCheckpoint();

        //END TEST
    }

    void LoadNextLevel()
    {
        LevelLoader.LoadLevel(world, nextLevel);
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

    public void spinEric(GameObject drEric)
    {
        Transform drEricTransform = drEric.transform;
        Rigidbody2D drEricRigidBody = drEric.GetComponent<Rigidbody2D>();
        ConstantForce2D force = drEric.AddComponent<ConstantForce2D>();
        drEric.GetComponent<BallController>().enabled = false;
        drEricTransform.parent = transform;
        drEricTransform.localPosition = Vector2.zero;
        drEricRigidBody.velocity = Vector2.zero;
        drEricRigidBody.gravityScale = 0;
        force.torque = 100;
    }
}
