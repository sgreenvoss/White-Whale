using UnityEngine;

public class FishManager : MonoBehaviour
{
    private int fishCaught = 0;

    public void CatchFish()
    {
        fishCaught++;
        GameEvents.OnFishCaught?.Invoke(fishCaught);
    }

    public int GetFishCount()
    
    {
        return fishCaught;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CatchFish(); // Simulate catching a fish
        }
    }

}
