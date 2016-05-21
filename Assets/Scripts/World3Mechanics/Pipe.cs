/**********************************************
 * Pipe.cs
 * 
 * author: Ajeya Rengarajan
 * 
 * Defines a Pipe game object
 * *********************************************/
using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;
	public const int OPENINGS = 4;

	private SpriteRenderer spriterenderer;
	Sprite straight;
	Sprite bend;
	Sprite threeWay;
	Sprite fourWay;

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

	/*********************************************************
	 * Initializers
	 *********************************************************/


	public void initPipe (int [] dir, int enterDir, int [] exit, Sprite [] sprites)
	{
		this.dir = dir;
		this.enterDir = enterDir;
		Start ();
		straight = sprites [0];
		bend = sprites [1];
		threeWay = sprites [2];
		fourWay = sprites [3];
	}

	// returns a random pipe with certain number of openings
	// and a certain directions
	public void initPipe (int enterDir, Sprite [] sprites)
	{
		dir = new int [OPENINGS];
		dir [enterDir] = 1;
		this.enterDir = enterDir;

		// 1/2/3 exits
		numExits = (int)Random.Range (1, OPENINGS);

		straight = sprites [0];
		bend = sprites [1];
		threeWay = sprites [2];
		fourWay = sprites [3];
	
		exit = new int[OPENINGS];
		setExitDirections ();
		Start ();
	}



	/*********************************************************
	 * Start and Update
	 *********************************************************/


	// Use this for initialization
	void Start () {
		spriterenderer = GetComponent <SpriteRenderer> ();
		setSprite ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		


	/*********************************************************
	 * Other helper functions
	 *********************************************************/


	// sets random directions to the exit array
	public void setExitDirections ()
	{
		int index = 0;
		int randomDir;
		while (index < numExits) {

			// get a random directionand set it
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


	// the pipe has 2/3/4 openings
	// this will set the sprite based on number of openings
	public void setSprite () {
		int openings = numExits + 1;
		Debug.Log (openings);

		switch (openings) {
			

		case 2:
			Debug.Log ("Called");
			// if straight pipe
			if ((dir [UP] == 1 && dir [DOWN] == 1) || (dir [LEFT] == 1 && dir [RIGHT] == 1)) {
				
				spriterenderer.sprite = straight;
				if (dir [LEFT] == 1 && dir [RIGHT] == 1)
					rotate (90);
				
			} 

			// if bend
			else {

				spriterenderer.sprite = bend;
				if (dir [RIGHT] == 1 && dir [DOWN] == 1)
					rotate (90);
				if (dir [DOWN] == 1 && dir [LEFT] == 1)
					rotate (180);
				if (dir [UP] == 1 && dir [LEFT] == 1)
					rotate (270);
			}
			break;


		case 3: // three way pipe
			spriterenderer.sprite = threeWay;
			break;
		
		case 4: // four way pipe
			spriterenderer.sprite = fourWay;
			break;

		}

	}


	// sets to a certain pipe
	public void setSprite (Sprite sprite) {
		spriterenderer.sprite = sprite;
	}


	// rotates clockwise 'angle' degrees
	public void rotate (int angle) {
		transform.Rotate(new Vector3(0, 0, angle));
	}
}
