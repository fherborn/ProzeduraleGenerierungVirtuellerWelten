using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomPair : IEquatable<RoomPair>{

	public RoomData room1;
	public RoomData room2;
	public List<Point2DTupel> doorPoints;

	public RoomPair(RoomData room1, RoomData room2){
		this.room1 = room1;
		this.room2 = room2;
		doorPoints = new List<Point2DTupel>();
	}

	public bool Equals(RoomPair other){
		return (room1 == other.room1 && room2 == other.room2) || (room1 == other.room2 && room2 == other.room1);
	}



}
