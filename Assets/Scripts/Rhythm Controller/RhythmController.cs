using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Filename: RhythmController.cs \n
 * Author: Jason Wu \n
 * Contributing Authors: N/A \n
 * Date Drafted: 10/10/2015 \n
 * Description: Plays and monitors the MusicalTrack, and calls \n
 * 				the RhythmEvents. \n
 */
public class RhythmController : MonoBehaviour {
    private const string NAME = "RhythmController";
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
    private List<int> measureKeys;
    private SortedDictionary<int, List<float>> measureTimeKeys;
    private SortedDictionary<int, SortedDictionary<float, List<RhythmEvent>>> events;

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
        // Create collection objects
        measureKeys = new List<int>();
        measureTimeKeys = new SortedDictionary<int, List<float>>();
        events = new SortedDictionary<int, SortedDictionary<float, List<RhythmEvent>>>();

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
    public void RegisterEvent(RhythmEvent e)
    {
        float noteLength = GetNoteLength(e.noteDivision);
        int measure = e.measureSeparation;
        // Add measure key to measure list
        if (!measureKeys.Contains(measure)) {
            measureKeys.Add(measure);
        }
        // Instantiate potentially null containers
        if (!measureTimeKeys.ContainsKey(measure) ) {
            measureTimeKeys[measure] = new List<float>();
        }
        List<float> timeKeysList = measureTimeKeys[measure];
        // Associate timekey to a measure
        if (!timeKeysList.Contains(noteLength)) {
            timeKeysList.Add(noteLength);
        }
        // Instantiate potentially null containers
        if (!events.ContainsKey(measure)) {
            events[measure] = new SortedDictionary<float, List<RhythmEvent>>();
        }
        SortedDictionary<float, List<RhythmEvent>> eventsAtMeasure = events[measure];
        if (!eventsAtMeasure.ContainsKey(noteLength)) {
            eventsAtMeasure[noteLength] = new List<RhythmEvent>();
        }
        List<RhythmEvent> eventList = eventsAtMeasure[noteLength];
        // Finally, we add the event
        eventList.Add(e);

    }
    float GetNoteLength(NoteDivision note)
    {
        switch (note)
        {
            case NoteDivision.eighthNote:
                return eigthNote;
            case NoteDivision.halfNote:
                return halfNote;
            case NoteDivision.quarterNote:
                return quarterNote;
            case NoteDivision.sixteenthNote:
                return sixteenthNote;
            case NoteDivision.tripleEigthNote:
                return tripleEigthNote;
            case NoteDivision.tripleQuarterNote:
                return tripleQuarterNote;
            case NoteDivision.tripleHalfNote:
                return tripleHalfNote;
            case NoteDivision.tripleWholeNote:
                return tripleWholeNote;
        }
        return -1; // something really bad happened if we get here
    }
    /*
        void PlayMusicalTrack(int track, int measure = 0, int beat = 0){
            musicList[track].PlayAt(measure, beat);
            currentTrack = musicList[track];
            startTime = AudioSettings.dspTime;
        }
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
