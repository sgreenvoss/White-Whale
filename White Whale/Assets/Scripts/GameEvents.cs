using UnityEngine;
using System;

public static class GameEvents
{
    public static Action<int> OnFishCaught;
    public static Action<int> OnCurrencyUpdated;
    public static Action OnRoundEnded;
}
