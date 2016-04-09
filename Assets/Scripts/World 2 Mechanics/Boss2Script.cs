using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { TRACKING, BLOATING,MOVING,DANMAKU, DEFLATING };
    public State state;
    public GameObject target;
    public GameObject missile;
    public int health = 3;
    public int hornNumber = 5;

	public float localScaleIncrementScalar = 1.005f;
	public float timeToBloat = 2;

    public float fireRate = 1f;
    public float chargingForceScalar = 10;
    public float invisibilityTime = 1;
    public GameObject danmakuGenerator;


    private int maxHP;
    private float startTime;
    private int hornsFired;
    private float lastHitTime = 0;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start () {
        state = State.TRACKING;
        hornsFired = 0;
        startTime = Time.time;
        maxHP = health;
        originalPosition = transform.position;
        originalScale = transform.localScale;
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.TRACKING:
                Track();
                break;
            case State.BLOATING:
                Bloat();
                break;
            case State.MOVING:
                Move();
                break;
            case State.DANMAKU:
                FireDanmaku();
                break;
            case State.DEFLATING:
                Deflate();
                break;
            default:
                break;
               
        }
	}

    void Deflate()
    {
        transform.localScale = (1.0f / localScaleIncrementScalar) * transform.localScale;
    }

    void Track()
    {
        // fire a horn for every damage, starting 1
        if (RespawnController.IsDead())
            return;
        myRigidBody.velocity *= 0;
        if (Time.time - startTime > fireRate && hornsFired <= maxHP - health+hornNumber)
        {
            GameObject newMissile = Instantiate(this.missile);//make horn & initialize variables
            Missile missile = newMissile.GetComponent<Missile>();
            missile.enabled = true;
            newMissile.transform.position = this.missile.transform.position;
            newMissile.transform.rotation = this.missile.transform.rotation;
            hornsFired++;
            startTime = Time.time;
        }
    }

    public void Respawn()
    {
        DestroyAllHorns();
        RunAway();
        if (health < maxHP || (state == State.DANMAKU && health < 2 ))
            health++;
        hornsFired = 0;
    }

    void DestroyAllHorns()
    {
        foreach (Missile missile in FindObjectsOfType(typeof(Missile)) as Missile[])
        {
            missile.PrepareExplosion();
        }
    }

    public void TakeDamage()//get hit
    {
        if (Time.time - lastHitTime < invisibilityTime)
            return;
        lastHitTime = Time.time;
        health--;
        if (health <= 0)
        {
            state = State.DEFLATING;
            Destroy(danmakuGenerator);
            DestroyAllHorns();
            Destroy(this.gameObject, 5);
        }
        else if (health > Mathf.Ceil(maxHP * .2f))
        {//if living go to next state   
            if(state == State.TRACKING)
            {
                startTime = Time.time;
                state = State.BLOATING;
            }
        }
        else
        {
            transform.localScale = originalScale;
            state = State.DANMAKU;
            FireDanmaku();
        }
    }

	// called at every frame when state is BLOATING
    void Bloat()
    {
        transform.localScale = localScaleIncrementScalar * transform.localScale;
		if (Time.time - startTime >= timeToBloat) 
		{
			state = State.MOVING;
            startTime = Time.time;
		}
		hornsFired = 0;

    }

    void Move()
    {
        Vector3 direction = chargingForceScalar * (target.transform.position - transform.position).normalized;
        myRigidBody.AddForce(direction, ForceMode2D.Impulse);
		state = State.DEFLATING;
		Invoke ("ReturnToWaiting", timeToBloat);
    }
    void FireDanmaku()
    {
        danmakuGenerator.GetComponent<Danmaku>().enabled = true;
        if(Time.time - startTime > 1 )
        {
            startTime = Time.time;
            RunAway();
        }
    }

    void ReturnToWaiting() {
		state = State.TRACKING;
	}
    void RunAway()
    {
        Vector2 runAwayForce = new Vector2(10, 10);
        myRigidBody.AddForce(runAwayForce, ForceMode2D.Impulse);
    }

}
