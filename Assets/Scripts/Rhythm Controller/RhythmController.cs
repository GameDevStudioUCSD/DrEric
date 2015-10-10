using UnityEngine;
using System.Collections;

/**
 * Filename: RhythmController.cs
 * Author: Jason Wu
 * Contributing Authors: N/A
 * Date Drafted: 10/10/2015
 * Description: Plays and monitors the MusicalTrack, and calls
 * 				the RhythmEvents.
 */
public class RhythmController : MonoBehaviour {

	/*Need MusicalTrack.cs*/
	public MusicalTrack[] musicList;

	/*Need RhythmEvent.cs*/
	RhythmEvent[] eventList;

	int currentMeasure;
	int currentBeat;

	/*Internals*/
	MusicalTrack currentTrack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayMusicalTrack(int track, int measure = 0, int beat = 0){
		//musicList[track].PlayAt(measure, beat);
		currentTrack = musicList[track];
	}

	/**
	 * Function Signature: void SetCurrentMeasure(int measure);
     * Description: Setter method for variable currentMeasure.
     */
	void SetCurrentMeasure(int measure){
		currentMeasure = measure;
	}

	/**
	 * Function Signature: void SetCurrentBeat(int beat);
     * Description: Setter method for variable currentBeat.
     */
	void SetCurrentBeat(int beat){
		currentBeat = beat;
	}

	/**
	 * Function Signature: int GetCurrentMeasure();
     * Description: Getter method for currentMeasure.
     */
	int GetCurrentMeasure(){
		return currentMeasure;
	}

	/**
	 * Function Signature: int GetCurrentBeat();
     * Description: Getter method for currentBeat.
     */
	int GetCurrentBeat(){
		return currentBeat;
	}
}
