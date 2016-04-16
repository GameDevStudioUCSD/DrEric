/**
 * Author: ViicEsquivel
 * Edited by Kamashamasay
 *
 * Found at: http://answers.unity3d.com/questions/835675/how-to-fill-polygon-collider-with-a-solid-color.html
 * UV application done by Kamashamasay
 */
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class ColliderToMesh : MonoBehaviour
{
    void Start()
    {
        int pointCount = 0;
        PolygonCollider2D pc2 = gameObject.GetComponent<PolygonCollider2D>();
        pointCount = pc2.GetTotalPointCount();

        
        Mesh mesh = new Mesh();
        Vector2[] points = pc2.points;
        Vector3[] vertices = new Vector3[pointCount];
        Vector2[] uv = new Vector2[pointCount];
        float top = 0;
        float right = 0;
        float bottom= 0;
        float left = 0;
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = points[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
            //uv[j] = new Vector2(j,1);
            if(actual.y < bottom)
            {
                bottom = actual.y;
            }
            if(actual.y > top )
            {
                top = actual.y;
            }
            if(actual.x < left)
            {
                left = actual.x;
            }
            if(actual.x > right)
            {
                right = actual.x;
            }
            
        }
        float lengthX = right - left;
        float heightY = top - bottom;
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = points[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
            uv[j] = new Vector2((actual.x - left)/lengthX, (actual.y - bottom)/heightY );
        }
        Triangulator tr = new Triangulator(points);
        int[] triangles = tr.Triangulate();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mesh_Renderer = this.GetComponent<MeshRenderer>();
        mf.mesh = mesh;
    }
}