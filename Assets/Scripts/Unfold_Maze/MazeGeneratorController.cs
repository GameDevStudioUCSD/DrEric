using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* @author: Michael Gonzalez
 * This class is used directly in the Unity Editor to generate the maze. To use 
 * it, just attach the script component to a blank Unity Game Object.
 * @param Rows - The number of rows to generate
 * @param Cols - The number of columns to generate
 * @param wallSize - The size of each individual wall. This ensures spacing is
 *                   accurate when the maze is generated.
 * @param algorithm - Defines the algorithm to be used by the controller.
 * @param Player - Player controller to be placed at the start of the maze.
 *                 (CURRENTLY ONLY WORKS WITH ONE GAMEOBJECT)
 * @param exit - GameObject to signify the end of the maze
 */
public class MazeGeneratorController : MonoBehaviour {
    const int NORTH = 0;
    const int SOUTH = 1;
    const int EAST = 2;
    const int WEST = 3;
    //Define new algorithm types here
    public const int DepthFirst = 0;
    public const int Recursive = 1;


    public int Rows = 20;
    public int Cols = 20;
    public float wallSize = 10;
    public enum AlgorithmChoice
    {
        DepthFirst,
        Recursive
    };
    
    public AlgorithmChoice algorithm;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Floor, Player, ExitMarker, DebugSphere;
    public GameObject[] spawnList;
    public bool debug_ON = false;
    [Range (1, 100)]
    public int spawningRate = 1;

    private Square[,] walls;
    private Square exit;
    private Square start;
    private Square curr;
    private MazeGenerator generator;
    private SortedDictionary<string, GameObject> westWalls;
    private GameObject innerWall;
    private Vector3 center;
    public MazeGeneratorController(AlgorithmChoice algorithm)
    {
        algorithm = this.algorithm;
    }
	// Use this for initialization
	public void Start () {
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        
        center  = new Vector3(wallSize * Rows / 2, wallSize * Cols / 2,0 );
        // Add new algorithm cases here
        switch(algorithm)
        {
            case AlgorithmChoice.Recursive:
                generator = new RecursiveMaze(Rows, Cols);
                break;
            default:
                Debug.LogError("Algorithm not defined");
                return;
        }
        //generator = new WorkingDepthFirstMazeGenerator(Rows,Cols);
        generator.run(walls, exit);
        CreateWalls();
	
	}
    // Removes duplicate walls from adjacent cells
    public void DetermineWallsToSpawn()
    {
        for( int c = 0; c < Cols; c++ )
        {
            for( int r = 0; r < Rows; r++)
            {
                curr = walls[r, c];
                if(curr.hasEast && c < Cols-1)
                {
                    walls[r, c + 1].hasWest = true;
                    curr.hasEast = false;
                }
                if(curr.hasSouth && r < Rows-1)
                {
                    walls[r + 1, c].hasNorth = true;
                    curr.hasSouth = false;
                }
            }
        }
    }
    public void DetermineLogicalWallCount()
    {
        for (int c = 1; c < Cols; c++)
        {
            for (int r = 1; r < Rows; r++)
            {
                curr = walls[r, c];
                if (curr.hasWest)
                {
                    walls[r, c - 1].hasEast = true;
                }
                if (curr.hasNorth)
                {
                    walls[r - 1, c].hasSouth = true;
                }
            }
        }
    }
    
    public void FixWallIssues()
    {
        // The cells directly to the south and south east to curr
        Square sCell, swCell;
        EditWalls wallNView;
        bool inBounds;
        for (int c = 0; c < Cols; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                curr = walls[r, c];
                if (r < Rows - 1)
                    sCell = walls[r + 1, c];
                else
                    sCell = curr;
                if (c > 0 && r < Rows - 1)
                    swCell = walls[r + 1, c - 1];
                else
                    swCell = curr;
                //First, fix any flickering issues
                if(curr.hasWest && curr.hasNorth)
                {
                    if(debug_ON)
                    {
                        Debug.Log("Tried to fix flickering");
                    }
                    if (westWalls.TryGetValue(curr.ToString(), out innerWall))
                    {
                        wallNView = innerWall.GetComponent<EditWalls>();
                        wallNView.ShrinkWall();
                    }
                }
                inBounds = (r < Rows - 1 && c > 0);
                if (inBounds && curr.hasWest && !sCell.hasNorth && !sCell.hasWest && swCell.hasNorth)
                {
                    if(debug_ON)
                    {
                        Debug.Log("Found a missing corner!");
                    }
                    if(westWalls.TryGetValue(curr.ToString(), out innerWall))
                    {
                        wallNView = innerWall.GetComponent<EditWalls>();
                        wallNView.ExpandWall();
                    }
                }
                
            }
        }
    }

    // Creates the walls flagged for creation
    public void CreateFloor()
    {
        GameObject floor;
        NetworkView nView;
        Vector3 position = new Vector3((Rows*wallSize/2), 0 , (Cols*wallSize/2));
        position -= center;
        floor = (GameObject)GameObject.Instantiate(Floor, position, Quaternion.identity);
        RemoveCloneFromName(floor);
        floor.GetComponent<Transform>().parent = this.transform;
        floor.GetComponent<EditFloor>().ModifyFloorSize(wallSize, Rows, Cols );
    }
    public void ApplyWallTexture( GameObject currentWall)
    {

    }
    public void CreateWalls()
    {
        DetermineWallsToSpawn();
        Stack instatiationList = new Stack();
        GameObject objToInstantiate = null;
        Vector3 pos;
        Quaternion rot = Quaternion.identity;
        westWalls = new SortedDictionary<string, GameObject>();
        CreateFloor();
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                // Displays indexing spheres
                if(debug_ON)
                {
                    GameObject indexSphere = (GameObject)GameObject.Instantiate(DebugSphere, new Vector3(r * wallSize, 15, c * wallSize), rot);
                    float red = ((float)r / Rows);
                    float blue = ((float)c / Cols);
                    Color indexColor = new Color(red, 0, blue);
                    indexSphere.GetComponent<Renderer>().material.SetColor("_Color", indexColor);

                }
                curr = walls[r, c];
                pos = new Vector3(curr.getRow() * wallSize, wallSize * curr.getCol(), 1);
                pos -= center; 
                if(curr.hasNorth)
                {
                    objToInstantiate = (GameObject) GameObject.Instantiate(NorthWall, pos, rot);
                    instatiationList.Push(objToInstantiate);
                }
                if (curr.hasSouth)
                {
                    objToInstantiate = (GameObject)GameObject.Instantiate(SouthWall, pos, rot);
                    instatiationList.Push(objToInstantiate);
                }
                if (curr.hasEast)
                {
                    objToInstantiate = (GameObject)GameObject.Instantiate(EastWall, pos, rot);
                    instatiationList.Push(objToInstantiate);
                }
                if (curr.hasWest)
                {
                    objToInstantiate = (GameObject)GameObject.Instantiate(WestWall, pos, rot);
                    instatiationList.Push(objToInstantiate);
                    westWalls.Add(curr.ToString(), objToInstantiate);
                }
                if (curr.start)
                {
                    start = curr;
                }
                if(curr.exit)
                {
                    objToInstantiate = (GameObject)GameObject.Instantiate(ExitMarker, pos, rot);
                    instatiationList.Push(objToInstantiate);
                }
                
                while (instatiationList.Count > 0)
                {
                    objToInstantiate = (GameObject)instatiationList.Pop();
                    RemoveCloneFromName(objToInstantiate);
                    objToInstantiate.GetComponent<Transform>().parent = this.transform;
                }
            }
        }
        FixWallIssues();
        DetermineLogicalWallCount();
        
    }
    public static void RemoveCloneFromName(GameObject obj)
    {
        obj.name = obj.name.Replace("(Clone)", "");
    }
    public void SetSpawnLocations()
    {
        
    }
    public Square getStartSquare()
    {
        return start;
    }

	public Square[,] getWalls()
	{
		return walls;
	}

}
