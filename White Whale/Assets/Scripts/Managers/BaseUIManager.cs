using UnityEngine;
using TMPro;

public class BaseUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    void Start()
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + savedHighScore.ToString();
    }
}
