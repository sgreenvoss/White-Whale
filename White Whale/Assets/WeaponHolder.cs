using Skills;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private GameObject currentGunInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (GameState.CurrentState)
        {
            case (GState.Diving):

                Debug.Log("diving");
                var gunData = PlayerSkills.Instance.currentGunData;
                if (gunData != null && gunData.gunPrefab != null)
                {
                    currentGunInstance = Instantiate(gunData.gunPrefab, transform.position, transform.rotation, parent:this.transform);
                }
                break;

            case (GState.HomeBase):
                Destroy(currentGunInstance);
                break;
        }
    }

}
