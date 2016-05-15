using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeIndicator : MonoBehaviour
{
    public bool isPast = false;
    public const string present = "Present";
    public const string past = "Past";
    public Text text;
    private moveRespawnerOnTeleport respawnerMoveScript;

    void start()
    {
        respawnerMoveScript = RespawnController.GetRespawnController().GetComponent<moveRespawnerOnTeleport>();
    }


    public void ToggleTime()
    {
        isPast = !isPast;
        text.text = isPast ? past : present;
    }

    public void ForcePresent()
    {
        //If respawner is possibly in past, use the state from the move script instead
        if (!respawnerMoveScript)
        {
            if (respawnerMoveScript.getIfPresent())
            {
                text.text = present;
                isPast = false;
            }
            else
            {
                text.text = past;
                isPast = true;
            }
        }
        else
        {
           if (!isPast) return;
            isPast = false;
            text.text = present;
        }
    }
}