using UnityEngine;
using System;

// Notifier
    // Raises onFishCaught when fish is caught
        // Observer: UIManager

public class FishManager : MonoBehaviour
{
    public static event Action<int> OnFishCaught; // Local event

    private int fishCaught = 0;

    public void CatchFish()
    {
        fishCaught++;
        OnFishCaught?.Invoke(fishCaught); // Notify observers (UI in this case)
    }

    public int GetFishCount()
    {
        return fishCaught;
    }


}
