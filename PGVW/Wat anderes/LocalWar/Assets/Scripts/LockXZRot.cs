using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockXZRot : MonoBehaviour
{

    public bool lockX = true;
    public bool lockY = false;
    public bool lockZ = true;

    public float lockAngleX = 0f;
    public float lockAngleY = 0f;
    public float lockAngleZ = 0f;

    void Update () {
        transform.rotation = Quaternion.Euler(
            lockX ? lockAngleX : transform.rotation.eulerAngles.x,
            lockY ? lockAngleY : transform.rotation.eulerAngles.y,
            lockZ ? lockAngleZ : transform.rotation.eulerAngles.z);
    }
}
