using UnityEngine;
using System.Collections;

public class ParadoxCounter : MonoBehaviour {

	public int paradoxCount;
	public Level thisLevel;
	Level nextLevel;
	public VictoryController portal; 

	// Use this for initialization
	void Start () {
		nextLevel = portal.nextLevel;
	}
	
	// Update is called once per frame
	void Update () {
		if (paradoxCount > 0) {
			portal.nextLevel = thisLevel;
		} else {
			portal.nextLevel = nextLevel;
		}
	}

	public void Decrement() {
		paradoxCount--;
	}

	public void Increment() {
		paradoxCount++;
	}
}
