using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;
    LineRenderer lr;
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

    //private void DecreaseAmmoPistol()
    //{
    //    currentAmmo--;
    //}
    //private void DecreaseAmmoAuto()
    //{
    //    currentAmmo
    //}

    private bool CanShoot() => !reloading && timeSinceLastShot > recip;

    private void Awake()
    {
        recip = 1f / (gunData.fireRate / div);
        //    GameState.GameStateChanged += HandleGameChange;

        PlayerShoot.reload += TryReload;

        if (gunData.auto)
        {
            PlayerShoot.shootHold += ShootAuto;
            lr = GetComponent<LineRenderer>();
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
        Debug.Log("SHOOTING AUTO!");
        if (currentAmmo > 0 && CanShoot())
        {
            lr.enabled = true;
            Vector3 start = muzzle.position;
            Vector3 direction = muzzle.forward; // or firePoint.right for 2D
            Vector3 end = start + direction * gunData.maxDistance;

            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                Debug.Log("hit!");
                if (hitInfo.transform.CompareTag("Fish"))
                {
                    ABSFish fish = hitInfo.transform.GetComponent<ABSFish>();

                    if (fish != null)
                    {
                        fish.Damage(gunData.damage);
                    }
                }
                currentAmmo--;
                timeSinceLastShot = 0;

            }
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
