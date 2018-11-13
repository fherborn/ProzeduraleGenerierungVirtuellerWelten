using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class House : MonoBehaviour {

	public HouseData houseData;

	public void CreateMesh(HouseData houseData){


	}



	public void CreateFoundation(){
		Vector3[,,] mapMeshPoints = Vector3[houseData.houseConfig.houseWidth,houseData.houseConfig.houseLength,4];
		for (int x = 0; x < mapMeshPoints.GetLength (0); x++) {
			for (int z = 0; z < mapMeshPoints.GetLength (1); z++) {
				if (houseData.floorPlan.area [x, z] == UnitType.INNER_GROUND) {
					
				}

			}
		}
	}


	public Vector3[] CreateFloor(Vector3 topLeft){
		Vector3[] points = new Vector3[4];
		points [3] = topLeft;
		points [1] = topLeft + Vector3.forward*UnitToPixel(1);
		points [2] = points [3] + Vector3.right * UnitToPixel (1);
		points [0] = points [1] + Vector3.right * UnitToPixel (1);
	
	}

	public int UnitToPixel(int units){
		return units * houseData.houseConfig.blocksPerUnit * houseData.houseConfig.blockWidth;
	}




}
