using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class TriggerStove : Triggerable {

        // is the stove lit initially?
        public bool lit = false;

        // public fields for the lit and unlit sprites
        public Sprite spriteLit;
        public Sprite spriteUnlit;
        public DialogBox dialogBox;

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


        // When object enters collider
        void OnTriggerEnter2D (Collider2D other)
        {
            if (other.tag == "Sapling" && lit) 
            {
                Debug.Log ("Triggered");
                Destroy (other.gameObject);
            }

            if (other.tag == "Player" && lit) {
                dialogBox.DisplayText ("Oops");
            }
        }

        public override void Trigger ()
        {

            // switching state and sprite on trigger
            if (currentState == State.Lit) {
                currentSprite = spriteUnlit;
                currentState = State.Unlit;
                lit = false;
            } 

            else {
                currentSprite = spriteLit;
                currentState = State.Lit;
                lit = true;
            }
			
            spriterenderer.sprite = currentSprite;
			
        }
    }
}
