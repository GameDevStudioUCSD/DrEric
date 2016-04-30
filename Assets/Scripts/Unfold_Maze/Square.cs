using System;
using System.Collections;
/* @author: Michael Gonzalez
 * This class represents one Square inside the wall matrix. By default, all 
 * walls are considered "up" or "on"
 */
public class Square
{
    public bool visited { get; set; } // Has this cell been visited yet?
    public bool start; // Is this the start point for the player?
    public bool exit; // Is this the maze exit?
    public bool hasNorth {get; set;} // Is there a wall to the North?
    public bool hasSouth { get; set; } // Is there a wall to the South?
    public bool hasWest { get; set; } // Is there a wall to the West?
    public bool hasEast { get; set; } // Is there a wall to the East?

    //(Can only be accessed through getters)
    private int row; // R index in walls matrix 
    private int col; // C index in walls matrix

    public int numOfWalls; // Number of walls adjacent to this cell
    public float weight; // Value to help determine type and chance within the
                          // spawning methods

	public float light;

    // You can be creative with this one. It ensures both adjacent walls are
    // destroyed in the DepthFirst algorithm. For example, say the next wall to 
    // destroy is to the North of the current square. In this algorithm, this 
    // variable provides space to store the fact the north wall in the current
    // square can be destroyed and the south wall in the square above it is 
    // destroyed as well.
    private int wallToDestroy;
	public Square(int r, int c)
        :this(r,c,true)
	{
        
	}
    public Square(int r, int c, bool wallsUp)
    {
        row = r;
        col = c;
        hasNorth = wallsUp;
        hasSouth = wallsUp;
        hasWest = wallsUp;
        hasEast = wallsUp;
        weight = 1;

    }
    public static float DistanceBetween( Square A, Square B)
    {
        float returnVal, row, col;
        col = A.getCol() - B.getCol();
        row = A.getRow() - B.getRow();
        col *= col;
        row *= row;
        returnVal = (float)Math.Sqrt(row + col);
        return returnVal;
    }
    // Returns an int that signifies which quadrant a cell in a cell matrix is
    // Quadrants will be defined as such:
    // 0,0 [2] [1] 0,C
    // R,0 [3] [4] R,C
    public static int DetermineQuadrant( Square cell, Square[,] cells )
    {
        int retVal = 1;
        if (cell.getRow() > cells.GetLength(1) / 2 && cell.getCol() > cells.GetLength(0) / 2)
            retVal = 4;
        if (cell.getRow() > cells.GetLength(1) / 2 && cell.getCol() < cells.GetLength(0) / 2)
            retVal = 3;
        if (cell.getRow() < cells.GetLength(1) / 2 && cell.getCol() < cells.GetLength(0) / 2)
            retVal = 2;
        return retVal;
    }
    public override string ToString()
    {
        return "Cell: " + row.ToString() + " , " + col.ToString();
    }
    public int getRow() { return row; }
    public int getCol() { return col; }
    public int getWallToDestroy() { return wallToDestroy;  }
    public void setWallToDestroy(int w2d) { wallToDestroy = w2d; }
    public static void ResetVisitedFlags( Square cell )
    {
        cell.visited = false;
    }
    public static void ResetVisitedFlags (Square[,] cells )
    {
        for (int x = 0; x < cells.GetLength(0); x++)
            for (int y = 0; y < cells.GetLength(1); y++)
                cells[x, y].visited = false;
    }
}
