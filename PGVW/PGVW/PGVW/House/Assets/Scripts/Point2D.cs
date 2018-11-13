using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Point2D  : IEquatable<Point2D>{

	public int x,z;

	public Point2D(int x, int z){
		this.x = x;
		this.z = z;
	}

	public bool Equals(Point2D other){
		return this.x == other.x && this.z == other.z;
	}

}
