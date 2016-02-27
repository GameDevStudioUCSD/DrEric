using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {


    private float starttime;
    public float checktime;
	private float dietime;
    public float secondsAliveOutOfWater;
    public int health;
    public FishSchool schoolOfFish;

	// Use this for initialization
	void Start () {
		dietime = Time.time;
        starttime = Time.time;
        //Debug.Log(currentpos);

	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Transform m = transform;
        float oldxrotation = transform.rotation.eulerAngles.x;
        float oldyrotation = transform.rotation.eulerAngles.y;
        float oldzrotation = transform.rotation.eulerAngles.z;

        if (rigid.velocity.x > 0 && oldyrotation != 180) oldyrotation = 180;
        else oldyrotation = 0;

        float degreeratio = Mathf.Atan2(rigid.velocity.y , rigid.velocity.x);
        degreeratio = degreeratio / Mathf.PI;
        oldzrotation = 180 * degreeratio;

        if (rigid.velocity.x > 0) oldzrotation *= -1;

        m.eulerAngles = new Vector3(oldxrotation, oldyrotation, oldzrotation);
		if (Time.time - dietime > secondsAliveOutOfWater)
		{
			if (schoolOfFish != null)
			{
				schoolOfFish.KillAFish();
			}
            Destroy(this.gameObject);
        }
    }

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Water")
        {
            dietime = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    	if (col.gameObject.tag == "Player")
    	{
    		health--;
    		if (health <= 0)
    		{
				if (schoolOfFish != null)
				{
					schoolOfFish.KillAFish();
				}
           		Destroy(this.gameObject);
    		}
    	}
    }
}
