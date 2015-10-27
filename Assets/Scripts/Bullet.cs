﻿using UnityEngine;
using System.Collections;

/**
 * The script for cannon bullets. Bullets move with direction and speed determined by the cannon.
 */
public class Bullet : MonoBehaviour {
	private Vector3 direction;
	private float delta;
    private float startTime;
    [Range(.1f, 10)]
    public float destroyAfterNSeconds = 1;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > destroyAfterNSeconds)
        {
            GameObject.Destroy(this.gameObject);
        }
		transform.position += delta * direction;
	}

	/**
	 * Changes the direction the cannonball travels in
	 * Called by the cannon that fires it
	 * 
	 * @param directionPar the direction to point the cannonball
	 */
	public void setDirection(Vector3 directionPar) {
		direction = directionPar;
	}

	/**
	 * Changes the speed of the cannonball
	 * Called by the cannon that fires it
	 * 
	 * @param deltaPar the new speed of the cannonball
	 */
	public void setDelta(float deltaPar) {
		delta = deltaPar;
	}
}