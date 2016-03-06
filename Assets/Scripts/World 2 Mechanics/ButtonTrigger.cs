using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public List<GameObject> triggerTargetList = new List<GameObject>();

    private List<Triggerable> triggerableScriptList = new List<Triggerable>();

	// Use this for initialization
	void Start () {
        if (triggerTargetList.Count == 0)
        {
            Debug.LogError("ButtonTrigger: List of triggerable objects empty!");
        }

        // Get the Triggerable script from targets
        foreach (GameObject triggerTarget in  triggerTargetList)
        {
            triggerableScriptList.Add(triggerTarget.GetComponent<Triggerable>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            foreach (Triggerable triggerScript in triggerableScriptList)
            {
                triggerScript.Trigger();
            }
        }
    }
}
