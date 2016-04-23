using UnityEngine;
using System.Collections;

public class PushAndHoldButton : ButtonTrigger
{
	void OnCollisionEnter2D(Collision2D other)
	{
		foreach (Triggerable triggerScript in triggerableScriptList) {
			triggerScript.Trigger ();

			// Animation state change
			happyButtonAnimator.SetBool ("pressed", true);
		}
	}

	void pressEnd() {
		foreach (Triggerable triggerScript in triggerableScriptList) {
			triggerScript.Trigger ();

			// Animation state change
			happyButtonAnimator.SetBool ("pressed", true);
		}
	}
}