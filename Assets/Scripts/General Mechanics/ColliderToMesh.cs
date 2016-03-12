/**
 * Author: ViicEsquivel
 * Found at: http://answers.unity3d.com/questions/835675/how-to-fill-polygon-collider-with-a-solid-color.html
 */
using UnityEngine;
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
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
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = points[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
            uv[j] = new Vector2(j,-2);
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