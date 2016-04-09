using UnityEngine;
using System.Collections;

public class Boss2Horn : MonoBehaviour {

    public GameObject target;
    public float invulnerabilitytime;
    public float explosiveRadius = 5.0F;
    public float explosionPower = 10.0F;

    private int speed = 100;
    private Rigidbody2D myRigidbody;
    private PIDController pidController;
    private enum State { LAUNCHING, TRACKING, BLOWINGUP}
    private State state = State.LAUNCHING;

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
        if (state != State.TRACKING)
            return;
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosiveRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            Debug.Log(hit + " got hit by the explosion!");
            if (rb != null) {
                // First calculate the direction
                Vector2 explosiveForce = hit.transform.position - explosionPos;
                // Normalize it and apply scalar
                explosiveForce = explosionPower * explosiveForce.normalized;
                // Apply it
                rb.AddForce(explosiveForce, ForceMode2D.Impulse);
            }
        }
        myRigidbody.velocity *= 0;
        BlowUp();
            //Boss2Script boss = other.gameObject.GetComponent<Boss2Script>();
            //boss.TakeDamage();
    }

    private void BlowUp()
    {
        state = State.BLOWINGUP;
        pidController.enabled = false;
        this.GetComponentInChildren<Animator>().SetBool("Exploded", true);
        Destroy(this.gameObject,.5f);
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
    }



}
