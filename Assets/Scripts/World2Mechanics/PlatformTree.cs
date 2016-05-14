using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class PlatformTree : MonoBehaviour{

        public GameObject playerHolder;
        public bool treeAlive = true;

        float floor;
        public float height = 0;

        public void Start() {
            floor = transform.position.y;
            if (treeAlive)
            {
                plantTree();
            }
            else
            {
                killTree();
            }
        }

        public void killTree()
        {
            treeAlive = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void plantTree()
        {
            treeAlive = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector3(transform.position.x, floor + height, transform.position.z);
        }

        public void shiftX(float X)
        {
            Vector3 oldPos = this.transform.position;
            Vector3 newPos = new Vector3(oldPos.x + X, oldPos.y, oldPos.z);
            this.transform.position = newPos;
        }

        public void shiftY(float Y)
        {
            Vector3 oldPos = this.transform.position;
            Vector3 newPos = new Vector3(oldPos.x, oldPos.y + Y, oldPos.z);
            this.transform.position = newPos;
        }
    }
}
