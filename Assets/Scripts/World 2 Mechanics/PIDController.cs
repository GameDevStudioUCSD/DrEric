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

    public float kP = 1.0f;
    public float kI = 1.0f;
    public float kD = 1.0f;

    public float maximumImpulse = 1.0f;
    public float minimumImpulse = 0f;

    public float impuseRate;

    public TrackingType trackingType = TrackingType.Transform;
    public Transform destinationTransform;
    public Vector3 destinationVector;

    private Vector3 previousError;
    private Vector3 currError;
    private float lastUpdate;
	void Start () {
        lastUpdate = Time.time;
	}
	
	void Update () {
        if(Time.time - lastUpdate > impuseRate)
        {
            Vector3 impulseVect = CalculatePID();

        }
	}

    Vector3 CalculatePID()
    {
        CalculateError();
        Vector3 res = currError;
        return kP * currError;
    }

    public static Vector3 ClampVector3( Vector3 v, float minMagnitude, float maxMagnitude )
    {
        Vector3 res = v;
        res = res.magnitude > maxMagnitude ? maxMagnitude * v.normalized : res; 
        res = res.magnitude < minMagnitude ? minMagnitude * v.normalized : res; 
        return res;
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
