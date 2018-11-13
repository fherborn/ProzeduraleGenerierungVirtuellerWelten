using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseConfig {

	[Header("Global Settings")]
	public int blocksPerUnit = 1;
	public float blockHeight = 10f;
	public float blockWidth = 10f;
	public float blockLength = 10f ;

	[Header("Room Setting (in Units)")]
	public int roomHeight = 3;
	public int roomWidthMin = 3;
	public int roomWidthMax = 12;
	public int roomLengthMin = 3;
	public int roomLengthMax = 12;

	[Header("Door Settings (in Units)")]
	public int doorWidth = 1;
	public int doorHeight = 2;
	public int entranceDoorCount = 2;

	[Header("Window Settings (in Units)")]
	public int windowWidth = 1;
	public int windowHeight = 1;
	public int windowPositionY = 1;
	public float windowProbability = 0.5f; 

	[Header("House Settings (int Units)")]
	public int floorCount = 1;
	public int houseLength = 50;
	public int houseWidth = 50;
	public int wallThickness = 1;
	public double roomProbability = 1;
}
