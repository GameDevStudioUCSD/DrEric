using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/** Filename: Switchscript.cs \n
 * Author: James Allen \n
 * Contributing authors: ^ \n 
 * Date Drafted: 11/9/2015 \n
 * Description: controlls switch pressing and events \n
 */
public class SwitchScript : MonoBehaviour {

    public UnityEvent pressEvent;
    public UnityEvent unPressEvent;
	public bool isDebugging;
	private bool isPressed;
	// Use this for initialization
	void Start () {
		isPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
        	if (!isPressed) {
	    		if (isDebugging)
           		 	Debug.Log("Switch Pressed");
            	pressEvent.Invoke();
            	isPressed = !isPressed;
        	}
        	else if (isPressed) {
        		if (isDebugging)
        			Debug.Log("Switch unpressed");
        		unPressEvent.Invoke();
        		isPressed = !isPressed;
        	}
        }
    }

    public void SetPressed(bool x) {
    	isPressed = x;
    }
    public bool GetPressed(){
    	return isPressed;
    }
}
