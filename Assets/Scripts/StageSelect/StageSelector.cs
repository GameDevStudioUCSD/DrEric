using UnityEngine;
using System.Collections;

public class StageSelector : MonoBehaviour {

    public GameObject stageCamera;
    public GameObject drEric;
    public GameObject stageGroup;

    public float ballTorque = 1.5f;
    public float cameraSpeed = 1f;

    private int currentStage;
    private Transform target = null;
    private StageCameraController cameraController = null;
    private StageBallController ballController = null;

    public void NextStage()
    {
        // Check if not on last stage
        if (currentStage < stageGroup.transform.childCount)
        {
            currentStage++;
            Transform targetStage = stageGroup.transform.GetChild(currentStage -1);
            cameraController.SetTarget(targetStage);
            ballController.SetTarget(targetStage);
            ballController.moveRight(ballTorque);
            ballController.SetRight(true);
        }
        Debug.Log("Moving to stage " + currentStage);
    }

    public void PrevStage()
    {
        // Check if not on first stage
        if (currentStage - 1 > 0)
        {
            currentStage--;
            Transform targetStage = stageGroup.transform.GetChild(currentStage -1);
            cameraController.SetTarget(targetStage);
            ballController.SetTarget(targetStage);
            ballController.moveLeft(ballTorque);
            ballController.SetRight(false);
        }
        Debug.Log("Moving to stage " + currentStage);
    }

    // Use this for initialization
    void Start () {
        currentStage = 1;
        cameraController = stageCamera.GetComponent<StageCameraController>();
        ballController = drEric.GetComponent<StageBallController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
