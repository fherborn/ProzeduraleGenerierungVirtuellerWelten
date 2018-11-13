using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TinyPlanet : MonoBehaviour
{
    #region Fields
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    private List<Vector3> vertices;

    public float radius = 1f;
    public Vector3 rotationAngles =
        new Vector3(90f, 0f, 0f);

    public int pointsLongitude = 60;
    public int pointsLatitude = 30;
    #endregion // Fields

    #region Heightmap

    #endregion // Heightmap

    public void CreateMesh()
    {
        PrepareMeshComponents();

        vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        #region Parameter berechnen
        float widthStep =
            360f / ((float)pointsLongitude - 1f);
        float heightStep =
            180f / ((float)pointsLatitude - 1f);

        float parameterWidth =
            1f / ((float)pointsLongitude - 1f);
        float parameterHeight =
            1f / ((float)pointsLatitude - 1f);
        #endregion // Parameter berechnen

        #region Vertices berechnen
        for (int lon = 0; lon < pointsLongitude; lon++)
        {
            for (int lat = 0; lat < pointsLatitude; lat++)
            {
                Vector3 pointOnSphere =
                    PointOnSphere(
                        lon * widthStep,
                        lat * heightStep,
                        radius,
                        transform.position);

                pointOnSphere = RotatePointAroundPivot(
                    pointOnSphere,
                    transform.position,
                    rotationAngles);

                vertices.Add(pointOnSphere);

                Vector2 uv = new Vector2(
                    lon * parameterWidth,
                    lat * parameterHeight);
                uvs.Add(uv);
            }
        }
        #endregion // Vertices berechnen

        #region triangles erzeugen
        for (int n = 1; n < pointsLongitude; n++)
        {
            for (int m = 1; m < pointsLatitude; m++)
            {
                int P0, P1, P2, P3;
                P0 = (n - 1) * pointsLatitude + m - 1;
                P1 = (n - 1) * pointsLatitude + m;
                P2 = n * pointsLatitude + m - 1;
                P3 = n * pointsLatitude + m;

                triangles.Add(P0);
                triangles.Add(P1);
                triangles.Add(P2);

                triangles.Add(P2);
                triangles.Add(P1);
                triangles.Add(P3);
            }
        }
        #endregion // triangles erzeugen

        #region Mesh zusammensetzen
        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        #endregion // Mesh zusammensetzen

        ApplyHeightmap();

        meshFilter.sharedMesh = mesh;
    }

    public void ApplyHeightmap()
    {

    }


    #region Menüeinbindung
#if UNITY_EDITOR
    [MenuItem("GameObject/PGVW/TinyPlanet")]
    public static TinyPlanet CreateTinyPlanet()
    {
        GameObject gO = new GameObject("TinyPlanet");
        TinyPlanet planet = gO.AddComponent<TinyPlanet>();
        return planet;
    }
#endif
    #endregion

    #region HelperMethoden
    private void PrepareMeshComponents()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "TinyPlanet";
        }
    }

    private Vector3 PointOnSphere(
        float lon, float lat, float radius,
        Vector3 sphereCenter)
    {
        Vector3 result;
        float lonRad = lon * Mathf.Deg2Rad;
        float latRad = lat * Mathf.Deg2Rad;

        result.x =
            radius * Mathf.Cos(lonRad) * Mathf.Sin(latRad);
        result.y =
            radius * Mathf.Sin(lonRad) * Mathf.Sin(latRad);
        result.z = radius * Mathf.Cos(latRad);

        return result + sphereCenter;
    }

    public Vector3 RotatePointAroundPivot(
        Vector3 point, Vector3 pivot,
        Vector3 angles)
    {
        Vector3 direction = point - pivot;
        direction = Quaternion.Euler(angles) * direction;

        point = pivot + direction;
        return point;
    }
    #endregion // HelperMethoden
}
