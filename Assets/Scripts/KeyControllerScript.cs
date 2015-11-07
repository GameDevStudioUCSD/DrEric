using UnityEngine;
using System.Collections;

public class KeyControllerScript : MonoBehaviour {

    public int keys_collected;

	void Start () {
        keys_collected = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void incrementKeys()
    {
        keys_collected++;
    }

    public int getKeys()
    {
        return keys_collected;
    }
        
}
