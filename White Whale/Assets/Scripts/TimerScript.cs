using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

// Notifier
    // Raises OnRoundEnded when round ends
        // Observer: UIManager



public class Timer : MonoBehaviour
{

    // Observer Events
    public static event Action<float> OnTimerTick;
    public static event Action OnRoundEnded;
    // Timer Settings
    [SerializeField] private float roundDuration = 60f; // 1 minute
    [SerializeField] private TMP_Text timerText;        // UI for timer

    // End Round Scene
    [SerializeField] private GameObject roundOverScreen;   // Round Over Screen

    [SerializeField] private Button restartButton;         // Restart game
    [SerializeField] private Button homeBaseButton;         // Go to home base

    private float _currTime;
    private bool _isRoundActive;

    void Start()
    {
        _currTime = roundDuration;
        _isRoundActive = true;

        if (roundOverScreen != null)
        {
            roundOverScreen.SetActive(false);
        }
        // Restart Button go click
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartRound);
        }
        // Home Button go home
        if (homeBaseButton != null)
        {
            homeBaseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1f; // Ensure time resumes before scene loads
                SceneManager.LoadScene("Underwater Base");
            });
        }

    }
    void Update()
    {
        if (_isRoundActive)
        {
            _currTime -= Time.deltaTime;
            UpdateTimerDisplay();

            OnTimerTick?.Invoke(_currTime);     // Broadcast time left

            if (_currTime <= 0)
            {
                _currTime = 0;
                CursorManager cursorManager = FindObjectOfType<CursorManager>();
                if (cursorManager != null)
                {
                    cursorManager.ShowCursor();
                }
                EndRound();
            }
        }
        else
        {
            // Restarts Round on Space or Return Key press
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                RestartRound();
            }
        }        
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(_currTime).ToString("00");
        }
    }

    void EndRound()
    {
        _isRoundActive = false;
        roundOverScreen.SetActive(true);

        // Pause
        Time.timeScale = 0f;

        GameEvents.RoundEnded(); // Notify all subscribed observers

    }
    void RestartRound()
    {
        // Unpause 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


