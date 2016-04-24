using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class CameraMover : MonoBehaviour {
    public enum MoveAfter { TimePasses, EventFired }
    public MoveAfter movementType = MoveAfter.TimePasses;
    public float returnTime = 2;
    public Camera otherCamera;

    private Camera savedCamera;

    // Unity Methods
    void Start()
    {
        savedCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D col )
    {
        if (col.tag != "Player")
            return;
        switch (movementType)
        {
            case MoveAfter.TimePasses:
                ActivateOtherCamera();
                Invoke("ReactivateSavedCamera", returnTime);
                break;
            case MoveAfter.EventFired:
                ActivateOtherCamera();
                break;
        }
        this.gameObject.SetActive(false);
    }

    // Public Methods
    public void ReactivateSavedCamera()
    {
        savedCamera.enabled = true;
        otherCamera.gameObject.SetActive(false);
    }
    public void ActivateOtherCamera()
    {
        savedCamera.enabled = false;
        otherCamera.gameObject.SetActive(true);
    }

    // Sets the Platform "zoomer" to zoom out with speed equal to the 
    // time the camera stays switched
    private void ConfigureCamera()
    {
        Platform cameraZoomer = otherCamera.GetComponent<Platform>();
        if (cameraZoomer != null)
            cameraZoomer.movementTime = returnTime;
    }
}
