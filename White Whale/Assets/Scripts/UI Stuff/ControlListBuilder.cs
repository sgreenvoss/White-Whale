using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlListBuilder : MonoBehaviour
{
    public GameObject controlLinePrefab;        // Prefab with 3 TMP_Text components: Key, Arrow, Action
    public Transform controlsListContainer;     // Vertical Layout Group container

    private Dictionary<string, string> controls = new Dictionary<string, string>()
    {
        {"N", "Flashlight"},
        {"P", "Pause"},
        {"Space", "Dash"},
        {"Shift", "Upward"},
        {"Left-click", "Shoot"},
        {"R", "Reload"},
        {"WSAD", "Swim Controls"}
    };

    void Start()
    {
        AddHeader(); // Add title row
        AddControlLines(); // Add all control lines
    }

    void AddHeader()
    {
        GameObject headerLine = Instantiate(controlLinePrefab, controlsListContainer);
        TMP_Text[] texts = headerLine.GetComponentsInChildren<TMP_Text>();

        if (texts.Length >= 3)
        {
            // Apply rich text styling and white color
            texts[0].text = "<b><i><color=Blue>Key</color></i></b>";
            texts[1].text = ""; // No arrow in header
            texts[2].text = "<b><i><color=Blue>Action</color></i></b>";

            // Optional: increase font size
            foreach (TMP_Text text in texts)
            {
                text.fontSize += 4;
                text.alignment = TextAlignmentOptions.Center;
            }
        }
    }


    void AddControlLines()
    {
        foreach (KeyValuePair<string, string> entry in controls)
        {
            GameObject controlLine = Instantiate(controlLinePrefab, controlsListContainer);
            TMP_Text[] texts = controlLine.GetComponentsInChildren<TMP_Text>();

            if (texts.Length >= 3)
            {
                texts[0].text = entry.Key;
                texts[1].text = "→"; // arrow
                texts[2].text = entry.Value;
            }
        }
    }
}
