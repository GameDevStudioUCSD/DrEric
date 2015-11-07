using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject go = GameObject.Find("KeyController");
            KeyControllerScript KCS = 
                (KeyControllerScript)go.GetComponent(typeof(KeyControllerScript));
            Debug.Log("Key touched");
            KCS.keys_collected++;
            Destroy(this.gameObject, 0);
        }
    }
}
