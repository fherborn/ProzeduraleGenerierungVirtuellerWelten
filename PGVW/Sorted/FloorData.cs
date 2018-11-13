using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorData {

	//Type of Unit for whole Map
	public UnitType[,] area;
	//Room references for each Unit
	public RoomData[,] roomData;
	//All Rooms
	public List<RoomData> allRoomData;

	public FloorData(int width, int length){
		area = new UnitType[width,length];
		roomData = new RoomData[width, length];
	}

}
