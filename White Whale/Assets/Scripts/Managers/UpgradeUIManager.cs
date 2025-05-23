using Skills;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UpgradeUIManager : MonoBehaviour
{
    SkillTree tree = SkillTree.Instance;
    public Button speedOpt1;
    public Button speedOpt2;
    public Button speedOpt3;


    public Button oxygenOpt1;
    public Button oxygenOpt2;
    public Button oxygenOpt3;


    public Button weaponOpt1;
    public Button weaponOpt2;
    public Button weaponOpt3;

    private Dictionary<string, Button[]> upgradeButtons;

    

    void Start()
    {
        // Make cursor show up
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        tree = SkillTree.Instance;

        upgradeButtons = new Dictionary<string, Button[]>
        {
            { "Speed", new Button[] { speedOpt1, speedOpt2, speedOpt3 } },
            { "Oxygen", new Button[] { oxygenOpt1, oxygenOpt2, oxygenOpt3 } },
            { "Weapon", new Button[] { weaponOpt1, weaponOpt2, weaponOpt3 } },
        };

        // Listeners
        foreach (var category in upgradeButtons.Keys)
        {
            for (int i = 0; i < upgradeButtons[category].Length; i++)
            {
                int capturedIndex = i + 1;
                string upgradeID = category + capturedIndex;
                Button btn = upgradeButtons[category][i];

                bool isUnlocked = tree.IsUnlocked(upgradeID);
                bool isUnlockable = tree.Unlockable(upgradeID); 

                if (isUnlocked)
                {
                    bool isCurrentlySelected = tree.IsCurrentlySelected(category, upgradeID);
                    btn.interactable = true;
                    SetButtonColor(btn, isCurrentlySelected ? "purchased" : "unlocked");

                }
                else if (isUnlockable)
                {
                    btn.interactable = true;
                    SetButtonColor(btn, "available");
                }
                else
                {
                    btn.interactable = false;
                    SetButtonColor(btn, "locked");
                }

                btn.onClick.AddListener(() => OnUpgradeClicked(category, capturedIndex));

            }
        }

    }

    void OnUpgradeClicked(string category, int option)
    {
        Debug.Log($"Upgrade selected: {category} Option {option}");

        string upgradeID = category + option.ToString();

        if (!tree.Unlockable(upgradeID)) return;
        tree.Unlock(upgradeID);
        tree.SelectUpgrade(category, upgradeID);

        for (int i = 0; i < upgradeButtons[category].Length; i++)
        {
            int tier = i + 1;
            string id = category + tier;

            bool isSelected = tree.IsCurrentlySelected(category, id);
            bool isUnlocked = tree.IsUnlocked(id);

            Button btn = upgradeButtons[category][i];
            btn.interactable = (tier == option + 1) && tree.Unlockable(id);
            SetButtonColor(btn, isSelected ? "purchased" : isUnlocked ? "available" : "locked");

        }

    }


    void SetButtonColor(Button btn, string state)
    {
        ColorBlock colors = btn.colors;

        switch (state)
        {
            case "locked":
                colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 1f);
                colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
                break;
            case "available":
                colors.normalColor = new Color(1f, 1f, 1f, 1f);
                colors.highlightedColor = new Color(0.8f, 0.8f, 1f, 1f);
                break;
            case "purchased":
                colors.normalColor = new Color(0.3f, 0.9f, 0.3f, 1f);
                colors.highlightedColor = new Color(0.4f, 1f, 0.4f, 1f);
                break;
            case "unlocked":
                colors.normalColor = new Color(1f, 1f, 1f, 1f);
                colors.highlightedColor = new Color(0.8f, 1f, 0.8f, 1f);
                break;

        }

        colors.disabledColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        colors.pressedColor = colors.normalColor;
        colors.selectedColor = colors.normalColor;

        btn.colors = colors;
    }

    void RefreshButtonStates()
    {
        foreach (var category in upgradeButtons.Keys)
        {
            for (int i = 0; i < upgradeButtons[category].Length; i++)
            {
                string id = category + (i + 1).ToString();
                bool isUnlocked = tree.IsUnlocked(id);
                bool isUnlockable = tree.Unlockable(id);

                var btn = upgradeButtons[category][i];
                btn.interactable = isUnlockable && !isUnlocked;

                if (isUnlocked)
                    SetButtonColor(btn, "purchased");
                else if (isUnlockable)
                    SetButtonColor(btn, "available");
                else
                    SetButtonColor(btn, "locked");
            }
        }
    }
    
}
