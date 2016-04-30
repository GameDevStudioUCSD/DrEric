using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SliderJoint2D))]
public class BikiniSapling : Triggerable {

    [SerializeField]
    public BikiniTree presentTree;

    public GameObject water;
    public float maxSurfaceHeight;
    public float maxWaterHeight;
    public bool saplingOnWater = true;
    public bool saplingHydrated = false;
	public bool playerOnTop = false;


    private float prevX;
    private SliderJoint2D slider;

    // Use this for initialization
    void Start () {
        prevX = this.transform.position.x;
		if (saplingHydrated)
			HydrateSapling();
		else
			DehydrateSapling();

        // Initializing the slider for x-axis locking
        slider = this.GetComponent<SliderJoint2D>();
        slider.connectedAnchor = new Vector2(this.transform.position.x, maxSurfaceHeight);

        slider.enableCollision = true;
        slider.enabled = false;
        slider.angle = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (presentTree != null && presentTree.treeAlive)
		{
			float currX = this.transform.position.x;
			presentTree.shiftX(currX - prevX);
			prevX = currX;
		}

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

    public sealed override void Trigger()
    {
		if (slider != null) {
			slider.enabled = false;
		}
        saplingOnWater = !saplingOnWater;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
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
