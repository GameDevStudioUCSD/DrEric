using UnityEngine;
using System.Collections;

/**
 * Filename: RhythmEvent.cs \n
 * Author: Sean Wenzel \n
 * Contributing Authors: N/A \n
 * Date Drafted: 10/10/2015 \n
 * Description:  Event that is “observing” the clock for a call. Originally
 * designed to be an interface, but is currently implemented as a class because
 * C# interfaces do not support fields.
 */

public class RhythmEvent : MonoBehaviour {
	
	int[] specifiedMeasures;   // the specified measures of a measure cycle that contains the event
	int measureGroupSize;      // the size of a measure cycle
	int[] specifiedBeats;      // the specified beats within a specified measure that will call the event
	
	/**
     * Description: to be implemented, does whatever event needs to do
     */
	void triggerEvent();
}