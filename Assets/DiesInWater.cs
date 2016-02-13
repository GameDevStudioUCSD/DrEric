using UnityEngine;
using System.Collections;

public class DiesInWater : MonoBehaviour {

    private float dietime;
    public float SecondsAliveOutOfWater;
	// Use this for initialization
	void Start () {
        dietime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time - dietime > SecondsAliveOutOfWater)
            Destroy(this.gameObject);
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Water")
        {
            Debug.Log("PENIS");
            dietime = Time.time;
        }
    }
}
