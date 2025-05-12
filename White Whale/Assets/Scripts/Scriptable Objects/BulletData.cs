using UnityEngine;

[CreateAssetMenu(fileName ="Bullet", menuName ="Weapon/Bullet")]
public class BulletData : ScriptableObject
{
    [Header("Information")]
    public new string name;
    public GameObject prefab;

    [Header("Shooting")]
    public int damage;
    public float projectileLifetime;
    public int ricochets;
    public bool explosions;
    public float explosionRadius;
}
