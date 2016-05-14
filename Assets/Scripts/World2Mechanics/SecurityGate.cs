using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class SecurityGate : MonoBehaviour {

        public GameObject gateCollider;
        public GameObject gateInitialSprite;
        public GameObject gateDestroyedSprite;
        public bool destroyed;


        // Use this for initialization
        void Update () {
            if (destroyed)
            {
                gateInitialSprite.SetActive(false);
                gateDestroyedSprite.SetActive(true);
                gateCollider.SetActive(false);
            }
            else
            {
                gateInitialSprite.SetActive(true);
                gateDestroyedSprite.SetActive(false);
     //           gateCollider.SetActive(true);
            }
        }
	
        // Update is called once per frame
        public void DestroyGate () {
            destroyed = true;
        }

    }
}
