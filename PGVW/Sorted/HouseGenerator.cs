using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour {

	/*public HouseConfig houseConfig = new HouseConfig();
	private System.Random randomGenerator = new System.Random();
	public HouseData currentHouse;
	public Texture2D previewTexture;

	public HouseData GenerateHouse(){
		HouseData houseData = new HouseData (houseConfig.floorCount);
		FloorPlan floorPlan = GenerateFloor ();
		for (int fC = 0; fC < houseData.houseConfig; fC++) {
			houseData.floorData [fC] = floorPlan;
		}
		return houseData;
	}

	public FloorPlan GenerateFloor(){
		//Erstellen von neuen Etagen mit Hilfe von Breite und Länge des Hauses
		FloorPlan floorData = new FloorPlan (houseConfig.houseWidth, houseConfig.houseLength);

//		for (int x = 1; x < houseConfig.houseWidth-1; x++) {
//			for (int z = 1; z < houseConfig.houseLength-1; z++) {									//Jede einzelne Unit in  dieser Etage außer den Aßenwänden
//				if (floorData.roomData [x, z] == null) {											//Die Units, die noch nicht belegt sind
//					//if (randomGenerator.NextDouble () < houseConfig.roomProbability) {				//Raum hat eine Chance zu beginnen (roomProbability)
//						CreateRoom(ref floorData, x,z);												//Erstellung des Raumes
//					//}
//				}
//			}
//		}

		CreateRoom(ref floorData, 1,1);												//Erstellung des Raumes


		//FixRooms (ref floorData);
		//GenerateOuterWalls (ref floorData);

		return floorData;

	}

	public void CreateRoom(ref FloorPlan floorData, int x, int z){
		RoomData roomData = new RoomData (																														//Neue Rauminformation initialisieren
			new Point2D (x, z), 																																//Startpunkt des Hauses
			Mathf.Min (houseConfig.houseWidth - x -1, randomGenerator.Next (houseConfig.roomWidthMax - houseConfig.roomWidthMin) + houseConfig.roomWidthMin), 	//Der minimale Wert aus der retslichen Hausbreite abzüglich der Hausaußenwand und einem Zufallswert zwischen der minimalen und der maximalen Raumgröße
			GetCollisionPoint (floorData, x, z));																												//Ermitteln der Raumlänge
		if (roomData.width < houseConfig.roomWidthMin)																											//Für den Fall, dass der erstellte Raum die mindestgröße unterschreitet
			return;																																				//wird er nicht weiter beachtet
		
		CreateWalls (ref roomData);																																//Rauminnenwände werden erstellt
		AddToFloorData (ref floorData, ref roomData);																											//Raum wird dem Floor hinzugefügt
		
	}

	public int GetCollisionPoint (FloorPlan floorData, int x, int z)
	{
		int tmpLength = Mathf.Min (houseConfig.houseLength - z -1, randomGenerator.Next (houseConfig.roomLengthMax - houseConfig.roomLengthMin) + houseConfig.roomLengthMin);	//Der minimale Wert aus der retslichen Hauslänge abzüglich der Hausaußenwand und einem Zufallswert zwischen der minimalen und der maximalen Raumgröße
		for (int i = z; i < z + tmpLength; i++) {																																//Überprüfung ein anderer Raum im Weg ist
			if (floorData.roomData [x, i] != null)
				return i-1;																																						//Gibt die maximale Länge zurück
		}
		return tmpLength;
	}

	public void CreateWalls(ref RoomData roomData){
		UnitType wallType;
		for (int x = 0; x < roomData.localArea.GetLength(0); x++) {
			for (int z = 0; z < roomData.localArea.GetLength(1); z++) {			//Alle Units innerhalb des Raumes
//				if (x == 0) {														
//					if (z == 0) {
//						wallType = UnitType.INNERWALL_CORNER_DR;
//						//wallType = UnitType.INNER_GROUND;
//					} else if (z == roomData.length-2) {
//						wallType = UnitType.INNERWALL_CORNER_TR;
//						//wallType = UnitType.INNER_GROUND;
//					} else {
//						wallType = UnitType.INNERWALL_RIGHT;
//						//wallType = UnitType.INNER_GROUND;
//					}
//				} else if (x == roomData.width-2) {
//					if (z == 0) {
//						wallType = UnitType.INNERWALL_CORNER_DL;
//						//wallType = UnitType.INNER_GROUND;
//					} else if (z == roomData.length-2) {
//						wallType = UnitType.INNERWALL_CORNER_TL;
//						//wallType = UnitType.INNER_GROUND;
//					} else {
//						wallType = UnitType.INNERWALL_LEFT;
//						//wallType = UnitType.INNER_GROUND;
//					}
//				} else if (z == 0) {
//					wallType = UnitType.INNERWALL_DOWN;
//					//wallType = UnitType.INNER_GROUND;
//				} else if (z == roomData.length-2) {
//					wallType = UnitType.INNERWALL_UP;
//					//wallType = UnitType.INNER_GROUND;
//				//if(x==roomData.topLeft.x||z==roomData.topLeft.z||x==roomData.topRight.x||z==roomData.topRight.z){
//				//	wallType = UnitType.WALL;
//				} else {
					wallType = UnitType.INNER_GROUND;
				//}																	//Hier werden alle 8 Fälle für die Innenwände und der Standardfall für den Boden durchgegangen

				roomData.localArea [x, z] = wallType;								//und das Array in den Rauminforationen erhält den UnitType für jede Unit
			}
		}
		Debug.Log("TL: "+roomData.topLeft.x+","+roomData.topLeft.z);
		Debug.Log("TR: "+roomData.topRight.x+","+roomData.topRight.z);
		Debug.Log("BL: "+roomData.bottomLeft.x+","+roomData.bottomLeft.z);
		Debug.Log("BR: "+roomData.bottomRight.x+","+roomData.bottomRight.z);
		Debug.Log("Length: "+roomData.length);
		Debug.Log("Width: "+roomData.width);
	}
		
	public void AddToFloorData(ref FloorPlan floorData, ref RoomData roomData){
		floorData.allRoomData.Add (roomData);																					//Raum wird zur Raumliste hinzugefügt
		Debug.Log("Room Added");
		for (int x = roomData.topLeft.x; x <= roomData.topRight.x; x++) {
			for (int z = roomData.topLeft.z; z <= roomData.topRight.z; z++) {														//Jede Unit des Raumes wird durchgegangen
				floorData.roomData [x, z] = roomData;																			//
				floorData.area [x, z] = roomData.localArea [x - roomData.topLeft.x, z - roomData.topLeft.z];					//Die Units in der RoomData wird auf die passenden Stelen der FloorData gelegt
//				if (x ==  roomData.topLeft.x || z == roomData.topLeft.z || x == roomData.width-1 || z == roomData.length-1) {
//					floorData.area [x, z] = UnitType.INNERWALL_UP;
//				} else {
//					floorData.area [x, z] = UnitType.INNER_GROUND;
//				}
			}
		}
	}
		

	public void FixRooms(ref FloorPlan floorData){
		RoomData[,] roomData = floorData.roomData;
		RoomData currentRoom = roomData[1,1]; 
		List<Point2D> pointsOfInterest = new List<Point2D> ();
		bool roomOfInterest = true;
		bool pointFound = false;

		for (int x = 1; x < roomData.GetLength (0)-1; x++) {
			for (int z = 1; z < roomData.GetLength(1)-1; z++) {
				if (currentRoom != roomData [x, z]) {
					if (currentRoom != null) {
						if (roomOfInterest) {
							//Raum vergrößern
							currentRoom.length += 1;

							foreach (Point2D point in pointsOfInterest) {
								//HouseArea anpassen
								if (point.x == currentRoom.topLeft.x) {
									floorData.area [point.x, point.z - 1] = UnitType.INNERWALL_LEFT;
									floorData.area [point.x, point.z] = UnitType.INNERWALL_CORNER_DL;
								} else if (point.x == currentRoom.topRight.x) {
									floorData.area [point.x, point.z - 1] = UnitType.INNERWALL_RIGHT;
									floorData.area [point.x, point.z] = UnitType.INNERWALL_CORNER_DR;
								} else {
									floorData.area [point.x, point.z - 1] = UnitType.INNER_GROUND;
									floorData.area [point.x, point.z] = UnitType.INNERWALL_DOWN;
								}
								//HouseRoomData anpassen
								roomData[point.x,point.z] = currentRoom;
							}
							CreateWalls (ref currentRoom);
						}
					}

					//Raum merken
					currentRoom = roomData [x, z];
					pointsOfInterest = new List<Point2D> ();
					roomOfInterest = true;
					pointFound = false;

				} else {
					//Point of interest hinzufügen wenn Lücke
					RoomData firstRoomAfter = GetFieldWithoutOutOfRange(roomData, x,z+1);
					RoomData secondRoomAfter = GetFieldWithoutOutOfRange(roomData, x,z+2);

					if (!pointFound && firstRoomAfter == null && secondRoomAfter != null) {
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
	}




	public void GenerateOuterWalls(ref FloorPlan floorData){
		for (int x = 0; x < floorData.roomData.GetLength(0); x++) {
			for (int z = 0; z < floorData.roomData.GetLength(1); z++) {

				//Alle RoomInfos die wir haben
				if (floorData.roomData [x, z] == null) {
					//Hier ist eine potentielle Außenwand

					RoomData[,] fields = floorData.roomData;
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

					floorData.area [x, z] = wallType;
				}
			}
		}
	}

	public RoomData GetFieldWithoutOutOfRange(RoomData[,] roomData, int x, int z ){
		if (x < 0 || z < 0 || x > roomData.GetLength (0) - 1 || z > roomData.GetLength (1) - 1)
			return null;
		return roomData [x, z];
	}

*/
}
