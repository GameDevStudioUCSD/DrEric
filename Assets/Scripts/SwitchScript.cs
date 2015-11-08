using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SwitchScript : MonoBehaviour {

    public UnityEvent Event;
	// Use this for initialization
	void Start () {
        Event.AddListener(doSomething);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Switch triggered");
            Event.Invoke();
        }
    }
    void doSomething()
    {
        Debug.Log("somethng");
    }
}
