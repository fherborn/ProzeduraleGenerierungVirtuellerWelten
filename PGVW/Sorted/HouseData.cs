using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseData{

	public FloorPlan floorPlan;
	public HouseConfig houseConfig;
	public List<RoomPair> roomPairs;

	public Texture2D previewTexture;

	public HouseData(HouseConfig houseConfig){
		this.houseConfig = houseConfig;
	}

	public HouseData(){
		this.houseConfig = new HouseConfig ();
	}

}
