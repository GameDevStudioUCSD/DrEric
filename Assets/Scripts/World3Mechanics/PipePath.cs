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
		int i, j, k, newi, newj;
		int[] exit;
		int direction;
		Pipe script;
		Vector3 position; 

		i = 0;
		j = 0;

		while (true){
				script = grid [i, j].GetComponent<Pipe> ();
				exit = script.getExitDirections ();
				exit = flipExits (exit);

				for (k = 0; k < exit.Length; k++) {

					direction = exit [k];	
					newi = i;
					newj = j;

					if (direction == 1) {
						switch (k) {
						case UP:
							if (i + 1 < height) {
								newi = i + 1;
							}
							break;
						case DOWN:
							if (i - 1 >= 0) {
								newi = i - 1;
							}
							break;
						case LEFT:
							if (j - 1 >= 0) {
								newj = j - 1;
							}
							break;

						case RIGHT:
							if (j + 1 < width) {
								newj = j + 1;
							}
							break;
						}
					}

					if (grid [newi, newj] == null) {
						Debug.Log ("Placing tile at " + newi + "," + newj);
						grid [newi, newj] = GameObject.Instantiate (piece);
					}
					script = grid [newi, newj].GetComponent<Pipe> ();
					script.initPipe (k, pipesprites);
					script.setSprite ();
					position = new Vector3 (newi * 0.3f, newj * 0.3f);
					grid [newi, newj].transform.position = position;
				}
			}
	}

	private int [] flipExits (int [] exits)
	{
		int i;
		for (i = 0; i < exits.Length; i++) {
			if (exits [i] == UP) {
				exits [i] = DOWN;
				continue;
			}
			if (exits [i] == DOWN) {
				exits [i] = UP;
				continue;
			}
			if (exits [i] == LEFT) {
				exits [i] = RIGHT;
				continue;
			}
			if (exits [i] == RIGHT) {
				exits [i] = LEFT;
				continue;
			}
		}

		return exits;
	}
}

