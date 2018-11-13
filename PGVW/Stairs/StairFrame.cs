using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairFrame {
	
	public Vector3[] points;
	public HouseData houseData;

	public Vector3[] vertecis = new Vector3[12];
	public int[] allTriangles = new int[18];


	public StairFrame(HouseData houseData,Vector3 p0, Vector3 p1){
		this.houseData = houseData;
		buildFrame (p0, p1);
	}

	private void buildFrame(Vector3 p0, Vector3 p1){
		
		points = CalculatePoints(new Vector3[8], p0, p1);

		#region front
		vertecis[0] = points[0];
		vertecis[1] = points[1];
		vertecis[2] = points[2];
		vertecis[3] = points[3];

		allTriangles[0] = 0;   
		allTriangles[1] = 1;   
		allTriangles[2] = 2;

		allTriangles[3] = 2;
		allTriangles[4] = 1;
		allTriangles[5] = 3;
		#endregion

		#region left side
		vertecis[4] = points[0];
		vertecis[5] = points[1];
		vertecis[6] = points[2];
		vertecis[7] = points[3];

		allTriangles[6] = 4;   
		allTriangles[7] = 5;   
		allTriangles[8] = 6;

		allTriangles[9] = 6;
		allTriangles[10] = 5;
		allTriangles[11] = 7;
		#endregion

		#region right side
		vertecis[8] = points[0];
		vertecis[9] = points[1];
		vertecis[10] = points[2];
		vertecis[11] = points[3];

		allTriangles[12] = 8;   
		allTriangles[13] = 9;   
		allTriangles[14] = 10;

		allTriangles[15] = 10;
		allTriangles[16] = 9;
		allTriangles[17] = 11;
		#endregion

	}

	public Vector3[] CalculatePoints(Vector3[] points, Vector3 p0, Vector3 p1) 
	{
		HouseConfig config = houseData.houseConfig;

		int roomHeight = config.roomHeight;
		int treadCount = (int) (config.roomHeight / config.treadHeight);
		int length = treadCount * config.treadLength;

		points[0] = p0 + Vector3.up * roomHeight + Vector3.left * config.treadWidth / 2;
		points[1] = points[0] + Vector3.up * config.treadHeight;
		points[2] = p0 + Vector3.up * roomHeight + Vector3.right * config.treadWidth / 2;
		points[3] = points[2] + Vector3.up * config.treadHeight;

		points[4] = p1 + Vector3.forward * config.treadLength + Vector3.left * config.treadWidth / 2;
		points[5] = points[4] + Vector3.up * config.treadHeight;
		points[6] = p1 + Vector3.forward * config.treadLength + Vector3.right * config.treadWidth / 2;
		points[7] = points[6] + Vector3.up * config.treadHeight;

		return points;

	}
}
