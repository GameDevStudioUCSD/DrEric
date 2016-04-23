using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PIDController))]
public class Flock : MonoBehaviour {

    public enum type { LEADER, FOLLOWER }
    public float personalSpaceRadius = 1;
    public float flockingRadius = 1;
    public float flockingPreference = 4;
    public float avoidancePreference = 2;


    private Rigidbody2D rb;
    private PIDController pid;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        pid = GetComponent<PIDController>();
	}
	
	// Update is called once per frame
	void Update () {
        pid.destinationVector = transform.position + DeterminePath();
	}
    Vector3 DeterminePath()
    {
        Vector2 path = Vector2.zero;
        Vector2 flockPath = Vector2.zero;
        int flockSize = 0;
        Collider2D[] objsInPersonalSpace = Physics2D.OverlapCircleAll(transform.position, personalSpaceRadius);
        Collider2D[] flock = Physics2D.OverlapCircleAll(transform.position, flockingRadius);
        foreach(Collider2D obstacle in objsInPersonalSpace )
        {
            Flock other = obstacle.GetComponent<Flock>();
            if (other == null)
                continue;
            Vector2 diff = transform.position - obstacle.transform.position;
            path += diff.normalized;
        }
        foreach(Collider2D friend in flock)
        {
            Flock other = friend.GetComponent<Flock>();
            if (other == null)
                continue;
            flockPath += other.rb.velocity.normalized;
            flockPath += (Vector2)(other.transform.position - transform.position).normalized;
            flockSize++;
        }
        flockPath = (flockPath * (1.0f / flockSize)).normalized;
        path = path.normalized;
        return ((flockingPreference * flockPath) + (avoidancePreference * path)).normalized;
    }
}
