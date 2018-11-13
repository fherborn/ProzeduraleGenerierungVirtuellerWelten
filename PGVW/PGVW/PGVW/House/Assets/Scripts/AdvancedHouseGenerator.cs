using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedHouseGenerator : MonoBehaviour {

	public HouseConfig currentHouseConfig = new HouseConfig ();
	private System.Random randomGenerator = new System.Random();	
	public HouseData currentHouseData = new HouseData();
	public House currentHouse;


	public HouseData CreateNewHouse(){
		currentHouseData = new HouseData (currentHouseConfig);
		return currentHouseData;
	}

	public HouseData CreateFullHouse(HouseData houseData){
		houseData = CreateNewHouse ();
		houseData = GenerateFloorPlan (houseData); 
		houseData = GenerateOuterWalls (houseData); 
		houseData = GenerateInnerDoors (houseData); 
		houseData = GenerateWindowsAndOuterDoors (houseData); 
		return houseData;
	}

	public House CreateHouse(HouseData houseData){
		currentHouse = new House ();
		currentHouse.CreateMesh (houseData);
		return currentHouse;
	}

	public HouseData GenerateFloorPlan(HouseData houseData){
		houseData.floorPlan = new FloorPlan (houseData.houseConfig.houseWidth, houseData.houseConfig.houseLength);
		for (int x = 1; x < houseData.floorPlan.area.GetLength (0) - 1; x++) {
			for (int z = 1; z < houseData.floorPlan.area.GetLength (1) - 1; z++) {
				houseData = TryToCreateRoom (houseData, x, z);
			}
		}
		//TryToCreateRoom (ref houseData.floorPlan, 1, 1);
		return houseData;
	}

	static int CorrectLength (int x, int z, FloorPlan floorPlan, int tmpRoomLength)
	{
		for (int i = z ; i < z + tmpRoomLength; i++) {
			if (floorPlan.roomData [x, i] != null) {
				//Wenn ein Raum im Weg ist
				return i-z-1;
				//return floorPlan;
			}
		}
		return tmpRoomLength;
	}

	public HouseData TryToCreateRoom(HouseData houseData, int x, int z){
		HouseConfig config = houseData.houseConfig;
		FloorPlan floorPlan = houseData.floorPlan;
		int roomWidth = Mathf.Min (config.houseWidth - x - 1,
			               			//10);
			                 		randomGenerator.Next (config.roomWidthMax - config.roomWidthMin) + config.roomWidthMin);
		int tmpRoomLength = Mathf.Min (config.houseLength - z - 1,
										//10);
										randomGenerator.Next (config.roomLengthMax - config.roomLengthMin) + config.roomLengthMin);
//		int roomWidth = 10, roomLength = 10;
		if (x + roomWidth >= config.houseWidth || z + tmpRoomLength >= config.houseLength) {
			return houseData;
		}

		int roomLength = CorrectLength (x, z, floorPlan, tmpRoomLength);

		if (roomWidth < config.roomWidthMin || roomLength < config.roomLengthMin) {
			return houseData;
		}
//		Debug.Log ("Room Width: " + roomWidth);
//		Debug.Log ("Room WidthMin: " + config.roomWidthMin);
//		Debug.Log ("Room Length: " + roomLength);
//		Debug.Log ("Room LengthMin: " + config.roomLengthMin);
		houseData = CreateRoom(x,z,roomWidth, roomLength, houseData);
		return houseData;
	}

	public HouseData CreateRoom(int x, int z, int roomWidth, int roomLength, HouseData houseData){
		RoomData room = new RoomData (x, z, roomWidth, roomLength);
		FloorPlan floorPlan = houseData.floorPlan;
		Debug.Log (x+","+z);

		//Create Walls
		UnitType wallType;
		for (int globalX = x; globalX < x + roomWidth; globalX++) {
			for (int globalZ = z; globalZ < z + roomLength; globalZ++) {
				if (globalX == x) {											//Linke Kante
					if (globalZ == z) {										//Obere Kante
						wallType = UnitType.INNERWALL_CORNER_DR;			//Linke Obere Eckwand (Facing Down-Right)
					} else if (globalZ == z + roomLength - 1) {				//Untere Kante
						wallType = UnitType.INNERWALL_CORNER_TR;			//Linke Untere Eckwand (Facing Top-Right)
					} else {												//Zwischen oberer und unterer Kante
						wallType = UnitType.INNERWALL_RIGHT;				//Linke Seitenwand (Facing Right)
					}														//
				} else if (globalX == x + roomWidth - 1) {					//Rechte Kante
					if (globalZ == z) {										//Obere Kante
						wallType = UnitType.INNERWALL_CORNER_DL;			//Rechte Obere Eckwand (Facing Down-Left)
					} else if (globalZ == z + roomLength - 1) {				//Untere Kante
						wallType = UnitType.INNERWALL_CORNER_TL;			//Rechte Untere Eckwand (Facing Top-Left)
					} else {												//Zwischen oberer und unterer Kante
						wallType = UnitType.INNERWALL_LEFT;					//Rechte Seitenwand (Facing Left)
					}														//
				} else {													//Zwischen linker und rechter Kante
					if (globalZ == z) {										//Obere Kante
						wallType = UnitType.INNERWALL_DOWN;					//Obere Seitenwand
					} else if (globalZ == z + roomLength - 1) {				//Untere Kante
						wallType = UnitType.INNERWALL_UP;					//Untere Seitenwand
					} else {												//
						wallType = UnitType.INNER_GROUND;					//Fußboden im Fall, dass es keine Kante berührt
					}
				}
				//Debug.Log (globalX+","+globalZ-1);
				floorPlan.area [globalX, globalZ] = wallType;
				floorPlan.roomData [globalX, globalZ] = room;
			}
		}

		floorPlan.allRoomData.Add (room);

		houseData.floorPlan = floorPlan;

		return houseData;
	}

	public HouseData FixRooms(HouseData houseData){
		RoomData currentRoom = houseData.floorPlan.roomData [1, 1];
		List<Point2D> pointsOfInterest = new List<Point2D> ();
		bool roomOfInterest = true;
		bool pointFound = false;

//		for (int x = 1; x < houseData.houseConfig.houseWidth - 1; x++) {
//			for (int z = 1; z < houseData.houseConfig.houseLength - 1; z++) {
//				if(houseData.floorPlan.area[x,z] != null && houseData.floorPlan.area [x,z+1] == null )			
//			
//			
//			}
//		}





		int currentRoomIndex = 0;


		for (int z = 1; z < houseData.floorPlan.roomData.GetLength(1)-1; z++) {
			for (int x = 1; x < houseData.floorPlan.roomData.GetLength (0)-1; x++) {
				if (currentRoom != houseData.floorPlan.roomData [x, z] ) {
					if (currentRoom != null) {
						if (roomOfInterest) {
							//Raum vergrößern
							currentRoom.length += 1;

							foreach (Point2D point in pointsOfInterest) {
								//HouseArea anpassen
								if (point.x == currentRoom.topLeft.x) {
									houseData.floorPlan.area [point.x, point.z - 1] = UnitType.INNERWALL_LEFT;
									houseData.floorPlan.area [point.x, point.z] = UnitType.INNERWALL_CORNER_DL;
								} else if (point.x == currentRoom.topRight.x) {
									houseData.floorPlan.area [point.x, point.z - 1] = UnitType.INNERWALL_RIGHT;
									houseData.floorPlan.area [point.x, point.z] = UnitType.INNERWALL_CORNER_DR;
								} else {
									houseData.floorPlan.area [point.x, point.z - 1] = UnitType.INNER_GROUND;
									houseData.floorPlan.area [point.x, point.z] = UnitType.INNERWALL_DOWN;
								}
								//HouseRoomData anpassen
								//houseData.floorPlan.roomData[point.x,point.z] = currentRoom;
							}
							houseData = CreateRoom (currentRoom.topLeft.x,currentRoom.topLeft.z,currentRoom.width, currentRoom.length, houseData);
						}
					}

					//Raum merken
					currentRoom = houseData.floorPlan.roomData [x, z];
					currentRoomIndex = 0;
					pointsOfInterest = new List<Point2D> ();
					roomOfInterest = true;
					pointFound = false;

				} else {
					currentRoomIndex++;
					//Point of interest hinzufügen wenn Lücke
					RoomData firstRoomAfter = GetFieldWithoutOutOfRange(houseData.floorPlan.roomData, x,z+1);
					RoomData secondRoomAfter = GetFieldWithoutOutOfRange(houseData.floorPlan.roomData, x,z+2);

					if (!pointFound && firstRoomAfter == null && secondRoomAfter != null && currentRoomIndex == 0) {
						//Lücke besteht also Lücke merken
						pointsOfInterest.Add (new Point2D (x, z + 1));
						pointFound = true;

					} else if (pointFound && firstRoomAfter == null) {
						pointsOfInterest.Add (new Point2D (x, z + 1));
					}else{
						roomOfInterest = false;
					}
				}
			}
		}



		return houseData;
	}

	public RoomData GetFieldWithoutOutOfRange(RoomData[,] roomData, int x, int z ){
		if (x < 0 || z < 0 || x > roomData.GetLength (0) - 1 || z > roomData.GetLength (1) - 1)
			return null;
		return roomData [x, z];
	}

	public HouseData GenerateOuterWalls(HouseData houseData){

		for (int x = 0; x < houseData.floorPlan.roomData.GetLength(0); x++) {
			for (int z = 0; z < houseData.floorPlan.roomData.GetLength(1); z++) {

				//Alle RoomInfos die wir haben
				if (houseData.floorPlan.roomData [x, z] == null) {
					//Hier ist eine potentielle Außenwand

					RoomData[,] fields = houseData.floorPlan.roomData;
					UnitType wallType;

					//Normal Walls
					//Fall OUTERWALL_LEFT
					if (
						GetFieldWithoutOutOfRange (fields, x + 1, z) != null &&
						GetFieldWithoutOutOfRange (fields, x, z + 1) == null &&
						GetFieldWithoutOutOfRange (fields, x, z - 1) == null) {

						wallType = UnitType.OUTERWALL_LEFT;
					} else
						//Fall OUTERWALL_UP
						if (
							GetFieldWithoutOutOfRange (fields, x + 1, z) == null &&
							GetFieldWithoutOutOfRange (fields, x, z + 1) != null &&
							GetFieldWithoutOutOfRange (fields, x - 1, z) == null) {

							wallType = UnitType.OUTERWALL_UP;
						} else
							//Fall OUTERWALL_RIGHT
							if (
								GetFieldWithoutOutOfRange (fields, x, z - 1) == null &&
								GetFieldWithoutOutOfRange (fields, x, z + 1) == null &&
								GetFieldWithoutOutOfRange (fields, x - 1, z) != null) {

								wallType = UnitType.OUTERWALL_RIGHT;
							} else
								//Fall OUTERWALL_DOWN
								if (
									GetFieldWithoutOutOfRange (fields, x, z - 1) != null &&
									GetFieldWithoutOutOfRange (fields, x + 1, z) == null &&
									GetFieldWithoutOutOfRange (fields, x - 1, z) == null) {

									wallType = UnitType.OUTERWALL_DOWN;
								} else
									//Outer Corners
									//Fall OUTERWALL_CORNER_TL_O
									if (
										GetFieldWithoutOutOfRange (fields, x + 1, z) == null &&
										GetFieldWithoutOutOfRange (fields, x, z + 1) == null &&
										GetFieldWithoutOutOfRange (fields, x + 1, z + 1) != null) {

										wallType = UnitType.OUTERWALL_CORNER_TL_O;
									} else
										//Fall OUTERWALL_CORNER_DL_O
										if (
											GetFieldWithoutOutOfRange (fields, x + 1, z) == null &&
											GetFieldWithoutOutOfRange (fields, x, z - 1) == null &&
											GetFieldWithoutOutOfRange (fields, x + 1, z - 1) != null) {

											wallType = UnitType.OUTERWALL_CORNER_DL_O;
										} else
											//Fall OUTERWALL_CORNER_DR_O
											if (
												GetFieldWithoutOutOfRange (fields, x - 1, z) == null &&
												GetFieldWithoutOutOfRange (fields, x, z - 1) == null &&
												GetFieldWithoutOutOfRange (fields, x - 1, z - 1) != null) {

												wallType = UnitType.OUTERWALL_CORNER_DR_O;
											} else
												//Fall OUTERWALL_CORNER_TR_O
												if (
													GetFieldWithoutOutOfRange (fields, x - 1, z) == null &&
													GetFieldWithoutOutOfRange (fields, x, z + 1) == null &&
													GetFieldWithoutOutOfRange (fields, x - 1, z + 1) != null) {

													wallType = UnitType.OUTERWALL_CORNER_TR_O;
												} else
													//Inner Corners
													//Fall OUTERWALL_CORNER_DR_I
													if (
														GetFieldWithoutOutOfRange (fields, x - 1, z) != null &&
														GetFieldWithoutOutOfRange (fields, x, z - 1) != null) {

														wallType = UnitType.OUTERWALL_CORNER_DR_I;
													} else
														//Fall OUTERWALL_CORNER_DL_I
														if (
															GetFieldWithoutOutOfRange (fields, x + 1, z) != null &&
															GetFieldWithoutOutOfRange (fields, x, z - 1) != null) {

															wallType = UnitType.OUTERWALL_CORNER_DL_I;
														} else
															//Fall OUTERWALL_CORNER_TR_I
															if (
																GetFieldWithoutOutOfRange (fields, x - 1, z) != null &&
																GetFieldWithoutOutOfRange (fields, x, z + 1) != null) {

																wallType = UnitType.OUTERWALL_CORNER_TR_I;
															} else
																//Fall OUTERWALL_CORNER_TL_I
																if (
																	GetFieldWithoutOutOfRange (fields, x + 1, z) != null &&
																	GetFieldWithoutOutOfRange (fields, x, z + 1) != null) {

																	wallType = UnitType.OUTERWALL_CORNER_TL_I;
																} else {
																	wallType = UnitType.GARDEN;
																}

					houseData.floorPlan.area [x, z] = wallType;
				}
			}
		}
		return houseData;
	}



	public HouseData GenerateInnerDoors(HouseData houseData){
		houseData.roomPairs = new List<RoomPair> ();
		for (int x = 2; x < houseData.floorPlan.roomData.GetLength (0)-2; x++) {
			for (int z = 2; z < houseData.floorPlan.roomData.GetLength (1)-2; z++) {
				if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_DOWN) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x, z - 1] == UnitType.INNERWALL_UP) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						RoomPair roomPair = GetExistingRoomPair(ref houseData.roomPairs, new RoomPair(houseData.floorPlan.roomData[x,z], houseData.floorPlan.roomData[x,z-1]));
						roomPair.doorPoints.Add (new Point2DTupel (x, z, x, z - 1));
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_UP) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x, z + 1] == UnitType.INNERWALL_DOWN) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						RoomPair roomPair = GetExistingRoomPair(ref houseData.roomPairs, new RoomPair(houseData.floorPlan.roomData[x,z], houseData.floorPlan.roomData[x,z+1]));
						roomPair.doorPoints.Add (new Point2DTupel (x, z, x, z + 1));
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_LEFT) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x+1, z] == UnitType.INNERWALL_RIGHT) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						RoomPair roomPair = GetExistingRoomPair(ref houseData.roomPairs, new RoomPair(houseData.floorPlan.roomData[x,z], houseData.floorPlan.roomData[x+1,z]));
						roomPair.doorPoints.Add (new Point2DTupel (x, z, x+1, z));
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.INNERWALL_RIGHT) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x-1, z] == UnitType.INNERWALL_LEFT) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						RoomPair roomPair = GetExistingRoomPair(ref houseData.roomPairs, new RoomPair(houseData.floorPlan.roomData[x,z], houseData.floorPlan.roomData[x-1,z]));
						roomPair.doorPoints.Add (new Point2DTupel (x, z, x-1, z));
					}
				}


			}
		}


		foreach (RoomPair rp in houseData.roomPairs) {
			Point2DTupel doorPlaces = rp.doorPoints [randomGenerator.Next (rp.doorPoints.Count)];
			houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point1.x*(double)1000+(double)doorPlaces.point1.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z]);
			houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point2.x*(double)1000+(double)doorPlaces.point2.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z]);

			houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z] = UnitType.DOOR;
			houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z] = UnitType.DOOR;
		}

		return houseData;
	}

	public RoomPair GetExistingRoomPair(ref List<RoomPair> list, RoomPair equal){
		foreach (RoomPair rp in list) {
			if (rp.Equals (equal))
				return rp;
		}
		list.Add (equal);
		return equal;
	}


	public HouseData GenerateWindowsAndOuterDoors(HouseData houseData){
		Dictionary<RoomData, List<Point2DTupel>> dictionary = new Dictionary<RoomData, List<Point2DTupel>> ();
		for (int x = 0; x < houseData.floorPlan.roomData.GetLength (0) ; x++) {
			for (int z = 0; z < houseData.floorPlan.roomData.GetLength (1) ; z++) {
				if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_DOWN) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x, z - 1] == UnitType.INNERWALL_UP && CheckDownContainsDoor(x, z, houseData)) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						List<Point2DTupel> roomTupels;
						if (!dictionary.ContainsKey(houseData.floorPlan.roomData[x,z-1])) {
							roomTupels = new List<Point2DTupel> ();
						} else {
							roomTupels = dictionary [houseData.floorPlan.roomData [x, z-1]];
						}
						roomTupels.Add(new Point2DTupel(x,z,x,z-1));
						dictionary [houseData.floorPlan.roomData [x, z-1]] = roomTupels;
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_UP) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x, z + 1] == UnitType.INNERWALL_DOWN && CheckUpContainsDoor(x,z, houseData)) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						List<Point2DTupel> roomTupels;
						if (!dictionary.ContainsKey(houseData.floorPlan.roomData[x,z+1])) {
							roomTupels = new List<Point2DTupel> ();
						} else {
							roomTupels = dictionary [houseData.floorPlan.roomData [x, z+1]];
						}
						roomTupels.Add(new Point2DTupel(x,z,x,z+1));
						dictionary [houseData.floorPlan.roomData [x, z+1]] = roomTupels;
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_LEFT) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x+1, z] == UnitType.INNERWALL_RIGHT&&CheckLeftContainsDoor(x,z, houseData)) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						List<Point2DTupel> roomTupels;
						//if (dictionary [houseData.floorPlan.roomData [x+1, z]] == null) {
						if(!dictionary.ContainsKey(houseData.floorPlan.roomData[x+1,z])){
							roomTupels = new List<Point2DTupel> ();
						} else {
							roomTupels = dictionary [houseData.floorPlan.roomData [x+1, z]];
						}
						roomTupels.Add(new Point2DTupel(x,z,x+1,z));
						dictionary [houseData.floorPlan.roomData [x+1, z]] = roomTupels;
					}
				} else if (houseData.floorPlan.area [x, z] == UnitType.OUTERWALL_RIGHT) {				//Mögliche Wand für eine Tür
					if (houseData.floorPlan.area [x-1, z] == UnitType.INNERWALL_LEFT&&CheckRightContainsDoor(x,z, houseData)) {			//Angrenzendes Feld ist das Gegenstück zur Wand
						List<Point2DTupel> roomTupels;
						if (!dictionary.ContainsKey(houseData.floorPlan.roomData[x-1,z])) {
							roomTupels = new List<Point2DTupel> ();
						} else {
							roomTupels = dictionary [houseData.floorPlan.roomData [x-1, z]];
						}
						roomTupels.Add(new Point2DTupel(x,z,x-1,z));
						dictionary [houseData.floorPlan.roomData [x-1, z]] = roomTupels;
					}
				}
			}
		}

		List<RoomData> roomsWithDoors = new List<RoomData> ();
		int doorCount = randomGenerator.Next (houseData.houseConfig.entranceDoorCount - 1) + 1;
		for (int i = 0; i < doorCount; i++) {
			RoomData key = dictionary.Keys.ElementAt (randomGenerator.Next (dictionary.Count));
			if (!roomsWithDoors.Contains (key)) {
				roomsWithDoors.Add (key);
				List<Point2DTupel> doorPoints = dictionary [key];
				Point2DTupel doorPlaces = doorPoints [randomGenerator.Next (doorPoints.Count)];
				houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point1.x*(double)1000+(double)doorPlaces.point1.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z]);
				houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point2.x*(double)1000+(double)doorPlaces.point2.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z]);
				houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z] = UnitType.DOOR;
				houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z] = UnitType.DOOR;
				doorPoints.Remove (doorPlaces);
				dictionary [key] = doorPoints;
			}
		}

		for (int k = 0;k<dictionary.Keys.Count;k++) {
			
			RoomData key = dictionary.Keys.ElementAt (k);
			List<Point2DTupel> windowPoints = dictionary [key];
			int windowCount = randomGenerator.Next ((int)(windowPoints.Count * houseData.houseConfig.windowProbability));

			for (int i = 0; i < windowCount; i++) {
				Point2DTupel doorPlaces = windowPoints [randomGenerator.Next (windowPoints.Count-1)+1];
				houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point1.x*(double)1000+(double)doorPlaces.point1.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z]);
				houseData.floorPlan.doorInfo.Add ((double)doorPlaces.point2.x*(double)1000+(double)doorPlaces.point2.z/(double)1000 , houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z]);
				houseData.floorPlan.area [doorPlaces.point1.x, doorPlaces.point1.z] = UnitType.WINDOW;
				houseData.floorPlan.area [doorPlaces.point2.x, doorPlaces.point2.z] = UnitType.WINDOW;
				windowPoints.Remove (doorPlaces);
				dictionary [key] = windowPoints;
			}

		}


		return houseData;
	}

	public bool CheckUpContainsDoor(int tx, int tz, HouseData houseData){
		for (int z = tz-1; z > 0; z--) {
			if (houseData.floorPlan.area [tx, z] != null) {
				return false;
			}
		}
		return true;
	}



	public bool CheckDownContainsDoor(int tx, int tz, HouseData houseData){
		for (int z = tz+1; z < houseData.houseConfig.houseLength; z++) {
			if (houseData.floorPlan.area [tx, z] != null) {
				return false;
			}
		}
		return true;
	}

	public bool CheckRightContainsDoor(int tx, int tz, HouseData houseData){
		for (int x = tx+1; x < houseData.houseConfig.houseWidth; x++) {
			if (houseData.floorPlan.area [x, tz] != null) {
				return false;
			}
		}
		return true;
	}

	public bool CheckLeftContainsDoor(int tx, int tz, HouseData houseData){
		for (int x = tx-1; x >0; x--) {
			if (houseData.floorPlan.area [x, tz] != null) {
				return false;
			}
		}
		return true;
	}

}
