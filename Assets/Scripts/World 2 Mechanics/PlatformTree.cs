using UnityEngine;
using System.Collections;

public class PlatformTree : MonoBehaviour{

	public GameObject playerHolder;
	public bool treeAlive = true;

	float floor;
	public float height = 0;

	public void Start() {
		floor = transform.position.y;
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
}
