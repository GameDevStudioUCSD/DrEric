/* Header comments here
 */

using UnityEngine;
using System.Collections;

public class MusicalTrack : MonoBehaviour
{
    public int bpm;
    public int timeSigUpper;
    public int timeSigLower;
    public AudioSource song;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

 /*   void Play()
    {
        //two parameters - please add
        song.PlayOneShot();
    }

    void PlayAt(int measure, int beat)
    {
        double time;
        song.PlayScheduled(time);
    }
}*/
