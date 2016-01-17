using UnityEngine;
using System.Collections;

public class Snek : MonoBehaviour {

	bool isMoving = false;
	public float speed;
	private Vector2 direction;
	
	// Use this for initialization
	void Start () {
		direction = new Vector2(-1,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving)
		{
	
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		isMoving = true;
		transform.position.Set (transform.position.x, transform.position.y - 1, transform.position.z);
	}
}
