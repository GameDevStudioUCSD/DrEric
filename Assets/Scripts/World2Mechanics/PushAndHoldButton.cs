using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushAndHoldButton : MonoBehaviour
{
	public List<GameObject> triggerTargetList = new List<GameObject>();

	public Animator happyButtonAnimator = null;

	protected List<Triggerable> triggerableScriptList = new List<Triggerable>();
	private AudioSource aSource;

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

		aSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		foreach (Triggerable triggerScript in triggerableScriptList) {
			triggerScript.Trigger (true);
		}
		// Animation state change
		happyButtonAnimator.SetBool ("pressed", true);
	}
		
	public void endPress()
	{
		foreach (Triggerable triggerScript in triggerableScriptList) {
			triggerScript.Trigger (false);
		}
		// Animation state change
		happyButtonAnimator.enabled = true;
		happyButtonAnimator.SetBool ("pressed", false);
	}

	public void pressEnd() {}
}