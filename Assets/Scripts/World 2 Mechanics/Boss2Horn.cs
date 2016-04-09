using UnityEngine;
using System.Collections;

public class Boss2Horn : MonoBehaviour {

    public GameObject target;
    public float invulnerabilitytime;
    private int speed = 100;
    private Rigidbody2D myRigidbody;
    private PIDController pidController;

    public float explosiveRadius = 5.0F;
    public float explosionPower = 10.0F;
    enum State { LAUNCHING, TRACKING, BLOWINGUP}
    State state = State.LAUNCHING;
    void Start()
    {
        target = GameObject.Find(Names.PLAYERHOLDER);
        myRigidbody = GetComponent<Rigidbody2D>();
        if (myRigidbody == null)
            myRigidbody = gameObject.AddComponent<Rigidbody2D>();
        pidController = this.GetComponent<PIDController>();
        pidController.enabled = true;
        pidController.destinationTransform = target.transform;
        Invoke("Track", invulnerabilitytime);
    }

    void Update()
    {
        //code that makes it face where its going
        Vector3 vectorToTarget = myRigidbody.velocity;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    public void BlowUp()
    {
        state = State.BLOWINGUP;
        pidController.enabled = false;
        this.GetComponentInChildren<Animator>().SetBool("Exploded", true);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(this.gameObject,1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (state != State.TRACKING)
            return;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosiveRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionPower, explosionPos, explosiveRadius, 3.0F);
        }
        myRigidbody.velocity *= 0;
        BlowUp();
            //Boss2Script boss = other.gameObject.GetComponent<Boss2Script>();
            //boss.TakeDamage();
    }

    void Track()
    {
        state = State.TRACKING;
    }



}
