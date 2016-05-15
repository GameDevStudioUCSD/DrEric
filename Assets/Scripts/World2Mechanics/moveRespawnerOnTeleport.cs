using UnityEngine;
using System.Collections;

public class moveRespawnerOnTeleport : MonoBehaviour  {

    private Vector3 respawnerPresentPosition;
    private Vector3 respawnerPastPosition;
    public float pastTranslateX;
    public float pastTranslateY;
    private bool isInPresent;
    private RespawnController respawner;

    // Use this for initialization
    void Start () {
        isInPresent = true;
        respawner = RespawnController.GetRespawnController();
        respawnerPresentPosition = respawner.transform.position;
        respawnerPastPosition = new Vector3(respawnerPresentPosition.x + pastTranslateX, respawnerPresentPosition.y + pastTranslateY, 0);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public bool getIfPresent()
    {
        return isInPresent;
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
