using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { WAITING, BLOATING,MOVING,DANMAKU, NONE };
    private State state;
    public GameObject target;
    public GameObject horn;
    public int health = 3;
    public int hornNumber = 5;

	public float SCALE_INCREMENT = 5;
	public float BLOAT_TIME = 2;

    public float horndelay;
    public float horninitialforce = 10;
    public float chargeScalar = 10;

    private ArrayList hornlist = new ArrayList();

    private int maxHP;
    private float starttime;
    private int hornsFired;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start () {
        state = State.WAITING;
        hornsFired = 0;
        starttime = Time.time;
        maxHP = health;
        originalPosition = transform.position;
        originalScale = transform.localScale;
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.WAITING:
                Waiting();
                break;
            case State.BLOATING:
                Bloating();
                break;
            case State.MOVING:
                Moving();
                break;
            case State.DANMAKU:
                DanmakuState();
                break;
            default:
                break;
               
        }
	}

    void Waiting()
    {// fire a horn for every damage, starting 1
        myRigidBody.velocity *= 0;
        if (RespawnController.IsDead())
            return;
        if (Time.time - starttime > horndelay && hornsFired <= maxHP - health+hornNumber)
        {
            GameObject firedhorn = Instantiate(horn);//make horn & initialize variables
            Boss2Horn hornscript = firedhorn.GetComponent<Boss2Horn>();
            hornlist.Add(hornscript);
            firedhorn.transform.position = horn.transform.position;
            firedhorn.transform.rotation = horn.transform.rotation;
            hornscript.target = target;
            hornscript.Fired = true;
            //hornscript.boss = this;
            hornscript.starttime = Time.time;

            float impulseradians = horn.transform.rotation.eulerAngles.z;//fire horn out of head
            Vector2 force = new Vector2(horninitialforce * Mathf.Abs(Mathf.Cos(impulseradians)),
                horninitialforce * Mathf.Abs(Mathf.Cos(impulseradians)))*transform.localScale.y;
            Debug.Log(impulseradians);
            Debug.Log(Mathf.Cos(impulseradians));
            Debug.Log(force);
            hornsFired++;
            starttime = Time.time;
            firedhorn.AddComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }
    }

    public void Respawn()
    {
        destroyAllHorns();
        health = maxHP;
        transform.position = originalPosition;
        transform.localScale = originalScale;
        starttime = Time.time;
        hornsFired = 0;
        state = State.WAITING;
    }

    void destroyAllHorns()
    {
        foreach (Boss2Horn horn in hornlist)
        {
            horn.Destroy();
        }
        hornlist.Clear();
    }

    public void hit()//get hit
    {
        health--;
        destroyAllHorns();
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
        else if (health > 1)
        {//if living go to next state   
            starttime = Time.time;
            state = State.BLOATING;
        }
        else
            state = State.DANMAKU;
    }

	// called at every frame when state is BLOATING
    void Bloating()
    {
		transform.localScale = new Vector3 (transform.localScale.x * SCALE_INCREMENT, 
			transform.localScale.y * SCALE_INCREMENT, transform.localScale.z);

		if (Time.time - starttime >= BLOAT_TIME) 
		{

			state = State.MOVING;
            transform.position = originalPosition;
            transform.localScale = originalScale;
            starttime = Time.time;
		}

    }

    void Moving()
    {
        Vector3 direction = chargeScalar * (target.transform.position - transform.position).normalized;
        Debug.Log("Trying to move towards: " + direction);
        myRigidBody.AddForce(direction, ForceMode2D.Impulse);
        state = State.WAITING;
    }
    void DanmakuState()
    {
        GetComponent<Danmaku>().enabled = true;
        state = State.NONE;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Deadly")
        {
        }
    }
}
