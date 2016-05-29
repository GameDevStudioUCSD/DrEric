/**********************************************
 * PipePath.cs
 * 
 * author: Ajeya Rengarajan
 * 
 * Generates a maze of random pipes
 * *********************************************/

using UnityEngine;
using System.Collections;

public class PipePath : MonoBehaviour {
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;
	public const int OPENINGS = 4;


	public Sprite [] pipesprites = new Sprite [4];

	public GameObject piece;

	// desired max height and width of the grid
	public int height;
	public int width;

	// the current height and width of the grid
	private int currentWidth, currentHeight;


	// the first pipe
	GameObject startPipe;
	int [] startPipeDir = new int[OPENINGS];
	int [] startPipeExit = new int[OPENINGS];
	int startPipeEnter;

	// the array of pipes
	GameObject [,] grid;

	// Use this for initialization
	void Start () {

		// initializing the first pipe
		startPipeDir [LEFT] = 1;
		startPipeDir [RIGHT] = 1;
		startPipeExit [RIGHT] = 1;
		startPipeEnter = LEFT;

		startPipe = GameObject.Instantiate (piece);
		startPipe.GetComponent<Pipe> ().initPipe (startPipeDir,startPipeEnter,startPipeExit, pipesprites);
		startPipe.GetComponent<Pipe> ().setSprite (pipesprites[0]);
		startPipe.GetComponent<Pipe> ().rotate (90);
		startPipe.transform.position = new Vector3 (0, 0, 0);

		grid = new GameObject[height, width];
		grid [0,0] = startPipe;

		placePipe (1, 0, LEFT);

	}
	
	// Update is called once per frame
	void Update () { return; }

	// places a pipe on the grid
	// place is the pipe to be places
	// connect is the pipe touching this pipe
	private void placePipe (int x, int y, int enterDir)
	{
		int i;
		int[] exit;
		int direction;

		Pipe script;
		Vector3 position; 

		// *******************************************
		// base cases 
		// *******************************************


		if (x < 0 || x >= height)
			return;

		if (y < 0 || y >= width)
			return;

		if (grid [x, y] != null)
			return;

		/************************************************/


		// initializing game object pipe 
		grid [x, y] = GameObject.Instantiate (piece);

		// getting the script 
		script = grid [x, y].GetComponent<Pipe> ();

		// initializing and setting sprite
		script.initPipe (enterDir, pipesprites);
		script.setSprite ();

		// setting position
		position = new Vector3 (x * 0.3f, y * -0.3f);
		grid [x, y].transform.position = position;

		// getting the exit directions and flipping to get the new
		// enter directions for the new pipes
		exit = script.getExitDirections ();
		exit = flipExits (exit);

		for (i = 0; i < exit.Length; i++) {

			if (exit [i] == 1) {

				// recursive calls based on directions
				switch (i) {

				case UP:
					placePipe (x + 1, y, UP);
					break;

				case DOWN:
					placePipe (x - 1, y, DOWN);
					break;

				case LEFT:
					placePipe (x, y - 1, LEFT);
					break;

				case RIGHT:
					placePipe (x, y + 1, RIGHT);
					break;

				}
			}
		}


	}


	private int [] flipExits (int [] exits)
	{
		int temp;

		// swapping left/right and up/down 

		temp = exits [DOWN];
		exits [DOWN] = exits [UP];
		exits [UP] = temp;

		temp = exits [LEFT];
		exits [LEFT] = exits [RIGHT];
		exits [RIGHT] = temp;

		return exits;
	}
}

