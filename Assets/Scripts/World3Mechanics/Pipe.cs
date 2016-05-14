using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;
	public const int OPENINGS = 4;

	private Sprite sprite;
	private SpriteRenderer spriterenderer;

	// valid directions in the pipe
	// dir[LEFT] == 1 if the pipe has a left dir
	int [] dir;

	// direction in which the pipe is entered
	int enterDir;

	// directions in which you leave this pipe
	// parallel array to dir, if exit[DOWN] == 1
	// you can exit in this direction
	int [] exit; 

	// number of openings the pipe has
	int numExits;

	// Use this for initialization
	void Start () {
		spriterenderer = GetComponent <SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	public Pipe (int [] dir, int enterDir, int [] exit)
	{
		this.dir = dir;
		this.enterDir = enterDir;
		Start ();
	}

	// returns a random pipe with certain number of openings
	// and a certain directions
	public Pipe (int enterDir)
	{

		dir = new int [OPENINGS];
		dir [enterDir] = 1;
		this.enterDir = enterDir;

		// 1/2/3 exits
		numExits = (int)Random.Range (1, OPENINGS);

		exit = new int[OPENINGS];
		setExitDirections ();
		Start ();
	}

	// sets random directions to the exit
	public void setExitDirections ()
	{
		int index = 0;
		int randomDir;
		while (index < numExits) {
			randomDir = Random.Range (0, 4);
			if (dir [randomDir] != 1) {
				dir [randomDir] = 1;
				exit [randomDir] = 1;
				index++;
			}
			else {
				continue;
			}
		}

	}


	public void setSprite ()
	{
		// sets sprite based on number of openings and direction
	}
}
