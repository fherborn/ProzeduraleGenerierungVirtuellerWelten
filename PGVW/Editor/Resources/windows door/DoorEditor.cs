using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Door))]
public class DoorEditor : Editor {

    private Door door;

    public void Awake()
    {
        this.door = target as Door; ;
    }

    public override void OnInspectorGUI()
    {

        EditorGUI.BeginChangeCheck();
        door.height = EditorGUILayout.Slider(door.height, 2f, 10f);
        door.width = EditorGUILayout.Slider(door.width, 1f, 10f);
        door.depth = EditorGUILayout.Slider(door.depth, 0.5f, 10f);

        if (EditorGUI.EndChangeCheck())
            this.door.CreateMesh();

    }
}
