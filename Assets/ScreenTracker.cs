using UnityEngine;
using System.Collections;

public class ScreenTracker : MonoBehaviour {

    public bool isDebugging;
    public int numberOfFrames = 3;
    private int height, width;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        height = Screen.height;
        width = Screen.width;
        if(isDebugging)
        {
            Debug.Log("Width: " + width + " Height: " + height
                      + " MousePos: " + Input.mousePosition);
        }

    }

    void OnDrawGizmos()
    {
        //Vector3 screenBase = Vector3.zero;
        int regionOffset = numberOfFrames * 2;
        int heightOffset = height / regionOffset;
        int widthOffset  = width  / regionOffset;
        Debug.Log("Height Offset: " + heightOffset + " Width Offset: " + widthOffset);
        for (int idx = 1; idx <= numberOfFrames; idx ++) {
            // Bottom
            Vector3 bottomLeft = Translate(idx * widthOffset, idx * heightOffset);
            Vector3 bottomRight = Translate(width - idx * widthOffset, idx * heightOffset);
            // Top
            Vector3 topLeft = Translate(idx * widthOffset, height - idx * heightOffset);
            Vector3 topRight = Translate(width - idx * widthOffset, height - idx * heightOffset);
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
        }
    }

    Vector3 Translate( int x, int y )
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
    }
}
