using UnityEngine;
using System.Collections;
using UnityEditor;

public class Door : MonoBehaviour {

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Mesh mesh;

    public float width = 1f;
    public float depth = 0.3f;
    public float height = 2f;

    public Vector3 P0, P1, P2, P3, P4, P5, P6, P7;

    public void CreateMesh()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
            meshFilter = this.gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

        this.mesh = meshFilter.sharedMesh;

        if (mesh == null)
        {
            this.mesh = new Mesh();
            mesh.name = "door";
        }

        P0 = new Vector3(0, 0, 0);
        P1 = Vector3.forward * depth;
        P2 = Vector3.right * width;
        P3 = Vector3.forward * depth + Vector3.right * width;

        P4 = Vector3.up * height;
        P5 = P4 + Vector3.forward * depth;
        P6 = P4 + Vector3.right * width;
        P7 = P6 + Vector3.forward * depth;

        mesh.Clear();

        Vector3[] vertices = new Vector3[24];
        Vector2[] uv = new Vector2[vertices.Length];

        vertices[0] = P0;
        vertices[1] = P1;
        vertices[2] = P2;
        vertices[3] = P3;

        //Front 
        vertices[4] = P0;
        vertices[5] = P2;
        vertices[6] = P4;
        vertices[7] = P6;

        //Back
        vertices[8] = P1;
        vertices[9] = P3;
        vertices[10] = P5;
        vertices[11] = P7;

        //SideA 
        vertices[12] = P3;
        vertices[13] = P2;
        vertices[14] = P6;
        vertices[15] = P7;

        //SideB
        vertices[16] = P0;
        vertices[17] = P1;
        vertices[18] = P4;
        vertices[19] = P5;

        //up
        vertices[20] = P4;
        vertices[21] = P5;
        vertices[22] = P6;
        vertices[23] = P7;

        uv[0] = new Vector2(0f, 0f);
        uv[1] = new Vector2(0f, 1f);
        uv[2] = new Vector2(1f, 0f);
        uv[3] = new Vector2(1f, 1f);


        //Triangles
        int[] triangles = new int[36];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 3;
        triangles[4] = 2;
        triangles[5] = 1;

        uv[4] = new Vector2(0f, 0f);
        uv[5] = new Vector2(0f, 1f);
        uv[6] = new Vector2(1f, 0f);
        uv[7] = new Vector2(1f, 1f);

        //Front
        triangles[6] = 6;
        triangles[7] = 5;
        triangles[8] = 4;

        triangles[9] = 6;
        triangles[10] = 7;
        triangles[11] = 5;

        uv[8] = new Vector2(0f, 0f);
        uv[9] = new Vector2(0f, 1f);
        uv[10] = new Vector2(1f, 0f);
        uv[11] = new Vector2(1f, 1f);

        //Back
        triangles[12] = 8;
        triangles[13] = 9;
        triangles[14] = 10;

        triangles[15] = 11;
        triangles[16] = 10;
        triangles[17] = 9;

        uv[12] = new Vector2(0f, 0f);
        uv[13] = new Vector2(0f, 1f);
        uv[14] = new Vector2(1f, 0f);
        uv[15] = new Vector2(1f, 1f);
        
        //SideA
        triangles[18] = 12;
        triangles[19] = 13;
        triangles[20] = 14;

        triangles[21] = 15;
        triangles[22] = 12;
        triangles[23] = 14;

        uv[16] = new Vector2(0f, 0f);
        uv[17] = new Vector2(0f, 1f);
        uv[18] = new Vector2(1f, 0f);
        uv[19] = new Vector2(1f, 1f);

        //SideB
        triangles[24] = 16;
        triangles[25] = 17;
        triangles[26] = 18;

        triangles[27] = 19;
        triangles[28] = 18;
        triangles[29] = 17;

        uv[20] = new Vector2(0f, 0f);
        uv[21] = new Vector2(0f, 1f);
        uv[22] = new Vector2(1f, 0f);
        uv[23] = new Vector2(1f, 1f);

        //up
        triangles[30] = 20;
        triangles[31] = 21;
        triangles[32] = 22;

        triangles[33] = 23;
        triangles[34] = 22;
        triangles[35] = 21;

        //build Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        //create/build material

        Material wood = Resources.Load("wood") as Material;
        
        Material[] material = new Material[1];

        

        material[0] = wood;

        meshRenderer.materials = material;

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(P0, 0.1f);
        //Gizmos.DrawSphere(P1, 0.1f);
        //Gizmos.DrawSphere(P2, 0.1f);
        //Gizmos.DrawSphere(P3, 0.1f);

        //Gizmos.DrawSphere(P4, 0.1f);
        //Gizmos.DrawSphere(P5, 0.1f);
        //Gizmos.DrawSphere(P6, 0.1f);
        //Gizmos.DrawSphere(P7, 0.1f);


    }

    #if UNITY_EDITOR
    [MenuItem("GameObject/3D Object/Primitives/door", false, 50)]
    public static Door Createdoor()
    {
        // Anlegen eines neuen GameObjekts
        GameObject gO = new GameObject("door");
        // Hinzufügen der Haus-Komponente zum GameObject
        Door door = gO.AddComponent<Door>();

       
        

        // Generiere das Haus
        door.CreateMesh();

        return door;
    }
#endif
	
}
