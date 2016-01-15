using UnityEngine;
using System.Collections;

public class MouseTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Screen Size: Height " + Screen.height + " Width " + Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Mouse Position: " + Input.mousePosition);
	}
}
