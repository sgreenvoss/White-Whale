using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Skills;

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



    void Start()
    {
        _currTime = roundDuration;
    //    if (PlayerSkills.Instance != null)
        //   {
        //        _currTime = PlayerSkills.Instance.gameTime;
        //    }
        //    else
        //   {
        //        Debug.LogWarning("player skills is null!");
        //        _currTime = roundDuration;
        //    }

        if (GameState.CurrentState != GState.Diving)
            GameState.Instance.ChangeState(GState.Diving);

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
                GameState.Instance.ChangeState(GState.HomeBase);
                SceneManager.LoadScene("Underwater Base");
            });
        }

    }
    void Update()
    {
        if (GameState.CurrentState == GState.Diving)
        {
            _currTime -= Time.deltaTime;
            UpdateTimerDisplay();

            OnTimerTick?.Invoke(_currTime);     // Broadcast time left

            if (_currTime <= 0)
            {
                _currTime = 0;
                CursorManager cursorManager = FindFirstObjectByType<CursorManager>();

                if (cursorManager != null)
                {
                    cursorManager.ShowCursor();
                }
                EndRound();
            }
        }
        //else
        //{
            // Restarts Round on Space or Return Key press
            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            //{
            //    RestartRound();
            //}
        //}        
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
        GameState.Instance.ChangeState(GState.EndRound);

        // Pause
        Time.timeScale = 0f;

        // convert score to total coins
        ABSFish.total_coins += ABSFish.score * 10;

        GameEvents.RoundEnded(); // Notify all subscribed observers

    }
    void RestartRound()
    {
        // Unpause 
        Time.timeScale = 1f;
        GameState.Instance.ChangeState(GState.Diving);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


