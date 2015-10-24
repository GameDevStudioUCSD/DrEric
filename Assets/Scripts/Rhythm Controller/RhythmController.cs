using UnityEngine;
using System.Collections;

/**
 * Filename: RhythmController.cs \n
 * Author: Jason Wu \n
 * Contributing Authors: N/A \n
 * Date Drafted: 10/10/2015 \n
 * Description: Plays and monitors the MusicalTrack, and calls \n
 * 				the RhythmEvents. \n
 */
public class RhythmController : MonoBehaviour {
    private static const string NAME = "RhythmController";
	private float wholeNote;
	private float halfNote;
	private float quarterNote;
	private float eigthNote;
    private float sixteenthNote;
    private float tripleWholeNote;
    private float tripleHalfNote;
    private float tripleQuarterNote;
    private float tripleEigthNote;

	/*Need MusicalTrack.cs*/
	public MusicalTrack[] musicList;

    public int songIndex = 0;
    public float errorMargin = 1f;

	/*Need RhythmEvent.cs*/
	RhythmEvent[] eventList;

    static RhythmController singleton = null;

	int currentMeasure;
	int currentBeat;

	/*Internals*/
	MusicalTrack currentTrack;

	double startTime;


    public static RhythmController GetController()
    {
        GameObject controller = GameObject.Find(NAME);
        return controller.GetComponent<RhythmController>();
    }
    /**
	 * Function Signature: void Awake();
     * Description: Ensures that there is only one RhythmController.
     */
    void Awake() {
        if (singleton != null && singleton != this){
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
        this.name = NAME;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SetNoteLengths() { /** multiply by 1000 to convert to milliseconds*/
        wholeNote = 60f * 1000f / currentTrack.bpm * 4;
        halfNote = 60f * 1000f / currentTrack.bpm * 2;
        quarterNote = 60f * 1000f / currentTrack.bpm;
        eigthNote = 60f * 1000f / currentTrack.bpm / 2;
        sixteenthNote = 60f * 1000f / currentTrack.bpm / 4;
        tripleWholeNote = 60f * 1000f / currentTrack.bpm * 4 / 3;
        tripleHalfNote = 60f * 1000f / currentTrack.bpm * 2 / 3;
        tripleQuarterNote = 60f * 1000f / currentTrack.bpm / 3;
        tripleEigthNote = 60f * 1000f / currentTrack.bpm / 6;
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
	/** workaround to margins of error involving modulo with floating point numbers*/
	bool WithinErrorMargin( float modResult, float noteLength ){
		if (modResult < errorMargin || noteLength - modResult < errorMargin) {
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
