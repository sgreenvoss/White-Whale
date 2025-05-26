using Skills;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private BulletBarUI bulletBarUI;
    static GameObject currentGunInstance;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        GameState.GameStateChanged += HandleGameStateChanged;
    }
    private void OnDisable()
    {
        UnequipGun();
        GameState.GameStateChanged -= HandleGameStateChanged;
    }

    private void Start()
    {
        if (GameState.CurrentState == GState.Diving)
        {
            EquipCurrentGun();
        }
        else UnequipGun();
    }

    private void EquipCurrentGun()
    {
        UnequipGun();

        var gunData = PlayerSkills.Instance.currentGunData;
        Debug.Log("Loading this gun:" + gunData.name);
        if (gunData != null && gunData.gunPrefab != null)
        {
            for (int i = 0; i < PlayerSkills.Instance.hands; i++)
            {
                Debug.Log("equipping gun: " + gunData.name);
                currentGunInstance = Instantiate(gunData.gunPrefab, parent: this.transform);
                currentGunInstance.transform.localPosition += PlayerSkills.Instance.gunPositions[i];
            }
            // this only references the last gun created. but that's fine because each ammo should dec the same.
            Gun gunScript = currentGunInstance.GetComponent<Gun>();
            if (gunScript != null && bulletBarUI != null)
            {
                gunScript.Initialize(bulletBarUI);
            }
        }
        Debug.Log(currentGunInstance.name);
    }
    private void UnequipGun()
    {
        if (currentGunInstance != null) { 
            Destroy(currentGunInstance);
            currentGunInstance = null;
        }
    }
    void HandleGameStateChanged(GState state)
    {
        switch (state)
        {
            case (GState.Diving):

                EquipCurrentGun();

                break;

            case (GState.HomeBase):
                UnequipGun();
                break;
        }
    }

}
