using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TinyPlanet))]
public class TinyPlanetEditor : Editor {

    TinyPlanet planet;
	
	void Awake () {
        planet = target as TinyPlanet;
	}

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        planet.radius = EditorGUILayout.Slider(
            "Radius", planet.radius, 1f, 10f);

        planet.rotationAngles =
            EditorGUILayout.Vector3Field("Rotation",
            planet.rotationAngles);

        planet.pointsLongitude = EditorGUILayout.IntSlider(
            "points Longitude", planet.pointsLongitude,
            2, 360);

        planet.pointsLatitude = EditorGUILayout.IntSlider(
            "points Latitude", planet.pointsLatitude,
            2, 180);


        if (EditorGUI.EndChangeCheck())
            planet.CreateMesh();
    }
	
}
