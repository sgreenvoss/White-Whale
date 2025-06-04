using UnityEngine;
using TMPro;

public class TubeTextRefresher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;

    private readonly string[] phrases = new string[]
    {
        "Watch out, the Whale is deadly",
        "Some Fish have more health than others",
        "You might want a Flashlight in the Twilight Zone",
        "Buy Upgrades in the Fridge!",
        "PETA APPROVED",
        "Press Shift to dash upwards",
        "View other Controls at your computer",
        "Legend has it, the Whale may have a crush!",
        "Play again for a higher score",
        "Did you know you can have up to 8 hands?"
    };

    private int lastIndex = -1;
    private float timer = 0f;
    private float switchInterval = 5f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            timer = 0f;
            ShowRandomText();
        }
    }

    private void ShowRandomText()
    {
        int newIndex;

        do
        {
            newIndex = Random.Range(0, phrases.Length);
        } while (newIndex == lastIndex && phrases.Length > 1);

        lastIndex = newIndex;
        textDisplay.text = phrases[newIndex];
    }
}
