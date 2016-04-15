using UnityEngine;
using System.Collections;

public class TriggerStove : Triggerable {

	// is the stove lit initially?
	public bool lit = false;

	// public fields for the lit and unlit sprites
	public Sprite spriteLit;
	public Sprite spriteUnlit;

	// reference to the current sprite
	private Sprite currentSprite;
	// reference to the current spriterenderer
	private SpriteRenderer spriterenderer;

	// states stove can be in
	enum State { Unlit, Lit };

	// current state of object
	private State currentState;


	// Use this for initialization
	void Start () {

		spriterenderer =  GetComponent<SpriteRenderer>();
		currentState = lit ? State.Lit : State.Unlit;
		currentSprite = lit ? spriteLit : spriteUnlit;

		spriterenderer.sprite = currentSprite;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.collider.tag == "Sapling") {

			Debug.Log ("Collided with sapling");
			if (currentState == State.Lit)
				other.gameObject.GetComponent<BikiniSapling> ().killSapling ();
		}
	}

	public override void Trigger ()
	{

		// switching state and sprite on trigger
		if (currentState == State.Lit) {
			currentSprite = spriteUnlit;
			currentState = State.Unlit;
		} 

		else {
			currentSprite = spriteLit;
			currentState = State.Lit;
		}
			
		spriterenderer.sprite = currentSprite;
			
	}
}
