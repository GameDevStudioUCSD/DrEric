using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Script for Cannon objects. Controls cannonball generation.
 */
public class Cannon : MonoBehaviour {
	public GameObject bullet; //the prefab for the cannonballs

	public bool fireOnUpdate = false; // Should the cannon fire from the update method?
	public int time; //the time between shots
	public float bulletSpeed; //Speed of the cannonballs
	private int curTime; //running count of time until next shot

	public float rotation; //Amount the cannon rotates each frame
	public float fireAngle; //Cannon fires every n degrees; disabled if 0
	private float curRot; //running count of degrees until next shot
	public SpriteRenderer skin;

	// Use this for initialization
	void Start () {
		curTime = time;
		curRot = fireAngle;
	}
	
	// Update is called once per frame
	void Update () {
		curTime -= 1;
		this.transform.Rotate (0,0,rotation);
		curRot -= rotation;
		if (fireOnUpdate && time != 0 && curTime <= 0) {
			//creating bullet
            FireBullet();
			//prepare for next shot
			curTime = time;
		}
		if (fireOnUpdate && fireAngle != 0 && curRot <= 0) {
			FireBullet ();
			curRot = fireAngle;
		}
	}
    public void FireBullet()
    {
        GameObject myBullet = (GameObject)GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet thisBullet = myBullet.GetComponent<Bullet>();
        float angle = this.transform.eulerAngles.z;
        if(myBullet.GetComponent<Rigidbody2D>() == null)
        {
            myBullet.AddComponent<Rigidbody2D>();
        }
        if(thisBullet == null )
        {
            thisBullet = myBullet.AddComponent<Bullet>();
            Debug.Log("Adding: " + thisBullet);
        }
        //setting bullet properties
        thisBullet.setDirection(angle);
        thisBullet.setDelta(bulletSpeed);
    }
}
