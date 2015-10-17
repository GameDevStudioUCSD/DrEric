using UnityEngine;
using System.Collections;

public class MainMenuControl : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void exitGame()
    {
        //http://answers.unity3d.com/questions/544869/problem-with-applicationquit.html
        //Only works in build
        Application.Quit();
    }

    public void loadLevel()
    {
        //This may seem redundant, but it may be useful for debugging later.
        Application.LoadLevel("");
    }

    public void startNewGame()
    {
        Application.LoadLevel("whoa");
    }

    public void loadOptions()
    {
        Application.LoadLevel("");
    }
}
