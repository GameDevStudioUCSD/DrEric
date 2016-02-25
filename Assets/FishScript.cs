using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour {


    private float starttime;
    public float checktime;
	// Use this for initialization
	void Start () {
   
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

        float degreeratio = Mathf.Atan(rigid.velocity.y / rigid.velocity.x);
        degreeratio = degreeratio / Mathf.PI;
        oldzrotation = 180 * degreeratio;

        if (rigid.velocity.x > 0) oldzrotation *= -1;


        m.eulerAngles = new Vector3(oldxrotation, oldyrotation, oldzrotation);
    }
}
