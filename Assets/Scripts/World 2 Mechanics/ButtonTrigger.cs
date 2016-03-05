using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// ButtonTrigger
/// 
/// Attach this script to a button type object that will "trigger"
/// something in another object.
/// Upon collision with player object, will call Trigger function
/// in other object.
/// 
/// </summary>

public class ButtonTrigger : MonoBehaviour {

    // The object to trigger something in
    public GameObject triggerTarget = null;

    // The Triggerable script attached to trigger target
    private Triggerable triggerableComponent = null;

	// Use this for initialization
	void Start () {
        if (triggerTarget == null)
        {
            Debug.LogError("ButtonTrigger: null exception, triggerTarget needs to be initialized.");
        }

        // Get the Triggerable script from target
        triggerableComponent = triggerTarget.GetComponent<Triggerable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            // trigger
            triggerableComponent.Trigger();
        }
    }
}
