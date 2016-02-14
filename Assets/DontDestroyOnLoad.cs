using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

    public Text tb;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        tb.text = "Level Loaded: " + level;
    }
}
