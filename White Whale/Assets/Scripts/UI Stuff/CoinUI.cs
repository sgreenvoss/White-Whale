using Skills;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    int coins;
    TMP_Text coinText;

    private void Awake()
    {
        coins = ABSFish.total_coins;
        ABSFish.total_coins = 0;
        coinText = GetComponent<TMP_Text>();
        coinText.text = "Sand Dollars: " + coins.ToString();
        PlayerSkills.Instance.coinCount += coins;
    }

}

