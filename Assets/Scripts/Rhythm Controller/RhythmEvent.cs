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

class RhythmEvent : MonoBehaviour{
	public NoteDivision noteDivision; // selection of note value for event to occur on
	public int noteSeparation = 1; // the note distance between events
	public int measureSeparation = 1; // the measure distance between events
    public UnityEvent m_MyEvent;
	
    void Start()
    {
        RhythmController controller = RhythmController.GetController();
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

    }
	
	/**
     * Description: The event that occurs when an object triggers it in gameplay.
     * To be implemented by the object that triggers the event.
     */
//	void TriggerEvent();
}