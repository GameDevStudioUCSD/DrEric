using UnityEngine;
using System.Collections;

public class PipePath : MonoBehaviour {
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;
	public const int OPENINGS = 4;


	public Sprite straight;
	public Sprite bend;
	public Sprite threeWay;
	public Sprite fourWay;

	// desired max height and width of the grid
	public int height;
	public int width;

	// the current height and width of the grid
	private int currentWidth, currentHeight;

	Pipe startPipe;
	int [] startPipeDir = new int[OPENINGS];
	int [] startPipeExit = new int[OPENINGS];
	int startPipeEnter;

	// Use this for initialization
	void Start () {

		// initializing the first pipe
		startPipeDir [LEFT] = 1;
		startPipeDir [RIGHT] = 1;
		startPipeExit [RIGHT] = 1;
		startPipeEnter = LEFT;

		startPipe = new Pipe (startPipeDir, startPipeEnter, startPipeExit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// places a pipe on the grid
	// place is the pipe to be places
	// connect is the pipe touching this pipe
	private void placePipe (Pipe place, Pipe connect)
	{
	}
}

