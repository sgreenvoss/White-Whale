using System.Collections.Generic;
using UnityEngine;
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

    public static GameState Instance { get; private set; }
    public static GState CurrentState { get; set; } = GState.Diving;
    private static GState _lastState = CurrentState;

    public delegate void OnGameStateChange(GState newState);
    public static event OnGameStateChange GameStateChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
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
    }

    public void ChangeState(GState state)
    {
        CurrentState = state;
        GameStateChanged?.Invoke(CurrentState);
    }
}
