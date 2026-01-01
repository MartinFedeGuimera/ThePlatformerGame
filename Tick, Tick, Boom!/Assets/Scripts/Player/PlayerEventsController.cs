using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventsController
{
    // -------- Movimiento --------
    public static event Action RunStarted;
    public static event Action RunStopped;

    // -------- Salto / Aire --------
    public static event Action JumpStarted;
    public static event Action FallStarted;
    public static event Action Landed;

    // -------- Pared --------
    public static event Action WallSlideStarted;
    public static event Action WallSlideEnded;
    public static event Action WallJumpStarted;

    // -------- Invokers --------
    public static void InvokeRunStarted() => RunStarted?.Invoke();
    public static void InvokeRunStopped() => RunStopped?.Invoke();

    public static void InvokeJumpStarted() => JumpStarted?.Invoke();
    public static void InvokeFallStarted() => FallStarted?.Invoke();
    public static void InvokeLanded() => Landed?.Invoke();

    public static void InvokeWallSlideStarted() => WallSlideStarted?.Invoke();
    public static void InvokeWallSlideEnded() => WallSlideEnded?.Invoke();
    public static void InvokeWallJumpStarted() => WallJumpStarted?.Invoke();
}
