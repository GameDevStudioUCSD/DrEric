using UnityEngine;
using System.Collections;

public class RecursiveMaze : MazeGenerator {
    private const int MINSPACE = 1;
    private bool hasEntrance, hasExit;
    public RecursiveMaze(int r, int c)
	{
        this.Rows = r;
        this.Cols = c;
	}
	override public void run(Square[,] cells, Square end)
    {
        walls = cells;
        exit = end;
        createSquares(false);
        constructOutterWalls();
        generateMaze(0, 0, Rows, Cols);
        if (!hasExit )
        {
            walls[0, 0].exit = true;
            hasExit = true;

        }
    }
    private void constructOutterWalls()
    {
        for (int r = 0; r < Rows; r++)
        {
            walls[r, 0].hasWest = true;
            walls[r, Cols - 1].hasEast = true;
        }
        for(int c = 0; c < Cols; c++)
        {
            walls[0, c].hasNorth = true;
            walls[Rows - 1, c].hasSouth = true;
        }
    }
    
    private void generateMaze(int r0, int c0, int rN, int cN)
    {
        if (((rN) - (r0)) > MINSPACE && ((cN) - (c0)) > MINSPACE)
        {
            int rR = Random.Range(r0 + 1, rN - 1);
            int cR = Random.Range(c0 + 1, cN - 1);
            for (int r = r0; r < rN; r++)
            {
                walls[r, cR].hasWest = true;
            }
            for (int c = c0; c < cN; c++)
            {
                walls[rR, c].hasNorth = true;
            }
            generateMaze(r0, c0, rR, cR);
            generateMaze(rR, c0, rN, cR);
            generateMaze(r0, cR, rR, cN);
            generateMaze(rR, cR, rN, cN);
            int omitDoor = randomEdge();
            if (omitDoor != NORTH) {
				int r = Random.Range(r0, rR);
                walls[r, cR].hasWest = false;
			}
            if (omitDoor != SOUTH) {
				int r = Random.Range(rR, rN);
                walls[r, cR].hasWest = false;
			}
            if (omitDoor != WEST) {
				int r = Random.Range (c0, cR);
                walls[rR, r].hasNorth = false;
			}
            if (omitDoor != EAST) {
				int r = Random.Range (cR, cN);
                walls[rR, r].hasNorth = false;
			}
        }
        else if (!hasEntrance && r0 < (Rows * .2) && c0 < (Cols * .2))
        {
            walls[r0, c0].start = true;
            hasEntrance = true;
        }
            
           
    }
}
