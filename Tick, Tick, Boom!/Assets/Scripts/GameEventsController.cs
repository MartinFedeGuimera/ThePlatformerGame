using System;
using UnityEngine;

public static class GameEventsController
{
    public static event Action PlayerDied;
    public static event Action<float> TimeChanged;
    public static event Action TimeFinished;
    public static event Action<Vector3, float, Vector3> RoomFinished;
    public static event Action LevelFinished;

    public static void InvokePlayerDied() => PlayerDied?.Invoke();
    public static void InvokeTimeChanged(float time) => TimeChanged?.Invoke(time);
    public static void InvokeTimeFinished() => TimeFinished?.Invoke();
    public static void InvokeRoomFinished(Vector3 newCameraPosition, float newCameraSize, Vector3 newTimeTextPos) => RoomFinished?.Invoke(newCameraPosition, newCameraSize, newTimeTextPos);
    public static void InvokeLevelFinished() => LevelFinished?.Invoke();
}
