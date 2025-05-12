using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Information")]
    public new string name;
    public GameObject gunPrefab;

    [Header("Shooting")]
    public float damage;
    public GameObject projectile;
    public int projectileVelocity;
    public float projectileLifetime;
    public int ricochets;

    [Header("Reloading")]
    public int capacity;
    public float fireRate;
    public float reloadTime;
}
