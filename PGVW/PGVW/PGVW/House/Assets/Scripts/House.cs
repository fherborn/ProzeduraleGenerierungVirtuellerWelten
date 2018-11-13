using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class House : MonoBehaviour {

	public HouseData houseData;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public Mesh mesh;
	public int floorCount;
	List<Vector3> vertices;
	List<Vector2>  uvs;
	int currentVerticesIndex;
	GameObject myHouse;
	public int subMeshCount;
	List<Material> materials;

	public void CreateMesh(HouseData houseData){
		this.houseData = houseData;
		myHouse = new GameObject ("House");
		floorCount = 0;
//		meshFilter = gameObject.GetComponent<MeshFilter> ();
//		if (meshFilter == null) {
		meshFilter = myHouse.AddComponent<MeshFilter> ();
//		}
//		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
//		if (meshRenderer == null) {
		meshRenderer = myHouse.AddComponent<MeshRenderer> ();
//		}
		this.mesh = meshFilter.sharedMesh;
		if (this.mesh == null) {
			this.mesh = new Mesh ();
			this.mesh.name = "House";
		}
		materials = new List<Material> ();
		vertices= new List<Vector3> ();
		uvs = new List<Vector2> ();
		currentVerticesIndex = 0;
		this.mesh.Clear ();
		subMeshCount = 0;
	}

	public void FinishHouse(){
		//this.mesh.vertices = vertices.ToArray();
		//this.mesh.RecalculateNormals ();
		this.mesh.uv = uvs.ToArray ();
		meshFilter.sharedMesh = this.mesh;
		meshRenderer.materials = materials.ToArray ();
		
	}

	public void CreateFloor(){
		//Vector3[,,] mapMeshPoints = new Vector3[houseData.houseConfig.houseWidth,houseData.houseConfig.houseLength,4];

		List<int> triangles = new List<int> ();
		for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
			for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
				if (houseData.floorPlan.area [x, z] == UnitType.INNER_GROUND) {

					Vector3[] points = CreateFloorUnit (new Vector3 (x* UnitToPixel(1), floorCount * UnitToPixel(houseData.houseConfig.roomHeight), z* UnitToPixel(1)));
//					for(int i = 0; i< 4; i++){
//						mapMeshPoints [x, z, i] = points [i];
//					}					
					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex+2);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+2);
					triangles.Add (currentVerticesIndex+3);

					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 4;


					Vector3[] pointsCeiling = CreateFloorUnit (new Vector3 (x* UnitToPixel(1), (floorCount+1) * UnitToPixel(houseData.houseConfig.roomHeight)-((float)houseData.houseConfig.wallThickness)/2, z* UnitToPixel(1)));

					vertices.Add (pointsCeiling [0]); 
					vertices.Add (pointsCeiling [1]); 
					vertices.Add (pointsCeiling [2]); 
					vertices.Add (pointsCeiling [3]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+2);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+3);
					triangles.Add (currentVerticesIndex+2);

					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 4;
				}

			}
		}

		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}
	public void CreateRoof(){
		floorCount = 1;
		//Vector3[,,] mapMeshPoints = new Vector3[houseData.houseConfig.houseWidth,houseData.houseConfig.houseLength,4];

		List<int> triangles = new List<int> ();
		for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
			for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
				if (houseData.floorPlan.area [x, z] != UnitType.EMPTY && houseData.floorPlan.area [x, z] != UnitType.GARDEN) {

					Vector3[] points = CreateFloorUnit (new Vector3 (x* UnitToPixel(1), floorCount * UnitToPixel(houseData.houseConfig.roomHeight), z* UnitToPixel(1)));
					//					for(int i = 0; i< 4; i++){
					//						mapMeshPoints [x, z, i] = points [i];
					//					}					
					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex+2);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+1);
					triangles.Add (currentVerticesIndex+2);
					triangles.Add (currentVerticesIndex+3);

					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 4;
				}

			}
		}

		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}
	private void OnDrawGizmos(){
		foreach (Vector3 v in vertices) {
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere (v, 0.1f);
		}
	}

	public Vector3[] CreateFloorUnit(Vector3 topLeft){
		Vector3[] points = new Vector3[4];
		points [3] = topLeft;
		points [1] = topLeft + Vector3.forward*UnitToPixel(1);
		points [2] = points [3] + Vector3.right * UnitToPixel (1);
		points [0] = points [1] + Vector3.right * UnitToPixel (1);
		return points;
	
	}

	public Vector3[] CreateWallUnit(Vector3 topLeft){
		Vector3[] points = new Vector3[4];
		points [3] = topLeft;
		points [1] = topLeft + Vector3.forward*UnitToPixel(1);
		points [2] = points [3] + Vector3.right * UnitToPixel (1);
		points [0] = points [1] + Vector3.right * UnitToPixel (1);
		return points;

	}

	public int UnitToPixel(int units){
		return (int)(units * houseData.houseConfig.blocksPerUnit * houseData.houseConfig.blockWidth);
	}

	public void CreateOuterWalls(){

		List<int> triangles = new List<int> ();

		for (int y = 0; y < houseData.houseConfig.roomHeight; y++) {
			for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
				for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
					Vector3[] points = new Vector3[4];
					if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_DOWN 
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_DOWN && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_DOWN && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {
					
						//Vector3[] points = CreateFloorUnit (new Vector3 (x* UnitToPixel(1), floorCount * UnitToPixel(houseData.houseConfig.roomHeight), z* UnitToPixel(1)));

						points [0] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z+((float)houseData.houseConfig.wallThickness)/2);
						points [1] = points [0] + Vector3.right * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_LEFT
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_LEFT && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_LEFT && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [0] = new Vector3 (x+((float)houseData.houseConfig.wallThickness)/2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z);
						points [1] = points [0] + Vector3.forward * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_RIGHT
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_RIGHT && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_RIGHT && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [1] = new Vector3 (x+((float)houseData.houseConfig.wallThickness)/2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z);
						points [0] = points [1] + Vector3.forward * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_UP
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_UP && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.OUTERWALL_UP && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [1] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z+((float)houseData.houseConfig.wallThickness)/2);
						points [0] = points [1] + Vector3.right * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);


					} else if(houseData.floorPlan.area [x, z] == UnitType.DOOR && y >= houseData.houseConfig.doorHeight-1){
						
					}

					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 2);
					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 2);


					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 4;
				}
			}
		}


		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}
	public void CreateInnerWalls(){

		List<int> triangles = new List<int> ();

		for (int y = 0; y < houseData.houseConfig.roomHeight; y++) {
			for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
				for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
					Vector3[] points = new Vector3[4];
					if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_DOWN
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_DOWN && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_DOWN && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						//Vector3[] points = CreateFloorUnit (new Vector3 (x* UnitToPixel(1), floorCount * UnitToPixel(houseData.houseConfig.roomHeight), z* UnitToPixel(1)));

						points [0] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), (float)z+((float)houseData.houseConfig.wallThickness));
						points [1] = points [0] + Vector3.right * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_LEFT
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_LEFT && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_LEFT && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [0] = new Vector3 ((float)x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z);
						points [1] = points [0] + Vector3.forward * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_RIGHT
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_RIGHT && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_RIGHT && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [1] = new Vector3 ((float)x+((float)houseData.houseConfig.wallThickness), floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), z);
						points [0] = points [1] + Vector3.forward * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_UP
						|| (houseData.floorPlan.area [x, z] == UnitType.DOOR &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_UP && y >= houseData.houseConfig.doorHeight)
						|| (houseData.floorPlan.area [x, z] == UnitType.WINDOW &&houseData.floorPlan.doorInfo[x*(double)1000+z/(double)1000]==UnitType.INNERWALL_UP && (y < houseData.houseConfig.windowPositionY||y >= houseData.houseConfig.windowPositionY + houseData.houseConfig.windowHeight))) {

						points [1] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight)+y*UnitToPixel(1), (float)z);
						points [0] = points [1] + Vector3.right * UnitToPixel (1);
						points [3] = points [1] + Vector3.up * UnitToPixel (1);
						points [2] = points [0] + Vector3.up * UnitToPixel (1);


					} 

					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 2);
					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 2);


					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 4;
				}
			}
		}


		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}
	public void CreateOuterWallCorners(){

		List<int> triangles = new List<int> ();

		for (int y = 0; y < houseData.houseConfig.roomHeight; y++) {
			for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
				for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
					Vector3[] points = new Vector3[6];

					if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_DL_I) {
						points [2] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [2] + Vector3.right * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.forward * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_DL_O) {

						points [2] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness) / 2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z);
						points [1] = points [2] + Vector3.forward * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.right * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_DR_I) {

						points [2] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness) / 2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness));
						points [1] = points [2] + Vector3.back * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.right * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_DR_O) {

						points [2] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [2] + Vector3.right * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.back * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_TL_I) {


						points [2] = new Vector3 (x + houseData.houseConfig.wallThickness / 2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z);
						points [1] = points [2] + Vector3.forward * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.left * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_TL_O) {

						points [2] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness), floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [2] + Vector3.left * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.forward * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_TR_I) {

						points [2] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness), floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z+ ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [2] + Vector3.left * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.back * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_CORNER_TR_O) {

						points [2] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness) /2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness));
						points [1] = points [2] + Vector3.back * UnitToPixel (1) / 2;
						points [0] = points [1] + Vector3.left * UnitToPixel (1) / 2;
						points [5] = points [2] + Vector3.up * UnitToPixel (1);
						points [4] = points [1] + Vector3.up * UnitToPixel (1);
						points [3] = points [0] + Vector3.up * UnitToPixel (1);

					}

					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);
					vertices.Add (points [4]);
					vertices.Add (points [5]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 1);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 4);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 4);
					triangles.Add (currentVerticesIndex + 5);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 5);
					triangles.Add (currentVerticesIndex + 2);

					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x+UnitToPixel(1)/2,z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1)/2,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 6;
				}
			}
		}


		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}

	/*public void CreateInnerWallCorners(){

		List<int> triangles = new List<int> ();

		for (int y = 0; y < houseData.houseConfig.roomHeight; y++) {
			for (int x = 0; x < houseData.houseConfig.houseWidth; x++) {
				for (int z = 0; z < houseData.houseConfig.houseLength; z++) {
					Vector3[] points = new Vector3[6];

					Vector3 offset = new Vector3 (0, 0, 0);
					if (y == houseData.houseConfig.roomHeight - 1) {
						offset = new Vector3 (0, houseData.houseConfig.wallThickness / 2, 0);
					}

					if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_CORNER_TR) {

						points [0] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness) , floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z);
						points [1] = points [0] + Vector3.forward * UnitToPixel (1) / 2;
						points [2] = points [1] + Vector3.right * UnitToPixel (1) / 2;
						points [3] = points [0] + Vector3.up * UnitToPixel (1) - offset;
						points [4] = points [1] + Vector3.up * UnitToPixel (1) - offset;
						points [5] = points [2] + Vector3.up * UnitToPixel (1) - offset;

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_CORNER_TL) {

						points [0] = new Vector3 (x, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [0] + Vector3.right * UnitToPixel (1) / 2;
						points [2] = points [1] + Vector3.back * UnitToPixel (1) / 2;
						points [3] = points [0] + Vector3.up * UnitToPixel (1) - offset;
						points [4] = points [1] + Vector3.up * UnitToPixel (1) - offset;
						points [5] = points [2] + Vector3.up * UnitToPixel (1) - offset;

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_CORNER_DR) {

						points [0] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness), floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness) / 2);
						points [1] = points [0] + Vector3.left * UnitToPixel (1) / 2;
						points [2] = points [1] + Vector3.forward * UnitToPixel (1) / 2;
						points [3] = points [0] + Vector3.up * UnitToPixel (1) - offset;
						points [4] = points [1] + Vector3.up * UnitToPixel (1) - offset;
						points [5] = points [2] + Vector3.up * UnitToPixel (1) - offset;

					} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_CORNER_DL) {

						points [0] = new Vector3 (x + ((float)houseData.houseConfig.wallThickness) /2, floorCount * UnitToPixel (houseData.houseConfig.roomHeight) + y * UnitToPixel (1), z + ((float)houseData.houseConfig.wallThickness));
						points [1] = points [0] + Vector3.back * UnitToPixel (1) / 2;
						points [2] = points [1] + Vector3.left * UnitToPixel (1) / 2;
						points [3] = points [0] + Vector3.up * UnitToPixel (1) - offset;
						points [4] = points [1] + Vector3.up * UnitToPixel (1) - offset;
						points [5] = points [2] + Vector3.up * UnitToPixel (1) - offset;

					}

					vertices.Add (points [0]); 
					vertices.Add (points [1]); 
					vertices.Add (points [2]); 
					vertices.Add (points [3]);
					vertices.Add (points [4]);
					vertices.Add (points [5]);

					//Debug.Log ("P0 = " + mapMeshPoints [x, z, 0] + " P1 = " + mapMeshPoints [x, z, 1] + " P2 = " + mapMeshPoints [x, z, 2] + " P3 = " + mapMeshPoints [x, z, 3]);

					triangles.Add (currentVerticesIndex);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 1);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 3);
					triangles.Add (currentVerticesIndex + 4);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 4);
					triangles.Add (currentVerticesIndex + 5);

					triangles.Add (currentVerticesIndex + 1);
					triangles.Add (currentVerticesIndex + 5);
					triangles.Add (currentVerticesIndex + 2);

					uvs.Add (new Vector2(x,z));
					uvs.Add (new Vector2(x+UnitToPixel(1)/2,z));
					uvs.Add (new Vector2(x+UnitToPixel(1),z));
					uvs.Add (new Vector2(x,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1)/2,z+UnitToPixel(1)));
					uvs.Add (new Vector2(x+UnitToPixel(1),z+UnitToPixel(1)));


					currentVerticesIndex += 6;
				}
			}
		}


		this.materials.Add(new Material(Shader.Find("Diffuse")));
		this.mesh.subMeshCount = ++subMeshCount;
		this.mesh.vertices = vertices.ToArray();
		this.mesh.SetTriangles (triangles.ToArray (),this.mesh.subMeshCount-1);
	}*/

}
