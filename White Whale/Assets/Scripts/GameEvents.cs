using UnityEngine;
using System;

// Event Hub
    // Centralized place for shared name events
        // Keeps code decoupled

public static class GameEvents
{
    public static event Action<int> OnFishCaught;
    public static event Action OnRoundEnded;
    public static event Action<bool> OnPauseStateChanged;
    public static event Action<bool> OnCursorVisibilityChanged;

    public static void FishCaught(int amount) => OnFishCaught?.Invoke(amount);
    public static void RoundEnded() => OnRoundEnded?.Invoke();
    public static void PauseStateChanged(bool isPaused) => OnPauseStateChanged?.Invoke(isPaused);
    public static void CursorVisibilityChanged(bool isVisible) => OnCursorVisibilityChanged?.Invoke(isVisible);
}
