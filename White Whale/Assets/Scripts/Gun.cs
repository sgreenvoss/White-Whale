using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;

    // for particle instantiation:
    GameObject impact; 
    GameObject muzzleInst;

    int currentAmmo;
    bool reloading;
    float timeSinceLastShot;
    float recip;
    float div = 30f;

    public delegate void DecreaseAmmo();
    public delegate void Shoot();
    public DecreaseAmmo decreaseAmmo;
    public Shoot shoot;

    public static System.Action<int> OnAmmoChanged;

    private bool CanShoot() => !reloading && timeSinceLastShot > recip;

    private void Awake()
    {
        recip = 1f / (gunData.fireRate / div);
        //    GameState.GameStateChanged += HandleGameChange;

        PlayerShoot.reload += TryReload;

        if (gunData.auto)
        {
            PlayerShoot.shootHold += ShootAuto;
            impact = gunData.impact;
            muzzleInst = gunData.muzzleFlash;
        }
        else
        {
            Debug.Log("initializing as shootpistol");
            PlayerShoot.shootPress += ShootPistol;
        }
        currentAmmo = gunData.capacity;
        reloading = false;
    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward, Color.green);
    }

    private void OnDestroy()
    {
        if (gunData.auto)
        {
            PlayerShoot.shootHold -= ShootAuto;
        }
        else
        {
            PlayerShoot.shootPress -= ShootPistol;
        }
        PlayerShoot.reload -= TryReload;
    //    GameState.GameStateChanged -= HandleGameChange;
    }
    public void ShootAuto()
    {
        if (currentAmmo > 0 && CanShoot())
        {
            // plays particle system and starts coroutine to delete self in one second. 
            Instantiate(muzzleInst, muzzle.position, muzzle.rotation);
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                Instantiate(impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                if (hitInfo.transform.CompareTag("Fish"))
                {
                    ABSFish fish = hitInfo.transform.GetComponent<ABSFish>();

                    if (fish != null)
                    {
                        fish.Damage(gunData.damage);
                    }
                }
               
            }

            currentAmmo--;
            timeSinceLastShot = 0;

            if (currentAmmo == 0)
            {
                StartCoroutine(Reload());
            }

            OnAmmoChanged?.Invoke(currentAmmo);
        }
    }
    public void ShootPistol()
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

    private void TryReload()
    {
        // this is called when R is pressed (shooting input all handled in PlayerShoot)

        // only let player manual reload if they have shot at least once
        if (currentAmmo < gunData.capacity)
        {
            StartCoroutine(Reload());
            OnAmmoChanged?.Invoke(currentAmmo);
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
