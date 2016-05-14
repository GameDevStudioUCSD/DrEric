using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class MoveObstacle : Triggerable {

        // What should the obstacle's starting position be?
        public bool startAtLoweredPosition = true;

        // The end positions of raising or lowering
        public Vector3 raisedPosition;
        public Vector3 loweredPosition;

        // Rate at which obstacle should lerp
        public float moveRate = 0f;

        // States this obstacle can be in
        enum State { Initial, Raised, Lowered, Raising, Lowering };

        // The current state the obstacle is in
        private State currentState = State.Initial;

        // The position the obstacle is currently at if it is going from lowering to raising or vice versa
        private Vector3 intermediatePosition;

        // Used by lerp to determine how much the object has moved
        private float positionFraction = 0.0f;

        public ActivateSaplingSpawners activateButtons;

        // Use this for initialization
        void Start()
        {
            // Initialize state based on whether the obstacle starts at lowered or raised position
            currentState = startAtLoweredPosition ? State.Lowered : State.Raised;
        }

        // Update is called once per frame
        void Update()
        {

            // Do something based on what state the obstacle is in
            switch (currentState)
            {
                case State.Raised:
                    break;

                case State.Lowered:
                    break;

                case State.Raising:
                    Raise();
                    break;

                case State.Lowering:
                    Lower();
                    break;
            }

        }

        /*
     * Trigger
     * 
     * Access function for obstacle.  Button will have to know about this function
     * to cause the obstacle to lower/raise.
     */
        public sealed override void Trigger()
        {
            // If the obstacle is currently in raised position or rising
            if (currentState == State.Raised || currentState == State.Raising)
            {
                currentState = State.Lowering;
            }
            // If the obstacle is currently in lowered position or lowering
            else if (currentState == State.Lowered || currentState == State.Lowering)
            {
                currentState = State.Raising;
            }

            // Get the intermediate position so lerp doesn't jump around
            intermediatePosition = this.transform.position;
            positionFraction = 0.0f;

        }

        private void Raise()
        {
            if (positionFraction < 1.0)
            {
                positionFraction += Time.deltaTime * moveRate;
                this.transform.position = Vector3.Lerp(intermediatePosition, raisedPosition, positionFraction);
            }
            else
            {
                currentState = State.Raised;
                activateButtons.Activate();
            }
        }

        private void Lower()
        {
            if (positionFraction < 1.0)
            {
                positionFraction += Time.deltaTime * moveRate;
                this.transform.position = Vector3.Lerp(intermediatePosition, loweredPosition, positionFraction);
            }
            else
            {
                intermediatePosition = this.transform.position;
                positionFraction = 0.0f;
                currentState = State.Raising;
            }
        }
    }
}
