using Skills;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    GameObject currentGunInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        GameState.GameStateChanged += HandleGameStateChanged;
    }
    private void OnDisable()
    {
        GameState.GameStateChanged -= HandleGameStateChanged;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (GameState.CurrentState == GState.Diving)
        {
            EquipCurrentGun();
        }
    }

    private void EquipCurrentGun()
    {
        UnequipGun();

        var gunData = PlayerSkills.Instance.currentGunData;
        Debug.Log("Loading this gun:" + gunData.name);
        if (gunData != null && gunData.gunPrefab != null)
        {
            Debug.Log("equipping gun: " + gunData.name);
            currentGunInstance = Instantiate(gunData.gunPrefab, transform.position, transform.rotation, parent: this.transform);
            Debug.Log("Here, gundata is " + gunData.name);
        }
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
