using UnityEngine;
using System.Collections;

/**
 * The script for cannon bullets. Bullets move with direction and speed determined by the cannon.
 */
public class Bullet : MonoBehaviour {
	private float angle;
	private float delta;
    private float startTime;
    public Transform spriteTransform;
    [Range(.1f, 10)]
    public float destroyAfterNSeconds = 1;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		Vector2 direction = new Vector2 (delta * Mathf.Cos (angle * Mathf.Deg2Rad),
		                                delta * Mathf.Sin (angle * Mathf.Deg2Rad));
		this.GetComponent<Rigidbody2D> ().AddForce (direction, ForceMode2D.Impulse);
        if( spriteTransform == null )
        {
            spriteTransform = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > destroyAfterNSeconds)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

	/**
	 * Changes the direction the cannonball travels in
	 * Called by the cannon that fires it
	 * 
	 * @param anglePar the angle to point the cannonball
	 */
	public void setDirection(float anglePar) {
		angle = anglePar;
        if(spriteTransform != null)
            spriteTransform.Rotate(new Vector3(0, 0, angle));
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