using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeIndicator : MonoBehaviour
{
    public bool isPast = false;
    public const string present = "Present";
    public const string past = "Past";
    public Text text;

    public void ToggleTime()
    {
        isPast = !isPast;
        text.text = isPast ? past : present;
    }

    public void ForcePresent()
    {
        if (!isPast) return;
        isPast = false;
        text.text = present;
    }
}