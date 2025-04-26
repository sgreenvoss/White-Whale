using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    // Timer Settings
    [SerializeField] private float roundDuration = 60f; // 1 minute
    [SerializeField] private TMP_Text timerText;        // UI for timer

    // End Round Scene
    [SerializeField] private GameObject roundOverScreen;   // Round Over Screen

    [SerializeField] private Button restartButton;         // Restart game

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

    }

    void Update()
    {
        if (_isRoundActive)
        {
            _currTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (_currTime <= 0)
            {
                _currTime = 0;
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


        // Pause Button ?
        Time.timeScale = 0f;
    }

    void RestartRound()
    {
        // Unpause 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
