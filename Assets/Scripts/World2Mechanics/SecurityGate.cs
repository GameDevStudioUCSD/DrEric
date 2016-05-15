using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class SecurityGate : MonoBehaviour {

        public GameObject gateCollider;
        public GameObject gateInitialSprite;
        public GameObject gateDestroyedSprite;
        public int health = 2;
        public bool destroyed;


        // Use this for initialization
        void Update () {
            if (destroyed)
            {
                gateInitialSprite.SetActive(false);
                gateDestroyedSprite.SetActive(true);
                this.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                gateInitialSprite.SetActive(true);
                gateDestroyedSprite.SetActive(false);
            }
        }
	
        // Update is called once per frame
        public void DestroyGate () {
            if (--health < 0)
                destroyed = true;
        }

    }
}
