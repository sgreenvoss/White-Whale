using Skills;
using TMPro;
using UnityEngine;

public class dollarDisplay : MonoBehaviour
{
    public TMP_Text dollarDisp;

    void Start()
    {
        PlayerSkills.OnCoinChange += CoinHandler;

        if (PlayerSkills.Instance != null)
        {
            CoinHandler(PlayerSkills.Instance.coinCount);
        }
        else
        {
            Debug.LogWarning("PlayerSkills.Instance is null at Start in dollarDisplay.");
        }
    }

    private void OnDisable()
    {
        PlayerSkills.OnCoinChange -= CoinHandler;
    }

    void CoinHandler(int newVal)
    {
        if (dollarDisp != null)
        {
            dollarDisp.text = "$" + newVal.ToString();
        }
        else
        {
            Debug.LogError("dollarDisp is not assigned in dollarDisplay!");
        }
    }
}
