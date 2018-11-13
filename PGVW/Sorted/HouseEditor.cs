using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AdvancedHouseGenerator))]
public class HouseEditor : Editor {
	//Instanz von AdvancedHouseGenerator, zu der der Editor erzeugt wurde
	AdvancedHouseGenerator advancedHouseGenerator;

	public UnitType unittype;
	public Color matColor = Color.white;

	void Awake () {
		advancedHouseGenerator = target as AdvancedHouseGenerator;		
	}



	public override void OnInspectorGUI() {

		//Ertellung der Slider für die einzelnen Variablen eines Hauses

		EditorGUI.BeginChangeCheck ();

		#region GlobalSettings
		EditorGUILayout.BeginVertical ("Box");
		EditorGUILayout.LabelField ("Global Settings (in Units)", EditorStyles.boldLabel);
		EditorGUILayout.LabelField (" ");

		advancedHouseGenerator.currentHouseConfig.blockHeight = EditorGUILayout.Slider ("Block height", advancedHouseGenerator.currentHouseConfig.blockHeight, 1f, 200f);

		advancedHouseGenerator.currentHouseConfig.blockWidth = EditorGUILayout.Slider ("Block Width", advancedHouseGenerator.currentHouseConfig.blockWidth, 1f, 200f);

		advancedHouseGenerator.currentHouseConfig.blockLength = EditorGUILayout.Slider ("Block length", advancedHouseGenerator.currentHouseConfig.blockLength, 1f, 200f);

		advancedHouseGenerator.currentHouseConfig.blocksPerUnit = EditorGUILayout.IntSlider ("Blocks per Unit", advancedHouseGenerator.currentHouseConfig.blocksPerUnit, 1, 10);
		EditorGUILayout.EndVertical ();
		EditorGUILayout.LabelField (" ");
		#endregion //GlobalSettings

		#region RoomSettings
		EditorGUILayout.BeginVertical ("Box");
		EditorGUILayout.LabelField ("Room Setings (in Units)", EditorStyles.boldLabel);
		EditorGUILayout.LabelField (" ");

		advancedHouseGenerator.currentHouseConfig.roomHeight = EditorGUILayout.IntSlider ("Room height", advancedHouseGenerator.currentHouseConfig.roomHeight, 3, 20);

		advancedHouseGenerator.currentHouseConfig.roomLengthMin = EditorGUILayout.IntSlider ("Room length min", advancedHouseGenerator.currentHouseConfig.roomLengthMin, 3, 20);

		advancedHouseGenerator.currentHouseConfig.roomLengthMax = EditorGUILayout.IntSlider ("Room length max", advancedHouseGenerator.currentHouseConfig.roomLengthMax, advancedHouseGenerator.currentHouseConfig.roomLengthMin, 20);

		advancedHouseGenerator.currentHouseConfig.roomWidthMin = EditorGUILayout.IntSlider ("Room width min", advancedHouseGenerator.currentHouseConfig.roomWidthMin, 3, 20);

		advancedHouseGenerator.currentHouseConfig.roomWidthMax = EditorGUILayout.IntSlider ("Room width max", advancedHouseGenerator.currentHouseConfig.roomWidthMax, advancedHouseGenerator.currentHouseConfig.roomWidthMin, 20);

		EditorGUILayout.EndVertical ();
		EditorGUILayout.LabelField (" ");
		#endregion //RoomSettings

		#region HomeSettings
		EditorGUILayout.BeginVertical ("Box");
		EditorGUILayout.LabelField ("Home Setings (in Units)", EditorStyles.boldLabel);
		EditorGUILayout.LabelField (" ");

		advancedHouseGenerator.currentHouseConfig.floorCount = EditorGUILayout.IntSlider ("Floor count", advancedHouseGenerator.currentHouseConfig.floorCount, 1, 10);

		advancedHouseGenerator.currentHouseConfig.houseLength = EditorGUILayout.IntSlider ("House length", advancedHouseGenerator.currentHouseConfig.houseLength, 1, 200);

		advancedHouseGenerator.currentHouseConfig.houseWidth = EditorGUILayout.IntSlider ("House width", advancedHouseGenerator.currentHouseConfig.houseWidth, 1, 200);

		EditorGUILayout.EndVertical ();
		#endregion //HomeSettings




		EditorGUILayout.LabelField (" ");

		EditorGUILayout.BeginVertical ("BOX");                                                         


		unittype = (UnitType)EditorGUILayout.EnumPopup ("Select UnitType", unittype);

		matColor = EditorGUILayout.ColorField("New Color", matColor);
		if(GUILayout.Button("Create House")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.CreateNewHouse ();
		}
		if(GUILayout.Button("Create Rooms")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.GenerateFloorPlan (advancedHouseGenerator.currentHouseData);
		}
		if(GUILayout.Button("Fix Rooms")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.FixRooms (advancedHouseGenerator.currentHouseData);
		}
		if(GUILayout.Button("Create Outer Walls")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.GenerateOuterWalls (advancedHouseGenerator.currentHouseData);
		}
		if(GUILayout.Button("Create Inner Doors")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.GenerateInnerDoors (advancedHouseGenerator.currentHouseData);
		}
		if(GUILayout.Button("Create Outer Holes")) {
			advancedHouseGenerator.currentHouseData = advancedHouseGenerator.GenerateWindowsAndOuterDoors (advancedHouseGenerator.currentHouseData);
		}
		if(GUILayout.Button("NEW")) {
			if (unittype == UnitType.INNER_WALLS) {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (null, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_UP, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_RIGHT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_DOWN, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_LEFT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_TR, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_TL, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_DL, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_DR, matColor);

			}else if (unittype == UnitType.OUTER_WALLS) {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (null, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_UP, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_RIGHT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_LEFT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_DOWN, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DL_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DR_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TL_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TR_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DL_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DR_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TL_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TR_O, matColor);
			} else {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (null, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, unittype, matColor);
			}
			advancedHouseGenerator.currentHouseData.previewTexture.Apply ();
			}

		if(GUILayout.Button("ADD")) {
			if (unittype == UnitType.INNER_WALLS) {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_UP, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_RIGHT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_DOWN, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_LEFT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_TR, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_TL, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_DL, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.INNERWALL_CORNER_DR, matColor);

			}else if (unittype == UnitType.OUTER_WALLS) {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_UP, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_RIGHT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_LEFT, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_DOWN, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DL_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DR_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TL_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TR_I, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DL_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_DR_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TL_O, matColor);
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, UnitType.OUTERWALL_CORNER_TR_O, matColor);
			} else {
				advancedHouseGenerator.currentHouseData.previewTexture = HouseVZ.Visualize (advancedHouseGenerator.currentHouseData.previewTexture, advancedHouseGenerator.currentHouseConfig, advancedHouseGenerator.currentHouseData.floorPlan, unittype, matColor);
			}
			advancedHouseGenerator.currentHouseData.previewTexture.Apply ();

		}

		EditorGUILayout.ObjectField ("MAP", advancedHouseGenerator.currentHouseData.previewTexture, typeof(Texture2D), true);
		EditorGUILayout.EndVertical ();


		if (EditorGUI.EndChangeCheck()) {
			SceneView.RepaintAll ();
		}





	}
		

	[MenuItem("GameObject/3D Object/House")]
	public static AdvancedHouseGenerator CreateHouse()
	{
		GameObject go = new GameObject("House");
		AdvancedHouseGenerator ahg = 
			go.AddComponent<AdvancedHouseGenerator>();

		return ahg;
	}


}
