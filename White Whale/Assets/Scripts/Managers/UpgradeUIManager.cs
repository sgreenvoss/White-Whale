using Skills;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        // Make cursor show up
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        speedOpt1.onClick.AddListener(() => OnUpgradeClicked("Speed", 1));
        speedOpt2.onClick.AddListener(() => OnUpgradeClicked("Speed", 2));
        speedOpt3.onClick.AddListener(() => OnUpgradeClicked("Speed", 3));

        oxygenOpt1.onClick.AddListener(() => OnUpgradeClicked("Oxygen", 1));
        oxygenOpt2.onClick.AddListener(() => OnUpgradeClicked("Oxygen", 2));
        oxygenOpt3.onClick.AddListener(() => OnUpgradeClicked("Oxygen", 3));

        weaponOpt1.onClick.AddListener(() => OnUpgradeClicked("Weapon", 1));
        weaponOpt2.onClick.AddListener(() => OnUpgradeClicked("Weapon", 2));
        weaponOpt3.onClick.AddListener(() => OnUpgradeClicked("Weapon", 3));


    }

    void OnUpgradeClicked(string category, int option)
    {
        Debug.Log($"Upgrade selected: {category} Option {option}");
        tree.Unlock(category + option.ToString());
    }


    
}
