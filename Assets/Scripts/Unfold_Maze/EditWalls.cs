using UnityEngine;
using System.Collections;

public class EditWalls : MonoBehaviour {
    public bool debugOn;
    //public GameObject container;
    private Transform wallTransform;
    void Start()
    {
        GameObject mazeRoot = GameObject.Find("Maze");
        if(mazeRoot != null)
        {
            wallTransform = GetComponent<Transform>();
            wallTransform.parent = mazeRoot.GetComponent<Transform>();
        }
    }
    private void FindInnerWall()
    {
        wallTransform = GetComponent<Transform>();
        wallTransform = wallTransform.Find("InnerWall");
    }
    private void SetupWall()
    {
        FindInnerWall();
        wallTransform.localPosition += (Vector3.right * (.5f));
        if(debugOn)
        {
            Debug.Log(wallTransform);
        }
    }
    
    public void ShrinkWall()
    {
        SetupWall();
        wallTransform.localScale -= Vector3.forward;
    }
    
    public void ExpandWall()
    {
        SetupWall();
        wallTransform.localScale += Vector3.forward;
    }
    
    public void UpdateTexture(int texture)
    {
    }
}
