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

		placePipe ();

	}
	
	// Update is called once per frame
	void Update () {
		

		return;
	}


	// places a pipe on the grid
	// place is the pipe to be places
	// connect is the pipe touching this pipe
	private void placePipe ()
	{
		int i, j;
		Pipe script;
		Vector3 position;

		for (i = 0; i < height; i++) {
			for (j = 0; j < width; j++) {

				if (i == 0 && j == 0)
					continue;

				grid [i, j] = GameObject.Instantiate (piece);
				script = grid [i, j].GetComponent<Pipe> ();
				script.initPipe (Random.Range (0, 4), pipesprites);
				script.setSprite ();
				position = new Vector3 (i * 0.3f, j * 0.3f);
				grid [i, j].transform.position = position;

			}
		}
	}
}

