using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

	public MotherSquid squid;
	public enum EyeState {open, closed, damaged, gone};
	public EyeState state;

	public SpriteRenderer eyeOpen;
	public SpriteRenderer eyeClosed;
	public SpriteRenderer eyeDamaged;

	// Use this for initialization
	void Start () {
		state = EyeState.closed;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == EyeState.closed)
		{
			eyeOpen.enabled = false;
			eyeClosed.enabled = true;
			eyeDamaged.enabled = false;
		}
		if (state == EyeState.open)
		{
			eyeOpen.enabled = true;
			eyeClosed.enabled = false;
			eyeDamaged.enabled = false;
		}
		if (state == EyeState.damaged)
		{
			eyeOpen.enabled = false;
			eyeClosed.enabled = false;
			eyeDamaged.enabled = true;
		}
		if (state == EyeState.closed)
		{
			eyeOpen.enabled = false;
			eyeClosed.enabled = false;
			eyeDamaged.enabled = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && squid.state == MotherSquid.SquidState.Recovering && state == EyeState.open) {
			state = EyeState.damaged;
			squid.getHit ();
			this.gameObject.SetActive(false);
		}
	}

	public bool isOpen() {
		return state == EyeState.open;
	}
}
