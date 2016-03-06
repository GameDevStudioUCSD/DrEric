using UnityEngine;
using System.Collections;

public class Boss2Horn : MonoBehaviour {

    public float starttime;
    public GameObject target;
    public Boss2Script boss;
    public float invulnerabilitytime;
    public bool Fired;
    private int speed = 100;
    private Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
        if(Fired)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Fired)
        {   //turn on PIDcontroller when fired
            PIDController pid = this.GetComponent<PIDController>();
            pid.enabled = true;
            pid.destinationTransform = target.transform;

            //code that makes it face where its going
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            Vector3 vectorToTarget = rigid.velocity;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle-90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss") && Time.time -starttime > invulnerabilitytime )
        {
            Debug.Log(other);
            boss.hit();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player" )
        {
        }
        else if(other.tag == "Wall")
        {
            myRigidbody.velocity *= 0;
        }
    }

}
