using UnityEngine;
using System.Collections;

public class StageCameraController : MonoBehaviour {

	//public int MAX_LEVEL = 4;
	//public bool state = false;
	//public float offset = 100;

	private int currentStage;
	public float speed = 0.0001f;

	public GameObject panelGroup;
	private Transform target = null;

	// Camera shifting using animations
	/*
	private ArrayList<Animation> animations = new ArrayList<Animation>;
	public void ShiftRight()
	{
		//Debug.Log ("shifting right");
		//Camera.main.transform.Translate (offset, 0, 0);
		Animation animation = GetComponent<Animation>();
		if (levelState < MAX_LEVEL) {
			animation.Play ("levelTransitionRight" + levelState);
			levelState++;
			Debug.Log ("Shifting screens");
		} else {
			Debug.Log ("No shit");
		}
		//state = false;
	}

	public void ShiftLeft()
	{
		Animation animation = GetComponent<Animation>();
		if (levelState > 1) {
			animation.Play ("levelTransitionLeft" + levelState);
			levelState--;
			Debug.Log ("Shit");
		} else {
			Debug.Log ("No shit");
		}
	}
	*/

	public void Start() {
		currentStage = 1;
		target = null;

	}

	private void SetTarget(int stage){
		this.target = panelGroup.transform.GetChild(stage - 1);
	}

	public void NextStage(){
		// Check if not on last stage
		if(currentStage < panelGroup.transform.childCount){
			SetTarget (currentStage + 1);
			currentStage++;
		}
	}

	public void PrevStage(){
		// Check if not on first stage
		if(currentStage - 1 > 0){
			SetTarget (currentStage - 1);
			currentStage--;
		}
	}

	void Update(){

		if (target != null) {
			Debug.Log ("Setting  to " + currentStage);
			transform.position = Vector3.Lerp (transform.position, new Vector3(target.position.x,target.position.y,transform.position.z), speed*Time.deltaTime);
			Debug.Log ("Moved");
			// Check if target is reached
			if (transform.position.Equals(target.position)){
				target = null;
			}
		}else{
			//Debug.Log ("Taking inputs");
			// Move camera via arrow keys
			if(Input.GetKey(KeyCode.RightArrow)){
				transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
			}
		}
	}	
}