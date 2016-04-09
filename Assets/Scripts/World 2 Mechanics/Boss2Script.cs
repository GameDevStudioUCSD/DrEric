using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { TRACKING, BLOATING,MOVING,DANMAKU, DEFLATING };
    private State state;
    public GameObject target;
    public GameObject horn;
    public int health = 3;
    public int hornNumber = 5;

	public float scaleIncrement = 5;
	public float bloatTime = 2;

    public float hornDelay;
    public float hornInitialForce = 10;
    public float chargeScalar = 10;

    private ArrayList missileList = new ArrayList();

    private int maxHP;
    private float startTime;
    private int hornsFired;
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
        transform.localScale = (1.0f / scaleIncrement) * transform.localScale;
    }

    void Track()
    {
        // fire a horn for every damage, starting 1
        myRigidBody.velocity *= 0;
        if (RespawnController.IsDead())
            return;
        if (Time.time - startTime > hornDelay && hornsFired <= maxHP - health+hornNumber)
        {
            GameObject newMissile = Instantiate(horn);//make horn & initialize variables
            Missile missile = newMissile.GetComponent<Missile>();
            missile.enabled = true;
            missileList.Add(missile);
            newMissile.transform.position = horn.transform.position;
            newMissile.transform.rotation = horn.transform.rotation;
            hornsFired++;
            startTime = Time.time;
        }
    }

    public void Respawn()
    {
        DestroyAllHorns();
        health = maxHP;
        transform.position = originalPosition;
        transform.localScale = originalScale;
        startTime = Time.time;
        hornsFired = 0;
        state = State.TRACKING;
    }

    void DestroyAllHorns()
    {
        foreach (Missile m in missileList)
            m.BlowUp();
        missileList.Clear();
    }

    public void TakeDamage()//get hit
    {
        health--;
        if (state != State.TRACKING)
            return;
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
        else if (health > 1)
        {//if living go to next state   
            startTime = Time.time;
            state = State.BLOATING;
        }
        else
            state = State.DANMAKU;
    }

	// called at every frame when state is BLOATING
    void Bloat()
    {
        transform.localScale = scaleIncrement * transform.localScale; //= new Vector3 (transform.localScale.x * scaleIncrement, 
			//transform.localScale.y * scaleIncrement, transform.localScale.z);
		if (Time.time - startTime >= bloatTime) 
		{
			state = State.MOVING;
            startTime = Time.time;
		}
		hornsFired = 0;

    }

    void Move()
    {
        Vector3 direction = chargeScalar * (target.transform.position - transform.position).normalized;
        Debug.Log("Trying to move towards: " + direction);
        myRigidBody.AddForce(direction, ForceMode2D.Impulse);
		state = State.DEFLATING;
		Invoke ("ReturnToWaiting", bloatTime);
        
    }
    void FireDanmaku()
    {
        GetComponent<Danmaku>().enabled = true;
        state = State.DEFLATING;
    }
    /**void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Deadly")
        {
        }
    }*/

	void ReturnToWaiting() {
		state = State.TRACKING;
	}

}
