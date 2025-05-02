using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Jobs;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponParent;
    List<Vector3> positions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        positions = new List<Vector3>();
        for (int i = 0; i < 7; i++)
        {
            positions.Add(new Vector3(0.51f - (0.13f * (i + 1)), 0.24f, 0.57f));
        }
        GameObject weapon = Instantiate(weaponPrefab, weaponParent);
        weapon.transform.localPosition = new Vector3(0.51f, 0.24f, 0.57f);

    }
    public void GrantWeapon()
    {
        int randIndex = Random.Range(0, positions.Count);
        GameObject weapon = Instantiate(weaponPrefab, weaponParent);
        weapon.transform.localPosition = positions[randIndex];
        positions.RemoveAt(randIndex);
    }
}
