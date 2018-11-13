using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData {

	public Point2D topLeft, topRight, bottomRight, bottomLeft;
	public int width, length;

//	public UnitType[,] localArea;

	public RoomData(int x, int z, int width, int lenght){
		this.width = width;
		this.length = lenght;
		this.topLeft = new Point2D (x, z);
		this.topRight = new Point2D (topLeft.x + width-1, topLeft.z);
		this.bottomRight = new Point2D (topLeft.x + width-1, topLeft.z + lenght-1);
		this.bottomLeft = new Point2D (topLeft.x, topLeft.z + lenght-1);
//		localArea = new UnitType[width,lenght];
	}

}
