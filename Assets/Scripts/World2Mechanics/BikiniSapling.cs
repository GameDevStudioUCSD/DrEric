using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class BikiniSapling : MonoBehaviour {

        [SerializeField]
        public BikiniTree presentTree;
        public bool playerOnTop = false;
        public bool saplingOnWater = true;
        public bool saplingHydrated = false;
        private float prevX;

        void Start () {
            prevX = this.transform.position.x;
            if (saplingHydrated)
                HydrateSapling();
            else
                DehydrateSapling();

        }
	
        // Update is called once per frame
        void Update () {
            if (presentTree != null && presentTree.treeAlive)
            {
                float currX = this.transform.position.x;
                presentTree.shiftX(currX - prevX);
                prevX = currX;
            }
        }
		

        void DehydrateSapling()
        {
            if (presentTree != null)
                presentTree.killTree();
            if (this.saplingOnWater)
                this.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        void HydrateSapling()
        {
            if (presentTree != null)
                presentTree.plantTree();
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.GetComponent<Rigidbody2D> ().AddForce (0.03f * Vector2.up, ForceMode2D.Impulse);
        }

        void OnTriggerEnter2D (Collider2D other) {
            if (other.tag == "Water") {
                HydrateSapling();
            }

            if (other.tag == "Player") {
                playerOnTop = true;
            }
        }

        void OnTriggerStay2D (Collider2D other) {
            if (other.tag == "Water") {
                HydrateSapling();
            }

            if (other.tag == "Player") {
                playerOnTop = true;
            }
        }

        void OnTriggerExit2D (Collider2D other) {
            if (other.tag == "Water") {
                DehydrateSapling();
            }

            if (other.tag == "Player") {
                playerOnTop = false;
            }
        }

        // If this object is destroyed, then destroy the present tree
        void OnDestroy()
        {
            if(presentTree != null)
            {
                Destroy(presentTree.gameObject);
            }
        }

    }
}
