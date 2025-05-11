using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileVelocity = 3f;
    public float projectileLifeTime = 10f;
    [SerializeField] KeyCode shoot = KeyCode.Mouse0;

    private void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        // instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        // shoot projectile
        projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward.normalized * projectileVelocity, ForceMode.Impulse);

        // destroy after some time
        StartCoroutine(DestroyProjectileAfterTime(projectile, projectileLifeTime));
    }

    private IEnumerator DestroyProjectileAfterTime(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (projectile)
        {
            Destroy(projectile);
        }
    }
}
