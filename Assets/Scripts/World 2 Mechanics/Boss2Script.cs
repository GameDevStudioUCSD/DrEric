using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { TRACKING, BLOATING,MOVING,DANMAKU, NONE };
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

    private ArrayList hornList = new ArrayList();

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
            default:
                break;
               
        }
	}

    void Track()
    {
        // fire a horn for every damage, starting 1
        myRigidBody.velocity *= 0;
        if (RespawnController.IsDead())
            return;
        if (Time.time - startTime > hornDelay && hornsFired <= maxHP - health+hornNumber)
        {
            GameObject firedhorn = Instantiate(horn);//make horn & initialize variables
            Boss2Horn hornscript = firedhorn.GetComponent<Boss2Horn>();
            hornList.Add(hornscript);
            firedhorn.transform.position = horn.transform.position;
            firedhorn.transform.rotation = horn.transform.rotation;
            hornscript.target = target;
            hornscript.Fired = true;
            //hornscript.boss = this;
            hornscript.starttime = Time.time;

            float impulseradians = horn.transform.rotation.eulerAngles.z;//fire horn out of head
            Vector2 force = new Vector2(hornInitialForce * Mathf.Abs(Mathf.Cos(impulseradians)),
                hornInitialForce * Mathf.Abs(Mathf.Cos(impulseradians)))*transform.localScale.y;
            Debug.Log(impulseradians);
            Debug.Log(Mathf.Cos(impulseradians));
            Debug.Log(force);
            hornsFired++;
            startTime = Time.time;
            firedhorn.AddComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

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
        foreach (Boss2Horn horn in hornList)
        {
            //horn.GetComponentInChildren<Animator>().SetBool("Exploded", true);
            //horn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
            //float timepenis = Time.time; int x = 0;
            //if (Time.time - timepenis > 5)
            //{
                horn.Destroy();
            //}
        }
        hornList.Clear();
    }

    public void TakeDamage()//get hit
    {
        health--;
        DestroyAllHorns();
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
		transform.localScale = new Vector3 (transform.localScale.x * scaleIncrement, 
			transform.localScale.y * scaleIncrement, transform.localScale.z);

		if (Time.time - startTime >= bloatTime) 
		{

			state = State.MOVING;
     //       transform.position = originalPosition;
            transform.localScale = originalScale;
            startTime = Time.time;
		}
		hornsFired = 0;

    }

    void Move()
    {
        Vector3 direction = chargeScalar * (target.transform.position - transform.position).normalized;
        Debug.Log("Trying to move towards: " + direction);
        myRigidBody.AddForce(direction, ForceMode2D.Impulse);
		state = State.NONE;
		Invoke ("ReturnToWaiting", 2);
        
    }
    void FireDanmaku()
    {
        GetComponent<Danmaku>().enabled = true;
        state = State.NONE;
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
