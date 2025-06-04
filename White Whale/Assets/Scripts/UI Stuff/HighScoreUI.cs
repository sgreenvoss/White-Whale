using Skills;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    private void Start()
    {
        if (GameState.Instance != null)
        {
            int score = GameState.Instance.highScore;
            TMP_Text text = GetComponent<TMP_Text>();
            text.text = "High Score: " + score.ToString();
        } 
    }

    

}

