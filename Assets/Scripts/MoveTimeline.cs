using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimeSlice
{
    public GameObject SpawnPositionR;
    public Quaternion RotationR;
    public GameObject SpawnPositionL;
    public Quaternion RotationL;
}

[System.Serializable]
public struct Slices
{
    public TimeSlice[] TimeSlices;
}

public class MoveTimeline : MonoBehaviour
{
    public Slices[] Slices;
}
