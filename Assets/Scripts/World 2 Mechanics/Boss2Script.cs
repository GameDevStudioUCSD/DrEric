using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boss2Script : MonoBehaviour {
    public enum State { WAITING, BLOATING,MOVING };
    private State state;
    public Vector3 originalPosition;
    public GameObject target;
    public GameObject horn;
    public int health = 3;

	public float SCALE_INCREMENT = 5;
	public float BLOAT_TIME = 2;

    public float horndelay;
    public float horninitialforce = 10;

    private ArrayList hornlist = new ArrayList();

    private int maxHP;
    private float starttime;
    private int hornsFired;


    // Use this for initialization
    void Start () {
        state = State.WAITING;
        hornsFired = 0;
        starttime = Time.time;
        maxHP = health;
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
               
        }
	}

    void Waiting()
    {// fire a horn for every damage, starting 1

        if (Time.time - starttime > horndelay && hornsFired <= maxHP - health)
        {
            GameObject firedhorn = Instantiate(horn);//make horn & initialize variables
            Boss2Horn hornscript = firedhorn.GetComponent<Boss2Horn>();
            hornlist.Add(hornscript);
            firedhorn.transform.position = horn.transform.position;
            firedhorn.transform.rotation = horn.transform.rotation;
            hornscript.target = target;
            hornscript.Fired = true;
            hornscript.boss = this;
            hornscript.starttime = Time.time;

            float impulseradians = horn.transform.rotation.eulerAngles.z;//fire horn out of head
            firedhorn.GetComponent<Rigidbody2D>().AddForce(new Vector2(-horninitialforce * Mathf.Cos(impulseradians), -horninitialforce * Mathf.Cos(impulseradians)), ForceMode2D.Impulse);

            hornsFired++;
            starttime = Time.time;
        }
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
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {//if living go to next state   
            starttime = Time.time;
            state = State.BLOATING;
        }
    }

	// called at every frame when state is BLOATING
    void Bloating()
    {
		transform.localScale = new Vector3 (transform.localScale.x * SCALE_INCREMENT, 
			transform.localScale.y * SCALE_INCREMENT, transform.localScale.z);

		if (Time.time - starttime >= BLOAT_TIME) 
		{
			state = State.MOVING;
		}

    }

    void Moving()
    {

    }
}
