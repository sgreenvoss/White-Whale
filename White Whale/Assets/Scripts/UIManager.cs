using UnityEngine;
using TMPro;

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
        FishManager.OnFishCaught += HandleFishCaught;
    }

    void OnDisable()
    {
        Debug.Log("UIManager unsubscribed from events");
        PauseManager.OnPauseStateChanged -= HandlePauseStateChanged;
        Timer.OnRoundEnded -= HandleRoundEnded;
        CursorManager.OnCursorVisibilityChanged -= HandleCursorChange;
        FishManager.OnFishCaught -= HandleFishCaught;
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
    }

    void HandleCursorChange(bool isVisible)
    {
        Debug.Log($"Cursor is now {(isVisible ? "Visible" : "Hidden")}");
    }


    void HandleFishCaught(int count)
    {
        if (fishCountText != null)
            fishCountText.text = $"Fish Count: {count}";
    }


}


