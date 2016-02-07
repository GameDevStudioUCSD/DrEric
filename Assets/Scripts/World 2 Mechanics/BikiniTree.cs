using UnityEngine;
using System.Collections;

public class BikiniTree : TimeObject{

	public GameObject playerHolder;
	private BallController playerCharacter;
	public float headHeightLimit = 2;
	private Platform myPlatform;
	private bool onTreeTop;
	public bool treeAlive = true;
	
	// Use this for initialization
	void Start () {
		playerCharacter = null;
		myPlatform = this.GetComponent<Platform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCharacter == null) {
			playerCharacter = playerHolder.GetComponentInChildren<BallController>();
		}
		
		if (onTreeTop)
			return;

		if (!treeAlive)
			return; 
	
		float playerTop = playerCharacter.transform.position.y;
		if (playerTop > this.headHeightLimit) {
			this.Expand(playerTop);
		} else {
			this.Expand(this.headHeightLimit);
		}
	}
	
	// Move tree's top to "height"
	public void Expand(float height) {
		//myPlatform.enabled = true;
		//myPlatform.endVector.y = height - 25;
        Vector3 oldPos = this.transform.position;
        Vector3 newPosition = new Vector3(oldPos.x, height, oldPos.z);
		this.transform.position = newPosition;
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			onTreeTop = true;
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

	}
	
}
