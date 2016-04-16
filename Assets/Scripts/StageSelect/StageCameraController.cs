using UnityEngine;
using System.Collections;

public class StageCameraController : MonoBehaviour {

	private int currentStage;
	public float speed = 1f;

    public GameObject drEric;
	public GameObject stageGroup;
	private Transform target = null;
    private StageBallController ballController = null;

	public void SetTarget(Transform stage){
		this.target = stage;
	}

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

	public void Start() {

	}

	void Update(){

		if (target != null) {
			transform.position = Vector3.Lerp (transform.position, new Vector3(target.position.x,target.position.y,transform.position.z), speed*Time.deltaTime);
			// Check if target is reached
			if (transform.position.x == target.position.x){
				Debug.Log ("Target Reached ");
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
}