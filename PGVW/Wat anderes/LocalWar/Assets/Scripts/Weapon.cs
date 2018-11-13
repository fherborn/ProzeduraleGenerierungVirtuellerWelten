using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform handlePosition;
    public float rotationOffsetY = 90f;
    public float rotationOffsetX = 0f;
    public float rotationOffsetZ = 20f;

    public void UpdatePosition(Vector3 handle,Vector3 helper)
    {
        transform.position = new Vector3(handle.x, handle.y, handle.z);
        transform.position = transform.position + (transform.position - handlePosition.position);
        transform.LookAt(helper);
        transform.Rotate(new Vector3(rotationOffsetX, rotationOffsetY, rotationOffsetZ));
    }
}
