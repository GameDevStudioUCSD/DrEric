using UnityEngine;
using System.Collections;

public class SecurityGate : MonoBehaviour {

	public GameObject gateCollider;
	public GameObject gateInitialSprite;
	public GameObject gateDestroyedSprite;


	// Use this for initialization
	void Start () {
		gateDestroyedSprite.SetActive(false);
	}
	
	// Update is called once per frame
	public void DestroyGate () {
		gateInitialSprite.SetActive(false);
		gateDestroyedSprite.SetActive(true);
		gateCollider.SetActive(false);
	}

}
