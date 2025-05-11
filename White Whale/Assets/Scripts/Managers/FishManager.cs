using UnityEngine;
using System;

// Notifier
    // Raises onFishCaught when fish is caught
        // Observer: UIManager

public class FishManager : MonoBehaviour
{
    public static event Action<ABSFish> OnFishCaught; // Local event


    public void CatchFish(ABSFish fish)
    {
        // fishCaught++;
        OnFishCaught?.Invoke(fish); // Notify observers (UI in this case)
    }

    // public int GetFishCount()
    // {
    //     return fishCaught;
    // }


}
