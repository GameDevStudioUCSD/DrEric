using UnityEngine;
using System.Collections;

public class PlayMusicAfterAlarm : MonoBehaviour {

	// Use this for initialization
    AudioSource audioSource;
    RhythmController rhythmController;
    float alarmLength;
	void Start () {
        audioSource = GetComponent<AudioSource>();
        alarmLength = audioSource.clip.length;
        rhythmController = RhythmController.GetController();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > alarmLength) {
            rhythmController.PlaySong();
            enabled = false;
        }
        else
            rhythmController.StopSong();

	}
}
