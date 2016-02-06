using UnityEngine;
using System.Collections;

public class KeyControllerScript : MonoBehaviour {

    public int keys_collected;
/** Filename: KeyControllerScript.cs \n
 * Author: James Allen \n
 * Contributing authors: ^ \n 
 * Date Drafted: 11/9/2015 \n
 * Description:  controls number of keys collected and spent \n
 */
	void Start () {
		ResetKeys();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

    public void IncrementKeys() {
        keys_collected++;
    }

    public int GetKeys() {
        return keys_collected;
    }
     
	public void ResetKeys() {
		keys_collected = 0;
	}
}
