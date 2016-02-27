using UnityEngine;
using System.Collections;

public class Boss2Script : MonoBehaviour {
    public enum State { WAITING, BLOATING,CHARGING };
    private State state;
    public Vector3 originalPosition;
    public GameObject target;
    private int hornsFired;
    public int health = 3;
	// Use this for initialization
	void Start () {
        state = State.WAITING;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
