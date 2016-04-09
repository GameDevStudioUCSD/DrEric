using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public GameObject target;
    public float invulnerabilitytime;
    public float explosiveRadius = 5.0F;
    public float explosionPower = 10.0F;
    public float initialForce = 2;

    private int speed = 100;
    private Rigidbody2D myRigidbody;
    private PIDController pidController;
    private Collider2D myCollider;
    private enum State { LAUNCHING, TRACKING, BLOWINGUP}
    private State state = State.LAUNCHING;

    void Start()
    {
        target = GameObject.Find(Names.PLAYERHOLDER);
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        if (myRigidbody == null)
            myRigidbody = gameObject.AddComponent<Rigidbody2D>();
        myRigidbody.AddForce(initialForce* transform.right, ForceMode2D.Impulse);
        pidController = this.GetComponent<PIDController>();
        pidController.destinationTransform = target.transform;
        Invoke("Track", invulnerabilitytime);
    }

    void Update()
    {
        switch(state)
        {
            case State.BLOWINGUP:
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            default:
                UpdatePose();
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PrepareExplosion();
    }

    public void PrepareExplosion()
    {
        if (state != State.TRACKING)
            return;
        pidController.enabled = false;
        this.GetComponentInChildren<Animator>().SetBool("Exploded", true);
        state = State.BLOWINGUP;
        Invoke("Explode", .2f);
    }

    public void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosiveRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // First calculate the direction
                Vector2 explosiveForce = hit.transform.position - explosionPos;
                // Normalize it and apply scalar
                explosiveForce = explosionPower * explosiveForce.normalized;
                // Apply it
                rb.AddForce(explosiveForce, ForceMode2D.Impulse);
            }
            MissileListener listener = hit.GetComponent<MissileListener>();
            if (listener != null)
                listener.eventList.Invoke();
        }

        Destroy(this.gameObject, .5f);
    }

    //code that makes it face where its going
    private void UpdatePose()
    {
        Vector3 vectorToTarget = myRigidbody.velocity;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    private void Track()
    {
        state = State.TRACKING;
        pidController.enabled = true;
        myCollider.enabled = true;
    }



}
