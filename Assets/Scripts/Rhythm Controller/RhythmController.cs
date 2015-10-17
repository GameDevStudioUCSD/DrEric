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

	private float quarterNote;
	private float halfNote;
	private float wholeNote;
	private float eigthNote;
    private float tripleEigthNote;

	/*Need MusicalTrack.cs*/
	public MusicalTrack[] musicList;

    public int songIndex = 0;

	/*Need RhythmEvent.cs*/
	RhythmEvent[] eventList;

    static RhythmController singleton;

	int currentMeasure;
	int currentBeat;

	/*Internals*/
	MusicalTrack currentTrack;

	double startTime;


    /**
	 * Function Signature: void Awake();
     * Description: Ensures that there is only one RhythmController.
     */
    void Awake() {
        if (singleton != null){
            Destroy(this.gameObject);
            return;
        } else {
            singleton = this;
        }
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
		startTime = AudioSettings.dspTime;
        currentTrack = musicList[songIndex];
        SetNoteLengths();
		DebugLengths ();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SetNoteLengths() { /*multiply by 1000 to convert to milliseconds*/
        quarterNote = 60f * 1000f / currentTrack.bpm;
        halfNote = 60f * 1000f / currentTrack.bpm * 2;
        eigthNote = 60f * 1000f / currentTrack.bpm / 2;
        wholeNote = 60f * 1000f / currentTrack.bpm * 4;
        tripleEigthNote = 60f * 1000f / currentTrack.bpm / 3;
    }

	void DebugLengths(){
		Debug.Log ("quarter note " + quarterNote);
		Debug.Log ("half note " + halfNote);
		Debug.Log ("eigth note " + eigthNote);
		Debug.Log ("whole note " + wholeNote);
		Debug.Log ("triple eigth note" + tripleEigthNote);
		Debug.Log ((300000%tripleEigthNote));
		Debug.Log (WithinErrorMargin (300000 % tripleEigthNote, tripleEigthNote));
	}
	bool WithinErrorMargin( float modResult, float noteLength ){
		if (modResult < 1 || noteLength - modResult < 1) {
			return true;
		}
		return false;
	}
    /*
        void PlayMusicalTrack(int track, int measure = 0, int beat = 0){
            musicList[track].PlayAt(measure, beat);
            currentTrack = musicList[track];
            startTime = AudioSettings.dspTime;
        }
    */
    /**
     * OUTDATED
	 * Function Signature: int[] ConvertToSongPosition(float time);
     * Description: Converts the current time into the song into the number of
     * measures and beats into the song.
     * Returns:     int[] of size 2 that contains the measure and beat.
     *              int[0] contains the measure.
     *              int[1] contains the beat.
     */
    /*public int[] ConvertToSongPosition(float time){
		int bpm = currentTrack.bpm;
		double current = currentTrack.source.time;
		int[] output = new int[2];
		output[0] = (int)(current*bpm/240f);
		output[1] = (int)((current*bpm % 240f)*bpm/60f);
		return output;
	}*/

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
