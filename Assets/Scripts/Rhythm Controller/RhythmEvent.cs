using UnityEngine;
using UnityEngine.Events; // We want the event system built into Unity!
using System.Collections;

/**
 * Filename: RhythmEvent.cs \n
 * Author: Sean Wenzel \n
 * Contributing Authors: Michael Gonzalez \n
 * Date Drafted: 10/10/2015 \n
 * Description:  Event that is “observing” the clock for a call. 
 */

public class RhythmEvent : MonoBehaviour{
	public NoteDivision noteDivision; // selection of note value for event to occur on
    //public NoteDivision noteOffset;
	//public int noteSeparation = 1; // the note distance between events
    [Range(1,20)]
	public int measureSeparation = 1; // the measure distance between events
    public UnityEvent OnEvent;

    private RhythmController controller;
    private float lastInvokeTime = 0;
    void Start()
    {
        if (OnEvent == null)
            OnEvent = new UnityEvent();

    }
    void Update()
    {
        if (controller == null)
        {
            controller = RhythmController.GetController();
            if (controller != null)
            {
                controller.RegisterEvent(this);
            }
        }
    }
    public void TestEvent()
    {
        Debug.Log("I'm an event!");
    }
    public float GetLastInvokeTime()
    {
        return lastInvokeTime;
    }
    public void SetLastInvokeTime(float t)
    {
        lastInvokeTime = t;
    }
	
	/**
     * Description: The event that occurs when an object triggers it in gameplay.
     * To be implemented by the object that triggers the event.
     */
//	void TriggerEvent();
}