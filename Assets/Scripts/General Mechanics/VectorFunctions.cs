using UnityEngine;
using System.Collections;

public class VectorFunctions  {
    public static Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float rads = Mathf.Deg2Rad * degrees;
        Vector2 row1 = new Vector2(Mathf.Cos(rads), -1.0f * Mathf.Sin(rads)); 
        Vector2 row2 = new Vector2(Mathf.Sin(rads), -1.0f * Mathf.Cos(rads));
        float x = Vector2.Dot(row1, vector);
        float y = Vector2.Dot(row2, vector);
        return new Vector2(x,y);
    }
}
