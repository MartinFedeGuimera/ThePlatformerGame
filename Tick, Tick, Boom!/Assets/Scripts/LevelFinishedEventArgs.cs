using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedEventArgs : EventArgs
{
    public Vector3 CameraPosition { get; }
    public float CameraSize { get; }
    public Vector3 TimeTextPosition { get; }

    public LevelFinishedEventArgs(Vector3 cameraPos, float cameraSize, Vector3 timeTextPosition)
    {
        CameraPosition = cameraPos;
        CameraSize = cameraSize;
        TimeTextPosition = timeTextPosition;
    }
}
