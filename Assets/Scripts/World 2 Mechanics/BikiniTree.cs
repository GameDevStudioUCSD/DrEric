using UnityEngine;
using System.Collections;

public class BikiniTree : TimeObject{

	public GameObject playerHolder;
	public BallController playerCharacter;
	private float top = 0;
	private Platform myPlatform;
	
	// Use this for initialization
	void Start () {
		playerCharacter = null;
		myPlatform = this.GetComponent<Platform>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Bounds bounds = mesh.bounds;
		top = bounds.max.y;		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCharacter == null) {
			playerCharacter = playerHolder.GetComponentInChildren<BallController>();
		}
		
		float playerTop = playerCharacter.transform.position.y;
		if (playerTop > this.top) {
			this.Expand(playerTop);
		} else {
			this.Expand(this.top);
		}
	}
	
	// Move tree's top to "height"
	public void Expand(float height) {
		//myPlatform.enabled = true;
		//myPlatform.endVector.y = height - 25;
		this.transform.position.y = height;
	}
	
}
