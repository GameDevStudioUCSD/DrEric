using UnityEngine;
using System.Collections;

public class moveRespawnerOnTeleport : MonoBehaviour {

    private Vector3 respawnerPresentPosition;
    private Vector3 respawnerPastPosition;
    public float respawnerPastX;
    public float respawnerPastY;
    private bool isInPresent;
    private RespawnController respawner;

    // Use this for initialization
    void Start () {
        isInPresent = true;
        respawner = RespawnController.GetRespawnController();
        respawnerPresentPosition = respawner.transform.position;
        respawnerPastPosition = new Vector3(respawnerPastX, respawnerPastY, 0);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void callOnTeleport()
    {
        if (isInPresent)
        {
            respawner.transform.position = respawnerPastPosition;
        }
        else
        {
            respawner.transform.position = respawnerPresentPosition;
        }
        isInPresent = !isInPresent;
        
    }
}
