using UnityEngine;
using System.Collections;

/**
 * Script for Cannon objects. Controls cannonball generation.
 */
public class Cannon : MonoBehaviour {
	public GameObject bullet; //the prefab for the cannonballs
	public int time; //the time between shots
	private int curTime; //running count of time until next shot
	public Vector3 direction; //Direction the cannon points and the cannonballs travel
	public float delta; //Speed of the cannonballs
    public bool fireOnUpdate = false; // Should the cannon fire from the update method?

	// Use this for initialization
	void Start () {
		curTime = time;
	}
	
	// Update is called once per frame
	void Update () {
		curTime -= 1;
		if (curTime == 0 && fireOnUpdate) {
			//creating bullet
            FireBullet();
			//prepare for next shot
			curTime = time;
		}
	}
    public void FireBullet()
    {
        GameObject myBullet = (GameObject)GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
        myBullet.transform.parent = transform; // This keeps the file hierarchy clean
        Bullet thisBullet = myBullet.GetComponent<Bullet>();
        //setting bullet properties
        thisBullet.setDirection(direction);
        thisBullet.setDelta(delta);

    }
}
