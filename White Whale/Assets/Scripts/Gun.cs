using NUnit.Framework;
using Skills;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Scripting;
using DistantLands;

public class Gun : MonoBehaviour
{
    [SerializeField] private BulletBarUI bulletBarUI;


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
    float _bulletSize;
    float autoBulletSize; // radius of spherecast - should be smaller
   // Vector3 muzzleOffset;

    public static System.Action<int> OnAmmoChanged;

    public int MaxAmmo => gunData.capacity;

    private bool CanShoot() => !reloading && timeSinceLastShot > recip;

    private void Awake()
    {
        recip = 1f / (gunData.fireRate / div);
        //    GameState.GameStateChanged += HandleGameChange;
        currentAmmo = gunData.capacity;
        _bulletSize = PlayerSkills.Instance.bulletScale;
        autoBulletSize /= 3f;
    //    muzzleOffset = Vector3.one;
    //    muzzleOffset.z += _bulletSize / 2f;

        reloading = false;

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
    }

    public void Initialize(BulletBarUI bulletBarUI)
    {
        this.bulletBarUI = bulletBarUI;
        this.bulletBarUI.SetTotalAmmo(gunData.capacity);
        this.bulletBarUI.UpdateBulletDisplay(currentAmmo);
    }

    private void Start()
    {
        // Set total ammo in UI at start
        if (bulletBarUI != null)
        {
            bulletBarUI.SetTotalAmmo(gunData.capacity);
            bulletBarUI.UpdateBulletDisplay(currentAmmo);
        }


        OnAmmoChanged += HandleAmmoChanged;
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

        OnAmmoChanged -= HandleAmmoChanged;
    }
    public void ShootAuto()
    {
        if (currentAmmo > 0 && CanShoot())
        {
            // plays particle system and starts coroutine to delete self in one second. 
            Instantiate(muzzleInst, muzzle.position, muzzle.rotation);
            if (Physics.SphereCast(muzzle.position, autoBulletSize, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
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

                if (hitInfo.transform.CompareTag("Shark"))
                {
                    Debug.Log("collision with shark detected");
                    if (WaypointSystem.attackPlayer == false)
                    {
                        WaypointSystem.attackPlayer = true;
                        Debug.Log("Shark is chasing you :0");
                    }
                    ABSFish shark = hitInfo.transform.GetComponent<ABSFish>();


                    if (shark != null)
                    {
                        Debug.Log("Shark hit!!");
                        shark.Damage(gunData.damage);
                    }
                    else
                    {
                        Debug.Log("Shark is Null");
                    }
                }
                if (hitInfo.transform.CompareTag("Whale"))
                {
                    Debug.Log("collision with WHale detected");

                    // if (WaypointSystem.attackPlayer == false)
                    // {
                    //     WaypointSystem.attackPlayer = true;
                    //     Debug.Log("Whale is chasing you :0");
                    // }
                    ABSFish Whale = hitInfo.transform.GetComponent<ABSFish>();


                    if (Whale != null)
                    {
                        Debug.Log("Whale hit!!");
                        Whale.Damage(gunData.damage);
                    }
                    else
                    {
                        Debug.Log("Whale is Null");
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
            Debug.Log("Muzzle rotation at shoot time: " + muzzle.rotation.eulerAngles);
            Debug.DrawRay(muzzle.position, muzzle.forward * 2, Color.red, 2f);
            GameObject projectile = Instantiate(gunData.projectile, muzzle.position, muzzle.rotation);
            Debug.Log("projectile rotation: " + projectile.transform.rotation.eulerAngles);
            // make the bullet the size as declared in the upgrade
            projectile.transform.localScale = Vector3.one * _bulletSize * 2f;
            // shoot projectile
            // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward.normalized * gunData.projectileVelocity, ForceMode.Impulse);
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

        // Trigger Reload text
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
            ui.ShowReloadText(gunData.reloadTime);



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

    // Bullet Bar UI
    private void HandleAmmoChanged(int currentAmmo)
    {
        if (bulletBarUI != null)
        {
            bulletBarUI.UpdateBulletDisplay(currentAmmo);
        }
    }

}
