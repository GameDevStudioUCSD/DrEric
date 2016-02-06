using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {

	// Use this for initialization
    BallController drEric;
    Animator controller;
    int maxJumps;
	void Start () {
        controller = GetComponent<Animator>();
        maxJumps = GameObject.Find(Names.SQUIDLAUNCHER).GetComponent<SquidLauncher>().maxJumps;
	}
	
	// Update is called once per frame
	void Update () {
        if (drEric == null)
            GetDrEric();
        else
            controller.SetInteger("Digit", maxJumps - drEric.GetJumps());
	}
    void GetDrEric()
    {
        GameObject obj = GameObject.Find(Names.DRERIC);
        if(obj != null)
            drEric = obj.GetComponent<BallController>();
    }
}
