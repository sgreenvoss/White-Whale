using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Collections;
using DistantLands;


// Observer
// Listens to:
// PauseManager.OnPauseStateChanged         pause UI
// Timer.OnRoundEnded                       round over screen
// FishManager.OnFishCaught                 Updates fish count UI
// CursorManager.OnCursorVisibilityChanged  reacts to cursor changes
//EnemyFish.

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text fishCountText;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject roundOverScreen;

    [SerializeField] private GameObject timerUI;

    [SerializeField] private BulletBarUI bulletBarUI;

    [SerializeField] private CanvasGroup reloadTextCanvasGroup;


    private Coroutine reloadFadeCoroutine;


    // Round Over children
    private GameObject RoundOverText;
    private GameObject HomeButton;
    private GameObject RestartButton;
    private GameObject RestartIndicator;

    public GameObject CoinUI;


    void Start()
    {
        // Restart Score
        if (fishCountText != null)
        {
            fishCountText.text = $"Score: 0";
            ABSFish.total_score = 0;
        }
    }



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
        GameState.Instance.setHighScore(ABSFish.total_score);

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
            // RoundOverText.GetComponent<TMP_Text>().text = EnemyFish.WhaleCaught ? "You Win!" : "Times up!";

            if (EnemyFish.WhaleCaught && RoundOverText != null)
            {
                RoundOverText.GetComponent<TMP_Text>().text = "You Win!";
            }
            
            if (EnemyFish.youDied && RoundOverText != null)
            {
                RoundOverText.GetComponent<TMP_Text>().text = "You Died!";
            }

            RoundOverText?.SetActive(true);

            HomeButton?.SetActive(false);
            RestartButton?.SetActive(false);
            CoinUI?.SetActive(false);

            StartCoroutine(ShowRoundOverUI());
        }


    }

    System.Collections.IEnumerator ShowRoundOverUI()
    {
        yield return new WaitForSecondsRealtime(1f);
        HomeButton?.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        RestartButton?.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        CoinUI?.SetActive(true);

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

    public void ShowReloadText(float reloadDuration)
    {
        if (reloadFadeCoroutine != null)
            StopCoroutine(reloadFadeCoroutine);

        reloadFadeCoroutine = StartCoroutine(FadeReloadText(reloadDuration));
    }

    private IEnumerator FadeReloadText(float reloadDuration)
    {
        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(reloadTextCanvasGroup, 0f, 1f, 0.3f));

        // Wait for reload to finish
        yield return new WaitForSeconds(reloadDuration - 0.6f);

        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(reloadTextCanvasGroup, 1f, 0f, 0.3f));


    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cg.alpha = end;
    }


}


