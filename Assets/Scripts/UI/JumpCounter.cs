using UnityEngine;
using System.Collections;

public class JumpCounter : MonoBehaviour {

	// Use this for initialization
    BallController drEric;
    RespawnController spawner;
    Animator controller;
    int maxJumps;
	void Start () {
        controller = GetComponent<Animator>();
        maxJumps = GameObject.Find(Names.SQUIDLAUNCHER).GetComponent<SquidLauncher>().maxJumps;
        spawner = GameObject.Find(Names.RESPAWNER).GetComponent<RespawnController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetDrEric();
        if (drEric != null)
            controller.SetInteger("Digit", maxJumps - drEric.GetJumps());
	}
    void GetDrEric()
    {
        GameObject obj = spawner.GetDrEric();
        if(obj != null)
            drEric = obj.GetComponent<BallController>();
    }
}
