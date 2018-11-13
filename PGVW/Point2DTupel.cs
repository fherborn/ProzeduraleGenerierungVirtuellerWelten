using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2DTupel {

	public Point2D point1, point2;

	public Point2DTupel(int x1, int z1, int x2, int z2){
		this.point1 = new Point2D (x1, z1);
		this.point2 = new Point2D (x2, z2);
	}

}
