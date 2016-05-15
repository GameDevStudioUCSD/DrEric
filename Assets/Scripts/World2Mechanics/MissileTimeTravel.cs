using Assets.Scripts.World2Mechanics;
using UnityEngine;

public class MissileTimeTravel : MonoBehaviour {
    public GameObject target;
    public float invulnerabilitytime;
    public float explosiveRadius = 1F;
    public float explosionPower = 1F;
    public float initialForce = 2;
    public Boss2Script boss;
    public int life = 100;

    private int speed = 100;
    private float angle = 0f;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private Rigidbody2D myRigidbody;
    private PIDController pidController;
    private Collider2D myCollider;
    private AudioSource audioSrc;

    private enum State {
        LAUNCHING,
        TRACKING,
        BLOWINGUP
    }

    private State state = State.LAUNCHING;

    void Start() {
        startingPosition = this.transform.position;
        target = GameObject.Find(Names.PLAYERHOLDER);
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y);
        var y = target.transform.position.y - this.transform.position.y;
        var x = target.transform.position.x - this.transform.position.x;
        angle = Mathf.Atan2(y, x);
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        audioSrc = GetComponent<AudioSource>();
        if (myRigidbody == null)
            myRigidbody = gameObject.AddComponent<Rigidbody2D>();
        myRigidbody.AddForce(initialForce * transform.right, ForceMode2D.Impulse);
        pidController = this.GetComponent<PIDController>();
        pidController.destinationTransform = target.transform;
        Invoke("Track", invulnerabilitytime);
    }

    void Update() {
        transform.Translate(.1f * Mathf.Cos(angle),.1f * Mathf.Sin(angle), 0);

        if (--life < 0) {
            PrepareExplosion();
        }

        switch (state) {
            //case State.BLOWINGUP:
            //    myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            //    break;
            default:
                UpdatePose();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        //PrepareExplosion();
        Debug.Log("Trigger Enter! on" + other);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "WallFloor" || col.gameObject.tag == "Gate") {
            PrepareExplosion();
        }
    }

    public void PrepareExplosion() {
        if (state != State.TRACKING)
            return;
        pidController.enabled = false;
        this.GetComponentInChildren<Animator>().SetBool("Exploded", true);
        state = State.BLOWINGUP;
        Invoke("Explode", .2f);
    }

    public void Explode() {
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosiveRadius);
        audioSrc.Play();
        foreach (Collider2D hit in colliders) {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null) {
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
        if (boss != null)
            boss.DecrementHornCount();
        Destroy(this.gameObject, .5f);
    }

    //code that makes it face where its going
    private void UpdatePose() {
        Vector3 vectorToTarget = myRigidbody.velocity;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    private void Track() {
        state = State.TRACKING;
        pidController.enabled = true;
        myCollider.enabled = true;
    }
}