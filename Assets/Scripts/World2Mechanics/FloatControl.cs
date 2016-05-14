using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class FloatControl : Triggerable {

        [SerializeField]
        public GameObject water;
        public float maxSurfaceHeight;
        public float maxWaterHeight;

        public GameObject sapling;
        private SliderJoint2D slider;

        // Use this for initialization
        void Start()
        {
            // Initializing the slider for x-axis locking
            slider = sapling.GetComponent<SliderJoint2D>();
            slider.connectedAnchor = new Vector2(sapling.transform.position.x, maxSurfaceHeight);

            slider.enableCollision = true;
            slider.enabled = false;
            slider.angle = 0;



        }

        // Update is called once per frame
        void Update () {
            // Check condition for the axis lock
            if (water.transform.position.y >= maxWaterHeight && this.transform.position.y >= maxSurfaceHeight)
            {
                slider.enabled = true;
                this.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else {
                slider.enabled = false;
            }
        }

        public sealed override void Trigger()
        {
            if (slider != null)
            {
                slider.enabled = false;
            }
            //sapling.saplingOnWater = !sapling.saplingOnWater;
            this.GetComponent<Rigidbody2D>().isKinematic = false;
        }

    }
}
