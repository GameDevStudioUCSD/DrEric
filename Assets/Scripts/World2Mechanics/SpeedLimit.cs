using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class SpeedLimit : MonoBehaviour {

        public float maxSpeed = 50.0f;
        private Rigidbody2D thisRigidbody2D;

        // Use this for initialization
        void Start () {
            thisRigidbody2D = this.GetComponent<Rigidbody2D>();
        }
	
        // Update is called once per frame
        void FixedUpdate () {

            if(thisRigidbody2D.velocity.magnitude > maxSpeed)
            {
                // Clamp the velocity to max speed
                thisRigidbody2D.velocity = thisRigidbody2D.velocity.normalized * maxSpeed;
            }
        }
    }
}
