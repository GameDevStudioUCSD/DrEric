using UnityEngine;
using System.Collections;

public class FlingObject : MonoBehaviour {

    public GameObject obj;
    public float forceScalar = 1;
    public float yScalar = 1;
    public float piScalar;
    public Vector3 initalVector, finalVector, deltaVector;
	void Start () {
	
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            initalVector = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            finalVector = Input.mousePosition;
            Fling();
        }
	}

    private void Fling()
    {
        // Calculate the delta vector
        deltaVector = finalVector - initalVector;
        deltaVector *= forceScalar;
        // We want to know the parent object's y rotation in radians
        float yDirection = transform.rotation.eulerAngles.y * Mathf.PI / 180;
        yDirection -= (Mathf.PI * piScalar);
        float forceMagnitude = deltaVector.magnitude;
        // To make the vector definition cleaner, temporary variables are used
        float x = Mathf.Sin(yDirection) * forceMagnitude;
        float z = Mathf.Cos(yDirection) * forceMagnitude;
        //yDirection += (Mathf.PI * piScalar);
        Vector3 forceVector = new Vector3(x, forceMagnitude, z);
        // Instantiate the game object to be flung
        GameObject objRuntime = (GameObject)GameObject.Instantiate(obj);
        objRuntime.transform.position = transform.position;
        // Add the rigid body to the object if it doesn't exist
        Rigidbody rigidBody = objRuntime.GetComponent<Rigidbody>();
        if (rigidBody == null)
            rigidBody = objRuntime.AddComponent<Rigidbody>();
        rigidBody.AddForce(forceVector, ForceMode.Impulse);
    }
}
