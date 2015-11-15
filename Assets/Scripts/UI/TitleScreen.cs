using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public GameObject blinkMessage;
	public GameObject buttonStart;
	public GameObject buttonLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void enableButtons(){
		buttonStart.SetActive(true);
		buttonLoad.SetActive(true);
	}

	public void disableButtons(){
		buttonStart.SetActive(false);
		buttonLoad.SetActive(false);
	}

	public void enableBlinkMessage(){
		blinkMessage.SetActive(true);
	}

	public void disableBlinkMessage(){
		blinkMessage.SetActive(false);
	}

}