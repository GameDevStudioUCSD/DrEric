using UnityEngine;
using System.Collections;

/**
 * Filename: RhythmEvent.cs \n
 * Author: Sean Wenzel \n
 * Contributing Authors: N/A \n
 * Date Drafted: 10/10/2015 \n
 * Description:  Event that is “observing” the clock for a call. 
 * Data fields from the UML are implemented as C# Properties.
 */

interface RhythmEvent {

	// the specified measures of a measure cycle that contains the event
	int[] specifiedMeasures {
		get;
		set;
	}
	// the size of a measure cycle
	int measureGroupSize {
		get;
		set;
	}
	// the specified beats within a specified measure that will call the event
	int[] specifiedBeats {
		get;
		set;
	}
	
	/**
     * Description: The event that occurs when an object triggers it in gameplay.
     * To be implemented by the object that triggers the event.
     */
	void TriggerEvent();
}