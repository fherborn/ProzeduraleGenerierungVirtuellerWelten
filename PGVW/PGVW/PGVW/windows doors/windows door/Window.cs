using UnityEngine;
using System.Collections;
using UnityEditor;

public class Window : MonoBehaviour
{

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Mesh mesh;


    public float size = 2f;
    

    public Vector3 P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20,P21,P22,P23,P24,P25,P26,P27,P28,P29,P30, P31;
      
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
            mesh.name = "window";
        }

        P0 = new Vector3(0, 0, 0);
        P1 = Vector3.right * size;
        P2 = Vector3.right * size / 5;
        P3 = Vector3.right * (size - (size / 5));

        P4 = Vector3.up * size;
        P5 = Vector3.up * size / 5;
        P6 = Vector3.up * (size - (size / 5));


        P7 = P1 + P4;
        P8 = P1 + P5;
        P9 = P1 + P6;

        P10 = P4 + P2;
        P11 = P4 + P3;

        P12 = P5 +  Vector3.right * (size / 5);
        P13 = P5 + Vector3.right * (size - (size / 5));
        P14 = P6 + Vector3.right * (size / 5);
        P15 = P6 + Vector3.right * (size - (size / 5));

        P16 = Vector3.forward * size + new Vector3(0, 0, 0);
        P17 = Vector3.forward * size + Vector3.right * size;
        P18 = Vector3.forward * size + Vector3.right * size / 5;
        P19 = Vector3.forward * size + Vector3.right * (size - (size / 5));

        P20 = Vector3.forward * size + Vector3.up * size;
        P21 = Vector3.forward * size + Vector3.up * size / 5;
        P22 = Vector3.forward * size + Vector3.up * (size - (size / 5));


        P23 = Vector3.forward * size + P1 + P4;
        P24 = Vector3.forward * size + P1 + P5;
        P25 = Vector3.forward * size + P1 + P6;

        P26 = Vector3.forward * size + P4 + P2;
        P27 = Vector3.forward * size + P4 + P3;

        P28 = Vector3.forward * size + P5 + Vector3.right * (size / 5);
        P29 = Vector3.forward * size + P5 + Vector3.right * (size - (size / 5));
        P30 = Vector3.forward * size + P6 + Vector3.right * (size / 5);
        P31 = Vector3.forward * size + P6 + Vector3.right * (size - (size / 5));


        mesh.Clear();

        Vector3[] vertices = new Vector3[48];
        Vector2[] uv = new Vector2[vertices.Length];

        //Unten
        vertices[0] = P0;
        vertices[1] = P5;
        vertices[2] = P1;
        vertices[3] = P8;

        //links
        vertices[4] = P0;
        vertices[5] = P2;
        vertices[6] = P4;
        vertices[7] = P10;

        //rechts
        vertices[8] = P1;
        vertices[9] = P3;
        vertices[10] = P7;
        vertices[11] = P11;

        //oben
        vertices[12] = P4;
        vertices[13] = P6;
        vertices[14] = P7;
        vertices[15] = P9;

        //zwischen unten
        vertices[16] = P12;
        vertices[17] = P13;
        vertices[18] = P28;
        vertices[19] = P29;

        //zwischen links 
        vertices[20] = P12;
        vertices[21] = P14;
        vertices[22] = P28;
        vertices[23] = P30;

        //zwischen rechts 
        vertices[24] = P13;
        vertices[25] = P15;
        vertices[26] = P29;
        vertices[27] = P31;

        //zwischen oben
        vertices[28] = P14;
        vertices[29] = P15;
        vertices[30] = P30;
        vertices[31] = P31;

        //untenAussen
        vertices[32] = P16;
        vertices[33] = P21;
        vertices[34] = P17;
        vertices[35] = P24;

        //linksAussen
        vertices[36] = P16;
        vertices[37] = P18;
        vertices[38] = P20;
        vertices[39] = P26;

        //rechtsAussen 
        vertices[40] = P17;
        vertices[41] = P19;
        vertices[42] = P23;
        vertices[43] = P25;

        //ObenAussen
        vertices[44] = P22;
        vertices[45] = P20;
        vertices[46] = P25;
        vertices[47] = P23;



        //Triangles und UVs

        uv[0] = new Vector2(0f, 0f);
        uv[1] = new Vector2(0f, 1f);
        uv[2] = new Vector2(1f, 0f);
        uv[3] = new Vector2(1f, 1f);

        int[] triangles = new int[51];


        


        uv[4] = new Vector2(0f, 0f);
        uv[5] = new Vector2(0f, 1f);
        uv[6] = new Vector2(1f, 0f);
        uv[7] = new Vector2(1f, 1f);

        uv[8] = new Vector2(0f, 0f);
        uv[9] = new Vector2(0f, 1f);
        uv[10] = new Vector2(1f, 0f);
        uv[11] = new Vector2(1f, 1f);

        uv[12] = new Vector2(0f, 0f);
        uv[13] = new Vector2(0f, 1f);
        uv[14] = new Vector2(1f, 0f);
        uv[15] = new Vector2(1f, 1f);

        //links

        triangles[0] = 5;
        triangles[1] = 6;
        triangles[2] = 7;

        triangles[3] = 4;
        triangles[4] = 6;
        triangles[5] = 5;

        //unten
        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 2;

        triangles[9] = 1;
        triangles[10] = 3;
        triangles[11] = 0;

        //rechts
        triangles[12] = 8;
        triangles[13] = 9;
        triangles[14] = 10;

        triangles[15] = 9;
        triangles[16] = 11;
        triangles[17] = 10;

        //oben
        triangles[18] = 12;
        triangles[19] = 14;
        triangles[20] = 13;

        triangles[21] = 15;
        triangles[22] = 13;
        triangles[23] = 14;

        //zwischenUnten
        triangles[24] = 16;
        triangles[25] = 18;
        triangles[26] = 19;

        triangles[27] = 17;
        triangles[28] = 16;
        triangles[29] = 19;

        //zwischenlinks
        triangles[30] = 20;
        triangles[31] = 21;
        triangles[32] = 22;

        triangles[33] = 21;
        triangles[34] = 23;
        triangles[35] = 22;

        //zwischenrechts
        triangles[36] = 26;
        triangles[37] = 25;
        triangles[38] = 24;

        triangles[39] = 27;
        triangles[40] = 25;
        triangles[41] = 26;
        
        //zwischenoben 
        //triangles[42] = 28;
        //triangles[43] = 31;
        //triangles[45] = 30;

        //triangles[46] = 29;
        //triangles[47] = 28;
        //triangles[48] = 30;


        //build Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;



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
        //Gizmos.DrawSphere(P8, 0.1f);
        //Gizmos.DrawSphere(P9, 0.1f);
        //Gizmos.DrawSphere(P10, 0.1f);
        //Gizmos.DrawSphere(P11, 0.1f);
        //Gizmos.DrawSphere(P12, 0.1f);
        //Gizmos.DrawSphere(P13, 0.1f);
        //Gizmos.DrawSphere(P14, 0.1f);
        //Gizmos.DrawSphere(P15, 0.1f);

        //Gizmos.DrawSphere(P16, 0.1f);
        //Gizmos.DrawSphere(P17, 0.1f);
        //Gizmos.DrawSphere(P18, 0.1f);
        //Gizmos.DrawSphere(P19, 0.1f);

        //Gizmos.DrawSphere(P20, 0.1f);
        //Gizmos.DrawSphere(P21, 0.1f);
        //Gizmos.DrawSphere(P22, 0.1f);
        //Gizmos.DrawSphere(P23, 0.1f);
        //Gizmos.DrawSphere(P24, 0.1f);
        //Gizmos.DrawSphere(P25, 0.1f);
        //Gizmos.DrawSphere(P26, 0.1f);
        //Gizmos.DrawSphere(P27, 0.1f);
        //Gizmos.DrawSphere(P28, 0.1f);
        //Gizmos.DrawSphere(P29, 0.1f);
        //Gizmos.DrawSphere(P30, 0.1f);
        //Gizmos.DrawSphere(P31, 0.1f);


    }
#if UNITY_EDITOR
    [MenuItem("GameObject/3D Object/Primitives/window", false, 50)]
    public static Window CreateWindow()
    {
        // Anlegen eines neuen GameObjekts
        GameObject gO = new GameObject("window");
        // Hinzufügen der Haus-Komponente zum GameObject
        Window window = gO.AddComponent<Window>();

       
        

        // Generiere das Haus
        window.CreateMesh();

        return window;
    }
#endif

}

