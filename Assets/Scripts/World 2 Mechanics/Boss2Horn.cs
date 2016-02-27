using UnityEngine;
using System.Collections;

public class Boss2Horn : MonoBehaviour {

    public GameObject target;
    public bool Fired;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Fired)
        {
            PIDController pid = this.GetComponent<PIDController>();
            pid.enabled = true;
            pid.destinationTransform = target.transform;

        }
    }
}
