using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.XR;

// Observer
// Listens to:
// PauseManager.OnPauseStateChanged         pause UI
// Timer.OnRoundEnded                       round over screen
// FishManager.OnFishCaught                 Updates fish count UI
// CursorManager.OnCursorVisibilityChanged  reacts to cursor changes

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text fishCountText;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject roundOverScreen;


    void OnEnable()
    {
        Debug.Log("UIManager subscribed to events");
        PauseManager.OnPauseStateChanged += HandlePauseStateChanged;
        Timer.OnRoundEnded += HandleRoundEnded;
        CursorManager.OnCursorVisibilityChanged += HandleCursorChange;
        FishManager.OnFishCaught += HandleFishScore;
    }

    void OnDisable()
    {
        Debug.Log("UIManager unsubscribed from events");
        PauseManager.OnPauseStateChanged -= HandlePauseStateChanged;
        Timer.OnRoundEnded -= HandleRoundEnded;
        CursorManager.OnCursorVisibilityChanged -= HandleCursorChange;
        FishManager.OnFishCaught -= HandleFishScore;
    }


    void HandlePauseStateChanged(bool isPaused)
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(isPaused);
    }

    void HandleRoundEnded()
    {
        // Show round over UI
        if (roundOverScreen != null)
            roundOverScreen.SetActive(true);
        // limit access? 
    }

    void HandleCursorChange(bool isVisible)
    {
        Debug.Log($"Cursor is now {(isVisible ? "Visible" : "Hidden")}");
    }


    void HandleFishScore(ABSFish fish)
    {
        if (fishCountText != null)
            fishCountText.text = $"Score: {ABSFish.total_score}";
    }


}


