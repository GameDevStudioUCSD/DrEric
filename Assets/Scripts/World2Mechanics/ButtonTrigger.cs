using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Used for List<T>

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

    public Animator happyButtonAnimator = null;

    private List<Triggerable> triggerableScriptList = new List<Triggerable>();
    private AudioSource aSource;

	// Use this for initialization
	void Start () {
        if (triggerTargetList.Count == 0)
        {
            Debug.LogError("ButtonTrigger: List of triggerable objects empty!");
        }

        // Set pressed animation state
        happyButtonAnimator.SetBool("pressed", true);

        // Get the Triggerable script from targets
        foreach (GameObject triggerTarget in  triggerTargetList)
        {
            triggerableScriptList.Add(triggerTarget.GetComponent<Triggerable>());
        }

		aSource = GetComponent<AudioSource>();
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
				aSource.Play();
                // Animation state change
                happyButtonAnimator.SetBool("pressed", true);
            }
        }
    }

    // Animation Event
    void pressEnd()
    {
        happyButtonAnimator.SetBool("pressed", false);
    }
}
