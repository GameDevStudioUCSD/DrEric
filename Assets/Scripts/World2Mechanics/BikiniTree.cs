﻿using UnityEngine;
using System.Collections;

public class BikiniTree : MonoBehaviour{

	public GameObject playerHolder;
	private BallController playerCharacter;
	public float headHeightLimit = 3;
	private bool onTreeTop;
	public bool treeAlive = true;
	private float yOrig;
	
	// Use this for initialization
	void Start () {
		playerCharacter = null;
		yOrig = transform.position.y;

		if (treeAlive)
		{
			plantTree();
		}
		else
		{
			killTree();
		}
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
        if (playerCharacter != null) {
            float playerTop = playerCharacter.transform.position.y;
            if (playerTop > this.headHeightLimit) {
                this.Expand(yOrig + playerTop - headHeightLimit);
            }
            else {
                this.Expand(yOrig);
            }
        }
	}
	
	// Move tree's top to "height"
	public void Expand(float height) {
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

	public void shiftX(float X)
	{
		Vector3 oldPos = this.transform.position;
		Vector3 newPos = new Vector3(oldPos.x + X, oldPos.y, oldPos.z);
		this.transform.position = newPos;
	}
}
