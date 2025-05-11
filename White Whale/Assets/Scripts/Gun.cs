using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;

    private void Awake()
    {
    //    GameState.GameStateChanged += HandleGameChange;
        PlayerShoot.shootInput += Shoot;
    }
    private void OnDestroy()
    {
        PlayerShoot.shootInput -= Shoot;
    //    GameState.GameStateChanged -= HandleGameChange;
    }
    // this is not quite working - shoot seems to be added too many times.
    //private void HandleGameChange(GState gameState)
    //{
    //    switch (gameState)
    //    {
    //        case (GState.Diving):
    //            PlayerShoot.shootInput += Shoot;
    //            break;
    //        case (GState.Paused):
    //            PlayerShoot.shootInput -= Shoot;
    //            break;
    //    }
    // }
    public void Shoot()
    {
        Debug.Log(GameState.CurrentState);
        if (gunData.currentAmmo > 0 && GameState.CurrentState == GState.Diving)
        {
            GameObject projectile = Instantiate(gunData.projectile, muzzle.position, muzzle.rotation);
            // shoot projectile
            projectile.GetComponent<Rigidbody>().AddForce(muzzle.forward.normalized * gunData.projectileVelocity, ForceMode.Impulse);

            // destroy after some time
            StartCoroutine(DestroyProjectileAfterTime(projectile, gunData.projectileLifetime));
        }
    }
    public void DoNothing()
    {
        return;
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
