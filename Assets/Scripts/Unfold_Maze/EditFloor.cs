using UnityEngine;
using System.Collections;

public class EditFloor : MonoBehaviour {
    private Transform floorTransform;
    void Start()
    {
        GameObject mazeRoot = GameObject.Find("Maze");
        if (mazeRoot != null)
        {
            floorTransform = GetComponent<Transform>();
            floorTransform.parent = mazeRoot.GetComponent<Transform>();
        }
    }
    public void ModifyFloorSize(float wallSize, int rows, int cols)
    {
        transform.localScale += new Vector3((wallSize * rows / 10), 0, (wallSize * cols / 10));
    }
    public void UpdateTexture( int texture)
    {
    }
}
