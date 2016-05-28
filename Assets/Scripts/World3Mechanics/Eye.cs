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
			Debug.Log("CLOSED");
			eyeOpen.enabled = false;
			eyeClosed.enabled = true;
			eyeDamaged.enabled = false;
		}
		else if (state == EyeState.open)
		{
			Debug.Log("OPEN");
			eyeOpen.enabled = true;
			eyeClosed.enabled = false;
			eyeDamaged.enabled = false;
		}
		else if (state == EyeState.damaged)
		{
			Debug.Log("DAMAGED");
			eyeOpen.enabled = false;
			eyeClosed.enabled = false;
			eyeDamaged.enabled = true;
		}
		else if (state == EyeState.gone)
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
