using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;
    int currentAmmo;
    bool reloading;

    public static System.Action<int> OnAmmoChanged;

    private void Awake()
    {
    //    GameState.GameStateChanged += HandleGameChange;
        PlayerShoot.shootInput += Shoot;
        currentAmmo = gunData.capacity;
        reloading = false;
    }
    private void OnDestroy()
    {
        PlayerShoot.shootInput -= Shoot;
    //    GameState.GameStateChanged -= HandleGameChange;
    }

    public void Shoot()
    {
        Debug.Log(GameState.CurrentState);

        if (reloading) return;

        else if (GameState.CurrentState == GState.Diving)
        {
            GameObject projectile = Instantiate(gunData.projectile, muzzle.position, muzzle.rotation);
            // shoot projectile
            projectile.GetComponent<Rigidbody>().AddForce(muzzle.forward.normalized * gunData.projectileVelocity, ForceMode.Impulse);
            currentAmmo--;
            if (currentAmmo == 0)
            {
                StartCoroutine(Reload());
            }
            OnAmmoChanged?.Invoke(currentAmmo);
            // destroy after some time
            StartCoroutine(DestroyProjectileAfterTime(projectile, gunData.projectileLifetime));
        }
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading!");
        reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        currentAmmo = gunData.capacity;
        OnAmmoChanged?.Invoke(currentAmmo);
        reloading = false;
        Debug.Log("Done reloading");
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
