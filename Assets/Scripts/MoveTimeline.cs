using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimeSlice
{
    public GameObject SpawnPositionR;
    public Vector3 RotationR;
    public GameObject SpawnPositionL;
    public Vector3 RotationL;
}

[System.Serializable]
public struct Slices
{
    public string Name;
    public TimeSlice[] TimeSlices;
}

public class MoveTimeline : MonoBehaviour
{
    public Slices[] Slices;
}
