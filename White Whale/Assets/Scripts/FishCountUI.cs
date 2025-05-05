using TMPro;
using UnityEngine;

public class FishCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text fishCountText;

    void OnEnable()
    {
        GameEvents.OnFishCaught += UpdateFishUI;
    }

    void OnDisable()
    {
        GameEvents.OnFishCaught -= UpdateFishUI;
    }

    void UpdateFishUI(int count)
    {
        if (fishCountText != null)
        {
            fishCountText.text = "Fish Count: " + count;
        }
    }
}

