using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class PlatformSapling : MonoBehaviour {

        public PlatformTree presentTree;
        public bool saplingHydrated = false;
        public bool playerOnTop = false;
        public Sprite hydratedSapling;
        public Sprite dehydratedSapling;
        private float prevX;
        private float prevY;

        // Use this for initialization
        void Start () {
            prevX = this.transform.position.x;
            prevY = this.transform.position.y;
            if (saplingHydrated)
                HydrateSapling();
            else
                DehydrateSapling();
        }
	
        // Update is called once per frame
        void Update () {
            if (presentTree != null /*&& presentTree.treeAlive*/)
            {
                float currX = this.transform.position.x;
                float currY = this.transform.position.y;
                presentTree.shiftX(currX - prevX);
                presentTree.shiftY(currY - prevY);
                prevX = currX;
                prevY = currY;
            }
        }

        void DehydrateSapling()
        {
            if (presentTree != null)
                presentTree.killTree();
            this.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = dehydratedSapling;

        }

        void HydrateSapling()
        {
            if (presentTree != null)
                presentTree.plantTree();
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionY
                | RigidbodyConstraints2D.FreezeRotation;

            this.gameObject.GetComponent<SpriteRenderer>().sprite = hydratedSapling;
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
