using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class StageBanner : MonoBehaviour {

	void setStageName(string name) {
		GameObject nameTransform = GameObject.FindGameObjectWithTag("StageTitle");
		Text stageName = nameTransform.GetComponent<Text> ();
		stageName.text = name;
	}

	void setStageScore(string score){
		GameObject scoreTransform = GameObject.FindGameObjectWithTag("StageScore");
		Text scoreValue = scoreTransform.GetComponent<Text> ();
		scoreValue.text = name;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
