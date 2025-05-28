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

    [SerializeField] private GameObject timerUI;

    [SerializeField] private BulletBarUI bulletBarUI;


    // Round Over children
    private GameObject RoundOverText;
    private GameObject HomeButton;
    private GameObject RestartButton;
    private GameObject RestartIndicator;



    void OnEnable()
    {
        Debug.Log("UIManager subscribed to events");
        PauseManager.OnPauseStateChanged += HandlePauseStateChanged;
        GameEvents.OnRoundEnded += HandleRoundEnded;
        CursorManager.OnCursorVisibilityChanged += HandleCursorChange;
        FishManager.OnFishCaught += HandleFishScore;
        Gun.OnAmmoChanged += UpdateAmmoUI;
    }

    void OnDisable()
    {
        Debug.Log("UIManager unsubscribed from events");
        PauseManager.OnPauseStateChanged -= HandlePauseStateChanged;
        GameEvents.OnRoundEnded -= HandleRoundEnded;
        CursorManager.OnCursorVisibilityChanged -= HandleCursorChange;
        FishManager.OnFishCaught -= HandleFishScore;
        Gun.OnAmmoChanged -= UpdateAmmoUI;
    }


    void HandlePauseStateChanged(bool isPaused)
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(isPaused);
    }

    void HandleRoundEnded()
    {

        if (timerUI != null)
            timerUI.SetActive(false);       // disables timer

            
        // Show round over UI
        if (roundOverScreen != null)
        {
            roundOverScreen.SetActive(true);

            // get child references
            RoundOverText = roundOverScreen.transform.Find("RoundOverText")?.gameObject;
            HomeButton = roundOverScreen.transform.Find("HomeButton")?.gameObject;
            RestartButton = roundOverScreen.transform.Find("RestartButton")?.gameObject;

            RoundOverText?.SetActive(true);

            HomeButton?.SetActive(false);
            RestartButton?.SetActive(false);

            StartCoroutine(ShowRoundOverUI());
        }


    }

    System.Collections.IEnumerator ShowRoundOverUI()
    {
        yield return new WaitForSecondsRealtime(1f);
        HomeButton?.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        RestartButton?.SetActive(true);

    }

    void HandleCursorChange(bool isVisible)
    {
        Debug.Log($"Cursor is now {(isVisible ? "Visible" : "Hidden")}");
    }


    public void HandleFishScore(ABSFish fish)
    {
        if (fishCountText != null)
            fishCountText.text = $"Score: {ABSFish.total_score}";
    }

    void UpdateAmmoUI(int currentAmmo)
    {
        Debug.Log($"UIManager received ammo update: {currentAmmo}");

        bulletBarUI?.UpdateBulletDisplay(currentAmmo);
    }


}


