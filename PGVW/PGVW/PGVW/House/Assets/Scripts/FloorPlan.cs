using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlan {

	//Type of Unit for whole Map
	public UnitType[,] area;
	//Room references for each Unit
	public RoomData[,] roomData;
	//All Rooms
	public List<RoomData> allRoomData;
	public Dictionary<double, UnitType> doorInfo;

	public FloorPlan(int width, int length){
		area = new UnitType[width,length];
		roomData = new RoomData[width, length];
		allRoomData = new List<RoomData> ();
		doorInfo = new Dictionary<double, UnitType> ();
	}

}
