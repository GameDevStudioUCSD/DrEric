using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {
    public enum State { WAITING, BLOATING,MOVING };
    private State state;
    public Vector3 originalPosition;
    public GameObject target;
    public GameObject horn;
    public int health = 3;
    public float horndelay;


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
    {
        if (Time.time -starttime > horndelay && hornsFired <= maxHP - health)
        {
            GameObject thishorn = Instantiate(horn);

        }
    }

    void Bloating()
    {

    }

    void Moving()
    {

    }
}
