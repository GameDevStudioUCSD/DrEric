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

	public const float quarterNote = 24f;
	public const float halfNote = 48f;
	public const float wholeNote = 96f;
	public const float eigthNote = 12f;
	public const float tripleEigthNote = 8f;

	/*Need MusicalTrack.cs*/
	public MusicalTrack[] musicList;

	/*Need RhythmEvent.cs*/
	RhythmEvent[] eventList;

	int currentMeasure;
	int currentBeat;

	/*Internals*/
	MusicalTrack currentTrack;

	double startTime;

	// Use this for initialization
	void Start () {
		startTime = AudioSettings.dspTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
/*
	void PlayMusicalTrack(int track, int measure = 0, int beat = 0){
		musicList[track].PlayAt(measure, beat);
		currentTrack = musicList[track];
		startTime = AudioSettings.dspTime;
	}
*/
	public int[] ConvertToSongPosition(float time){
		int bpm = currentTrack.bpm;
		double current = currentTrack.source.time;
		int[] output = new int[2];
		output[0] = current*bpm/240f;
		output[1] = (current*bpm % 240f)*bpm/60f;
	}

	/**
	 * Function Signature: void SetCurrentMeasure(int measure);
     * Description: Setter method for variable currentMeasure.
     */
	public void SetCurrentMeasure(int measure){
		currentMeasure = measure;
	}

	/**
	 * Function Signature: void SetCurrentBeat(int beat);
     * Description: Setter method for variable currentBeat.
     */
	public void SetCurrentBeat(int beat){
		currentBeat = beat;
	}

	/**
	 * Function Signature: int GetCurrentMeasure();
     * Description: Getter method for currentMeasure.
     */
	public int GetCurrentMeasure(){
		return currentMeasure;
	}

	/**
	 * Function Signature: int GetCurrentBeat();
     * Description: Getter method for currentBeat.
     */
	public int GetCurrentBeat(){
		return currentBeat;
	}
}
