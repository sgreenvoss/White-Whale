using System.Collections.Generic;
using UnityEngine;
using Skills;
//using DistanceLands;

public enum GState
{
    Diving,
    Paused,
    HomeBase,
    Upgrading,
    EndRound
}
public class GameState : MonoBehaviour
{
    private Dictionary<string, GState> SceneLookup = new Dictionary<string, GState>
    {
        ["Sunlight Zone"] = GState.Diving,
        ["Underwater Base"] = GState.HomeBase,
        ["UpgradeScene"] = GState.Upgrading,
        ["Twilight Zone"] = GState.Diving
    };

    public int highScore = 0;

    public AudioManager AudioManager;

    public void setHighScore(int score)
    {
        Debug.Log("current high: " + highScore.ToString());
        Debug.Log("considering high: " + score.ToString());
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public static GameState Instance { get; private set; }
    public static GState CurrentState { get; set; } = GState.Diving;
    private static GState _lastState = CurrentState;

    public delegate void OnGameStateChange(GState newState);
    public static event OnGameStateChange GameStateChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Debug.Log($"This is me {name}");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        PauseManager.OnPauseStateChanged += HandlePauseStateChanged;
    }
    private void OnDisable()
    {
        PauseManager.OnPauseStateChanged -= HandlePauseStateChanged;
    }

    private void HandlePauseStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            // store the state it was in before pausing
            _lastState = CurrentState;
            CurrentState = GState.Paused;
        } else
        {
            // restore last state
            CurrentState = _lastState;
        }
        GameStateChanged?.Invoke(CurrentState);
    }

    public void SceneChange(string sceneName)
    {
        Debug.Log("changing to " + sceneName);
        GState newState = SceneLookup[sceneName];
        CurrentState = newState;
        GameStateChanged?.Invoke(newState);

        // Update Audio Manager
        // AudioManager.currState = sceneName;
    }

    public void ChangeState(GState state)
    {
        CurrentState = state;
        GameStateChanged?.Invoke(CurrentState);
    }

    public void ResetState()
    {
        Debug.Log("Resetting GameState");

        ABSFish.total_score = 0;
        ABSFish.total_coins = 0;
        ABSFish.score = 0;

        CurrentState = GState.Diving;
        _lastState = GState.Diving;

        DistantLands.EnemyFish.WhaleCaught = false;

        DistantLands.EnemyFish.youDied = false;

        if (SkillTree.Instance != null)
        {
            SkillTree.Instance.ResetSkills();
        }


        // Optionally reset other shared or static states
    }



}
