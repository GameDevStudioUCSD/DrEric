<<<<<<< HEAD:Assets/Scripts/World 2 Mechanics/Boss2Script.cs
﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Boss2Dialog))]
public class Boss2Script : MonoBehaviour {
    public enum State { TRACKING, BLOATING,MOVING,DANMAKU, DEFLATING, IDLE, DYING };
    public State state;
    public GameObject target;
    public GameObject missile;
    public float health = 3;
    public int hornNumber = 5;

	public float localScaleIncrementScalar = 1.005f;
	public float timeToBloat = 2;

    public float fireRate = 1f;
    public float chargingForceScalar = 10;
    public float invisibilityTime = 1;
    public Text healthLabel;
    public GameObject danmakuGenerator;


    private int hornsFired;
    private float maxHP;
    private float startTime;
    private float lastHitTime = 0;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Rigidbody2D myRigidBody;
    private Boss2Dialog dialogController;
    private string originalHealthLabel;
    private Color originalHealthColor;

    // Use this for initialization
    void Start () {
        state = State.TRACKING;
        hornsFired = 0;
        startTime = Time.time;
        maxHP = health;
        originalPosition = transform.position;
        originalScale = transform.localScale;
        myRigidBody = GetComponent<Rigidbody2D>();
        dialogController = GetComponent<Boss2Dialog>();
        // Setup health labels
        originalHealthLabel = healthLabel.text;
        originalHealthColor = healthLabel.color;
        UpdateHealth(new Color(0,0,0));
        ResetText();
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

    public void DecrementHornCount()
    {
        if (hornsFired > 0)
            hornsFired--;
    }

    void Track()
    {
        // fire a horn for every damage, starting 1
        if (RespawnController.IsDead())
            return;
        myRigidBody.velocity *= 0;
        if (Time.time - startTime > fireRate && hornsFired < hornNumber)
        {
            GameObject newMissile = Instantiate(this.missile);//make horn & initialize variables
            Missile missile = newMissile.GetComponent<Missile>();
            missile.boss = this;
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
        dialogController.TriggerKillingDialog();
        RunAway();
        if (( state != State.DANMAKU && health < maxHP) || (state == State.DANMAKU && health < 2 ))
        {
            health++;
            UpdateHealth(new Color(0,1,0));
        }
        hornsFired = 0;
    }

    void DestroyAllHorns()
    {
        hornsFired = 0;
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
        UpdateHealth(new Color(1,0,0));
        if (health <= 0)
        {
            health = Mathf.NegativeInfinity;
            state = State.DEFLATING;
            Destroy(danmakuGenerator);
            DestroyAllHorns();
            dialogController.TriggerDyingDialog();
            Invoke("EnterDyingState", 3);
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
            DestroyAllHorns();
            dialogController.TriggerFinalFormDialog();
            state = State.IDLE;
            Invoke("FireDanmaku", 5);
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
        state = State.DANMAKU;
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
    void EnterDyingState()
    {
        state = State.DYING;
    }
    void RunAway()
    {
        Vector2 runAwayForce = new Vector2(10, 10);
        myRigidBody.AddForce(runAwayForce, ForceMode2D.Impulse);
    }
    void UpdateHealth(Color color)
    {
        healthLabel.text = originalHealthLabel + health + "/" + maxHP;
        healthLabel.color = color;
        Invoke("ResetText", 1);
    }
    void ResetText()
    {
        healthLabel.color = originalHealthColor; 
    }

}
=======
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { TRACKING, BLOATING,MOVING,DANMAKU, DEFLATING };
    private State state;
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
        myRigidBody.velocity *= 0;
        if (RespawnController.IsDead())
            return;
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
            Destroy(this.gameObject,5);
        }
        else if (health > 2)
        {//if living go to next state   
            startTime = Time.time;
            if(state == State.TRACKING)
                state = State.BLOATING;
        }
        else
        {
            transform.localScale = originalScale;
            Vector2 runAwayForce = new Vector2(10, 10);
            myRigidBody.AddForce(runAwayForce, ForceMode2D.Impulse);
            state = State.DANMAKU;
            FireDanmaku();
        }
    }

	// called at every frame when state is BLOATING
    void Bloat()
    {
        transform.localScale = localScaleIncrementScalar * transform.localScale; //= new Vector3 (transform.localScale.x * scaleIncrement, 
			//transform.localScale.y * scaleIncrement, transform.localScale.z);
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
        //state = State.DEFLATING;
    }

	void ReturnToWaiting() {
		state = State.TRACKING;
	}

}
>>>>>>> origin/master:Assets/Scripts/World2Mechanics/Boss2Script.cs
