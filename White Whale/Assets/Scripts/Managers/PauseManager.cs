using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// Notifier
    // Raises OnPauseStateChanged when the game is paused / resumed
        // Observer: UIManager

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private Button homeButton;

    private bool isPaused = false;

    public static event Action<bool> OnPauseStateChanged;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (homeButton != null)
            homeButton.onClick.AddListener(GoHome);
    }

    void Update()
    {

        if (Time.timeScale == 0f && !isPaused) return;

        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        pauseMenuUI?.SetActive(true);
        cursorManager.ShowCursor();

        OnPauseStateChanged?.Invoke(isPaused);


    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseMenuUI?.SetActive(false);
        cursorManager?.HideCursor();

        OnPauseStateChanged?.Invoke(isPaused);

    }
    private void GoHome()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Underwater Base"); 
    }

    public bool IsPaused()
    {
        return isPaused;
    }

}
