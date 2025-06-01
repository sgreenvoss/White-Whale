using Skills;
using TMPro;
using UnityEngine;

public class dollarDisplay : MonoBehaviour
{
    int coins;
    public TMP_Text dollarDisp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerSkills.OnCoinChange += CoinHandler;
        coins = PlayerSkills.Instance.coinCount;
        dollarDisp.text = "$" + coins.ToString(); 
    }
    private void OnDisable()
    {
        PlayerSkills.OnCoinChange -= CoinHandler;
    }

    // Update is called once per frame
    void CoinHandler(int newVal)
    {
        dollarDisp.text = "$" + newVal.ToString();
    }
}
