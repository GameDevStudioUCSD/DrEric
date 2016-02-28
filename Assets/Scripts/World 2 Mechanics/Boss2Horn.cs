using UnityEngine;
using System.Collections;

public class Boss2Horn : MonoBehaviour {

    public GameObject target;
    public bool Fired;
    private int speed = 10;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Fired)
        {
            PIDController pid = this.GetComponent<PIDController>();
            pid.enabled = true;
            pid.destinationTransform = target.transform;


            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            Vector3 vectorToTarget = rigid.velocity;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
            Debug.Log(transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z < 180 && transform.localScale.y < 0) transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y);
            if (transform.rotation.eulerAngles.z > 180 && transform.localScale.y > 0) transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y);
        }
    }
}
