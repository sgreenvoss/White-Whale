using NUnit.Framework;
using Skills;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Scripting;
using DistantLands;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    [SerializeField] private BulletBarUI bulletBarUI;

    


    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;
    public List<AudioClip> sounds;
    // if i had more time i would move these to the scriptable objects
    // but alas and alack it is tuesday. so they are here now.
    public AudioClip shootSound;
    [SerializeField] public AudioClip reloadSound;
    AudioSource shootingSource;


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

    private float pitch = 1;

    private void Awake()
    {
        shootingSource = GetComponent<AudioSource>();
        shootSound = sounds[0];
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
            pitch = pitch * 1.005f;
            shootingSource.pitch = pitch;
            shootingSource.PlayOneShot(shootSound); // keeping the same one (since it plays a million times)
            
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
                        AudioManager.PitchShift(1.35f);
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

                    if (WaypointSystem.attackPlayer == false)
                    {
                        WaypointSystem.attackPlayer = true;
                        AudioManager.PitchShift(1.5f);
                        Debug.Log("Whale is chasing you :0");
                    }
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
                pitch = 1f;
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
            pitch = pitch * 1.1f;
            shootingSource.pitch = pitch;
            shootingSource.PlayOneShot(shootSound);
            GameObject projectile = Instantiate(gunData.projectile, muzzle.position, muzzle.rotation);
            // make the bullet the size as declared in the upgrade
            projectile.transform.localScale = Vector3.one * _bulletSize * 2f;
            // shoot projectile
            currentAmmo--;
            if (currentAmmo == 0)
            {
                StartCoroutine(Reload());
            }
            OnAmmoChanged?.Invoke(currentAmmo);
            // destroy after some time
            StartCoroutine(DestroyProjectileAfterTime(projectile, gunData.projectileLifetime));
            int r = Random.Range(0, sounds.Count);
            Debug.Log(r);
            shootSound = sounds[r];
            
            shootingSource.clip = shootSound;
            Debug.Log(shootingSource.clip.name);
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
        pitch = 1f;
        shootingSource.pitch = pitch;
        shootingSource.PlayOneShot(reloadSound);

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
