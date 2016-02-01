using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/** Filename: Switch.cs \n
 * Author: James Allen \n
 * Contributing authors: ^ \n 
 * Date Drafted: 11/9/2015 \n
 * Description: controlls switch pressing and events \n
 */
public class Switch : MonoBehaviour {

    public UnityEvent pressEvent;
    public UnityEvent unPressEvent;
    public bool isEnabled;
	public bool isDebugging;
	private bool isPressed;
    private Animator animator;
	// Use this for initialization
	void Start () {
		isPressed = false;
        isEnabled = true;
        animator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PerformCollision(Collider2D other) {
		if (isEnabled) {
			if (other.gameObject.tag == "Player") {
				if (!isPressed) {//upressed to pressed
					if (isDebugging)
						Debug.Log ("Switch Pressed");
					pressEvent.Invoke (); //threw an ArgumentOutOfRangeException, boss disappeared
					isPressed = !isPressed;
					if (animator != null)
						animator.SetBool ("IsPressed", true);
				} else if (isPressed) {//pressed to unpressed
					if (isDebugging)
						Debug.Log ("Switch unpressed");
					unPressEvent.Invoke ();
					isPressed = !isPressed;
					if (animator != null)
						animator.SetBool ("IsPressed", false);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		PerformCollision (other);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
		PerformCollision (other.collider);
    }

    public void SetPressed(bool x) {
    	isPressed = x;
    }
    public bool GetPressed(){
    	return isPressed;
    }

    public void setEnabled (bool x)
    {
        enabled = x;
    }
}
