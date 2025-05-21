using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Information")]
    public new string name;
    public GameObject gunPrefab;
    public bool auto;

    [Header("Shooting")]
    public int damage;
    public GameObject projectile;
    public int projectileVelocity;
    public float projectileLifetime;
    public int ricochets;

    [Header("Reloading")]
    public int capacity;
    public float reloadTime;
    public float maxDistance;

    [Header("Only for auto")]
    public float fireRate;
}
