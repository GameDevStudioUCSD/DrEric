using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {
/** Filename: GateScript.cs \n
 * Author: James Allen \n
 * Contributing authors: ^ \n 
 * Date Drafted: 11/9/2015 \n
 * Description:  checks for player collision and key number \n
 */
 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject go = GameObject.Find("KeyController");
            KeyControllerScript KCS =
                (KeyControllerScript)go.GetComponent(typeof(KeyControllerScript));
            Debug.Log("Gate Touched");
            if (KCS.keys_collected > 0)
            {
                KCS.keys_collected--;
                Destroy(this.gameObject, 0);
            }
        }
    }
}
