using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGenerator : MonoBehaviour  {

	#region Mesh und Komponenten Deklaration
	public Mesh mesh;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	#endregion

	public HouseData houseData = new HouseData();
	public StairsData stairData = new StairsData();
	public StairFrame stairFrame;


	private int roomHeight;
	private int treadCount;	
	private int length;

	public void CreateStairs() {

		//Berechnen der relevanten Werte
		roomHeight = houseData.houseConfig.roomHeight;
		treadCount = (int) (houseData.houseConfig.roomHeight / houseData.houseConfig.treadHeight);
		length = treadCount * houseData.houseConfig.treadLength;
		
		//Anfangs und Endpunkt der Treppe
		Vector3 P0 = new Vector3 (0f, 0f, 0f);
		Vector3 P1 = new Vector3 (0f,roomHeight,length);  

		//Ermitteln der Point On Line
		for (int i = 0; i < treadCount; i++) {
				stairData.allTreads.Add (new Tread (houseData, PointOnLine (P0, P1, (1f/treadCount) * i),i));
		}

		stairFrame = new StairFrame (houseData, P0, P1);
	
		CreateMesh ();
	}

	public void CreateMesh() {


		#region Komponenten sicherstellen
		meshFilter = GetComponent<MeshFilter>();
		if (meshFilter == null)
			meshFilter = this.gameObject.AddComponent<MeshFilter>();

		meshRenderer = GetComponent<MeshRenderer>();
		if (meshRenderer == null)
			meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

		this.mesh = meshFilter.sharedMesh;

		if (this.mesh == null)
			this.mesh = new Mesh();
		#endregion


		mesh.Clear ();

		//Vertices hinzufügen
		#region vertices
		List<Vector3> vertices = new List<Vector3>();
		for (int i = 0; i < treadCount; i++) {
			for (int y = 0; y < stairData.allTreads [i].vertices.Length; y++) {
				vertices.Add(stairData.allTreads[i].vertices[y]);
			}
		}

//		for (int i = 0; i < stairFrame.vertecis.Length; i++) {
//			vertices.Add(stairFrame.vertecis[i]);
//		}

		mesh.vertices = vertices.ToArray();
		#endregion

		//triangles hinzufügen
		#region triangles
		List<int> trianglesTread = new List<int>();
		for (int i = 0; i < treadCount; i++) {
			for (int y = 0; y < stairData.allTreads [i].allTriangles.Length; y++) {
				trianglesTread.Add(stairData.allTreads[i].allTriangles[y]);
			}
		}

//		List<int> trianglesFrame = new List<int>();
//		for (int i = 0; i < stairFrame.allTriangles.Length; i++) {
//			trianglesFrame.Add(stairFrame.allTriangles[i]);
//		}

		this.mesh.triangles = trianglesTread.ToArray();

//		mesh.subMeshCount = 1;
//		mesh.SetTriangles(trianglesTread.ToArray(),0);
		//mesh.SetTriangles(trianglesFrame.ToArray(),1);
		#endregion	

		//uvs hinzufügen
		#region uvs
		List<Vector2> uvs = new List<Vector2>();
		for (int i = 0; i < treadCount; i++) {
			for (int y = 0; y < stairData.allTreads [i].uvs.Length; y++) {
				uvs.Add(stairData.allTreads[i].uvs[y]);
			}
		}
		this.mesh.uv = uvs.ToArray();
		#endregion


		// Berechnet die Vertex-Normalen für das Mesh neu
		mesh.RecalculateNormals();

		// das Mesh zuweisen
		meshFilter.mesh = mesh;

		Texture brick = Resources.Load("brick") as Texture;
		Debug.Log (brick);

		// Material-Array anlegen
		Material[] materials = new Material[1];

		// Materialien erstellen
		materials[0] = new Material(Shader.Find("Diffuse"));

		// Den Materialien die Textur zuweisen
		materials[0].mainTexture = brick;

		// Dem MeshRenderer das MaterialArray übergeben
		meshRenderer.materials = materials;

	}


	private Vector3 PointOnLine(Vector3 p0, Vector3 p1, float t)
	{
		return (1 - t) * p0 + t * p1;
	}

	public void OnDrawGizmos() {
//		for (int i = 0; i < treadCount; i++) {
//			for (int y = 0; y < stairData.allTreads [i].vertices.Length; y++) {
//				Gizmos.DrawSphere(stairData.allTreads[i].vertices[y], 0.1f);
//			}
//		}
		for (int i = 0; i < stairFrame.vertecis.Length; i++) {
			Gizmos.DrawSphere (stairFrame.vertecis[i],0.1f);
			//print (stairFrame.vertecis[i]);
		}
	
	}

}
