using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class SaplingEater : MonoBehaviour {

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == Names.SAPLING)
            {
                // Destroy sapling and the corresponding present tree
                Destroy(other.gameObject);
            }
        }
    }
}
