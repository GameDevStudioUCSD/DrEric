﻿using UnityEngine;
using System.Collections;

public class ChangeGravity : MonoBehaviour {

    public Vector2 Gravity = (-9.8f) * Vector2.down;
    public bool redirectMomentum = true;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerStay2D (Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Player")
        {
            go.GetComponent<BallController>().Land();
            ResetGravity();
            Physics2D.gravity = Gravity;
            if(redirectMomentum)
                RedirectMomentum.Redirect(go.GetComponent<Rigidbody2D>(), Gravity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Player")
        {
            go.GetComponent<BallController>().Land();
            ResetGravity();
            Physics2D.gravity = Gravity;
            if (redirectMomentum)
                RedirectMomentum.Redirect(go.GetComponent<Rigidbody2D>(), Gravity);
        }
    }

    public void ChangeTest()
    {
        Physics2D.gravity = Gravity;
    }


    public void ResetGravity()
    {
        Physics2D.gravity = -9.81f * Vector2.up;
    }
}
