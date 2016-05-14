using UnityEngine;
using System.Collections; //Inherits Random class
/* @author: Michael Gonzalez
 * This abstract class provides a basic model for each maze generation 
 * algorithm. Please override the run method to implement new algorithms.
 * Methods to destroy walls and generate a random wall are provided, along
 * with constant integers to refer to each wall within a cell. There is no
 * abstract or protected constructor. This is to encourage creativity and allow
 * flexibility in the masterpiece you're bound to make :)
 */
public abstract class MazeGenerator
{
    protected const int NORTH = 0;
    protected const int SOUTH = 1;
    protected const int EAST = 2;
    protected const int WEST = 3;

    protected int Rows, Cols;
    protected Square[,] walls;
    protected Square start;
    protected Square exit;

    /* This function is to be overrided by the different algorithms. 
     * @param cells - The matrix of walls to transform into a maze.
     * @param end - Reference to the "end" cell. Try to flag the bool exit 
     * in the Square that represents the exit in your algorithm.
     */
    public abstract void run(Square[,] cells, Square end);
    protected void createSquares(bool wallsUp)
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                walls[r, c] = new Square(r, c, wallsUp);
            }
        }
    }

    /* This function to prevent individual walls from generating at runtime.
     * @param cell - Cell in which walls are stored.
     * @param wallToDestroy - Which wall to destroy
     */
    protected void destroyWall(Square cell, int wallToDestroy)
    {
        switch (wallToDestroy)
        {
            case NORTH:
                //Debug.Log("Removing NORTH Wall");
                cell.hasNorth = false;
                break;
            case SOUTH:
                //Debug.Log("Removing SOUTH Wall");
                cell.hasSouth = false;
                break;
            case EAST:
                //Debug.Log("Removing EAST Wall");
                cell.hasEast = false;
                break;
            case WEST:
                //Debug.Log("Removing WEST Wall");
                cell.hasWest = false;
                break;
        }
    }

    // Function returns an int that corrolates to a wall
    protected int randomEdge()
    {
        return Random.Range(0, 3);
    }

}
