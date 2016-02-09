using UnityEngine;
using System.Collections;

public class PIDController : MonoBehaviour {
    /**
     * FileName: PIDController.cs
     * Author: Michael Gonzalez
     * Date Drafted: 2/9/2016
     * Description: A Generic proportional–integral–derivative controller 
     *              We can use this as a an alternative to lerping when 
     *              the moving object may collide with another object
     * Underlying Phyics: https://en.wikipedia.org/wiki/PID_controller
     */

    public enum TrackingType { Transform, Vector }

    public float kP;
    public float kI;
    public float kD;

    public float maximumImpulse;
    public float minimumImpulse;

    public float impuseRate;

    public TrackingType trackingType = TrackingType.Transform;
    public Transform destinationTransform;
    public Vector3 destinationVector;

    private Vector3 previousError;
    private Vector3 currError;
	void Start () {
	
	}
	
	void Update () {
	
	}

    void CalculateError()
    {
        Vector3 dest = destinationVector;
        switch(trackingType)
        {
            case TrackingType.Transform:
                dest = destinationTransform.position;
                break;
            case TrackingType.Vector:
                dest = destinationVector;
                break;
        }
        currError = dest - transform.position;
    }
}
