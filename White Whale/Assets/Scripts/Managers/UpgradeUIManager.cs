
using Skills;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;




public class UpgradeUIManager : MonoBehaviour
{
    //[SerializeField] private BulletBarUI bulletBarUI;

    [SerializeField] private UpgradeTooltipUI tooltipUI;
    //[SerializeField] private TextMeshProUGUI tooltipText;
    private GameObject currentHoveredButton = null;
    private Coroutine hideTooltipCoroutine;

    private Dictionary<string, int> currentUpgradeCounts = new Dictionary<string, int>()
    {
        { "Speed1", 0 },
        { "Oxygen3", 0 }
    };
    private Dictionary<string, int> maxUpgradeCounts = new Dictionary<string, int>()
    {
        { "Speed1", 3 },
        { "Oxygen3", 3 }
    };


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

        // Pop Up Descriptions
        Dictionary<string, string> upgradeDescriptions = new Dictionary<string, string>
        {
            { "Speed1", "Increases Dash Speed x3" },
            { "Speed2", "Increases swimming Speed" },
            { "Speed3", "Longer Timer for Longer Rounds" },
            { "Weapon1", "A better version of starter Pistol" },
            { "Weapon2", "Long Pistol, Mag Size: 12" },
            { "Weapon3", "Assault Rifle, Mag Size: 30" },
            { "Oxygen1", "Illuminates the Twilight Zone" },
            { "Oxygen2", "Increases Bullet Size" },
            { "Oxygen3", "Increases Number of Hands x7" },
        };

        // Listeners
        foreach (var category in upgradeButtons.Keys)
        {
            for (int i = 0; i < upgradeButtons[category].Length; i++)
            {
                int capturedIndex = i + 1;
                string upgradeID = category + capturedIndex;
                string id = category + (i + 1).ToString();
                Button btn = upgradeButtons[category][i];

                bool isUnlocked = tree.IsUnlocked(upgradeID);
                bool isUnlockable = tree.Unlockable(upgradeID);

                if (isUnlocked)
                {
                    bool isSelected = tree.IsCurrentlySelected(category, id);
                    if (category == "Weapon" && !isSelected)
                    {
                        SetButtonColor(btn, "locked");
                    }
                    else
                    {
                        SetButtonColor(btn, "purchased");
                    }
                }
                else if (isUnlockable)
                    SetButtonColor(btn, "available");
                else
                    SetButtonColor(btn, "locked");
                

                btn.onClick.AddListener(() => OnUpgradeClicked(category, capturedIndex));

                if (upgradeDescriptions.TryGetValue(upgradeID, out string description))
                {
                    AddTooltipEvents(btn.gameObject, description);
                }
                else
                {
                    Debug.LogWarning($"No description found for {upgradeID}");
                }
            }
        }
    }

    void OnUpgradeClicked(string category, int option)
    {
        string upgradeID = category + option.ToString();

        // Special multi-press buttons
        if (maxUpgradeCounts.ContainsKey(upgradeID))
        {
            if (!currentUpgradeCounts.ContainsKey(upgradeID))
                currentUpgradeCounts[upgradeID] = 0;

            int currentCount = currentUpgradeCounts.ContainsKey(upgradeID) ? currentUpgradeCounts[upgradeID] : 0;
            int maxCount = maxUpgradeCounts[upgradeID];

            if (currentCount < maxCount)
            {
                currentCount++;
                currentUpgradeCounts[upgradeID] = currentCount;

                if (currentCount == 1)
                {
                    tree.Unlock(upgradeID);
                }
                Debug.Log($"{upgradeID} upgraded to level {currentCount}");

                // Optionally, select upgrade or update skill tree
                tree.SelectUpgrade(category, upgradeID);
            }
        }
        else if (category == "Weapon")
        {
            if (!tree.IsUnlocked(upgradeID))
            {
                tree.Unlock(upgradeID);
            }

            tree.SelectUpgrade(category, upgradeID);
        }
        else
        {
            // Existing single press logic
            if (!tree.Unlockable(upgradeID)) return;
            tree.Unlock(upgradeID);
            tree.SelectUpgrade(category, upgradeID);
        }

        RefreshButtonStates();
    }


    void SetButtonColor(Button btn, string state)
    {
        var colors = btn.colors;
        ColorBlock cb = colors;

        switch (state)
        {
            case "locked":
                btn.interactable = false;
                Debug.Log("locked");
                // Use disabledColor (automatically shown when not interactable)
                break;

            case "available":
                btn.interactable = true;
                Debug.Log("available");
                cb.normalColor = colors.normalColor;  // default normal
                break;

            case "unlocked":
                btn.interactable = true;
                Debug.Log("unlocked");
                cb.normalColor = colors.pressedColor;  // upgradable
                break;

            case "purchased":
                btn.interactable = true; // keep interactable if multi-press allowed
                Debug.Log("purchased");
                cb.normalColor = colors.selectedColor;
                btn.Select(); // visually mark as selected
                break;

            default:
                btn.interactable = true;
                cb.normalColor = colors.normalColor;
                break;
        }

        btn.colors = cb;

        // Deselect if not purchased
        if (state != "purchased")
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    void RefreshButtonStates()
    {
        foreach (var category in upgradeButtons.Keys)
        {
            for (int i = 0; i < upgradeButtons[category].Length; i++)
            {
                string id = category + (i + 1).ToString();
                var btn = upgradeButtons[category][i];

                bool isWeapon = category == "Weapon";

                // Check for multi-press capacity
                if (!isWeapon && maxUpgradeCounts.ContainsKey(id))
                {
                    int currentCount = currentUpgradeCounts.ContainsKey(id) ? currentUpgradeCounts[id] : 0;
                    int maxCount = maxUpgradeCounts[id];
                    bool WeapPurchase = false;

                    // Allow pressing
                    if (currentCount >= maxCount)
                    {
                        btn.interactable = false;
                        SetButtonColor(btn, "purchased");
                    }
                    else if (tree.IsUnlocked(id))
                    {
                        btn.interactable = true;
                        SetButtonColor(btn, "unlocked");
                    }
                    else if (tree.Unlockable(id))
                    {
                        btn.interactable = true;
                        SetButtonColor(btn, "available");
                    }
                    else
                    {
                        btn.interactable = false;
                        SetButtonColor(btn, "locked");
                    }
                }
                else
                {
                    // Normal logic for buttons without multi-press
                    bool isUnlocked = tree.IsUnlocked(id);
                    bool isUnlockable = tree.Unlockable(id);

                    btn.interactable = isUnlockable && !isUnlocked;

                    if (isUnlocked)
                    {
                        bool isSelected = tree.IsCurrentlySelected(category, id);
                        if (category == "Weapon" && !isSelected && !isUnlockable)
                        {
                            SetButtonColor(btn, "locked");
                        }
                        else
                        {
                            SetButtonColor(btn, "purchased");
                        }
                    }
                    else if (isUnlockable)
                        SetButtonColor(btn, "available");
                    else
                        SetButtonColor(btn, "locked");
                }
            }
        }
    }


    void AddTooltipEvents(GameObject obj, string description)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = obj.AddComponent<EventTrigger>();

        trigger.triggers.Clear();

        // Pointer Enter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) =>
        {
            // Old code
            //Vector2 pos = Input.mousePosition;
            //tooltipUI.Show(description, pos);


            // Cancel hide coroutine if running
            if (hideTooltipCoroutine != null)
            {
                StopCoroutine(hideTooltipCoroutine);
                hideTooltipCoroutine = null;
            }

            // Only update tooltip if new button hovered
            if (currentHoveredButton != obj)
            {
                currentHoveredButton = obj;
                RectTransform buttonRect = obj.GetComponent<RectTransform>();
                tooltipUI.Show(description, buttonRect);
            }
        });
        trigger.triggers.Add(entryEnter);

        // Pointer Exit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) =>
        {
            // Start delayed hide coroutine
            if (hideTooltipCoroutine != null)
                StopCoroutine(hideTooltipCoroutine);

            hideTooltipCoroutine = StartCoroutine(HideTooltipAfterDelay(0.1f));
        });
        trigger.triggers.Add(entryExit);
    }
    


    private IEnumerator HideTooltipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if pointer is still over a button - if not, hide tooltip
        if (currentHoveredButton != null)
        {
            // Check if pointer is over currentHoveredButton
            if (!IsPointerOverGameObject(currentHoveredButton))
            {
                tooltipUI.Hide();
                currentHoveredButton = null;
            }
        }
        else
        {
            tooltipUI.Hide();
        }
    }

    // Helper function
    private bool IsPointerOverGameObject(GameObject obj)
    {
        // Check if pointer is currently over the GameObject obj
        // Use UnityEngine.EventSystems.PointerEventData if needed, or use RaycastAll

        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (var result in results)
        {
            if (result.gameObject == obj)
                return true;
        }

        return false;
    }

    
}
