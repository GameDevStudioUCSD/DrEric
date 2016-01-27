using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
	/** Filename: KeyScript.cs \n
 * Author: James Allen \n
 * Contributing authors: Kalan Miurrelle\n 
 * Date Drafted: 11/9/2015 \n
 * Description: determines key collision \n
 */
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
			//play sound effect
			AudioSource source = GetComponent<AudioSource> ();
			source.Play ();
			//disable sprite and remove object after 1 second.
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			Invoke ("DestroyKey", 1.0f);
            
        }
    }
	void DestroyKey()
	{
		Destroy (this.gameObject, 0);
	}
}
