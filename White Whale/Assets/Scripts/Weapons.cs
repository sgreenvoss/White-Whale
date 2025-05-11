using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;

namespace Weapons
{
	public abstract class Gun : MonoBehaviour
	{
        protected GameObject projectilePrefab;
        protected Transform projectileSpawn;
        protected float projectileVelocity;
        protected int capacity;
        protected int current;
        protected float reloadTime;
        protected int ricochets;
        protected int damage;

        float projectileLifeTime = 3f;

        public abstract void Shoot();
        protected abstract void Initialize();
        protected void FireWeapon()
        {
            // instantiate projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            // shoot projectile
            projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward.normalized * projectileVelocity, ForceMode.Impulse);

            // destroy after some time
            StartCoroutine(DestroyProjectileAfterTime(projectile, projectileLifeTime));
            // and reload gun
            StartCoroutine(Recharge(reloadTime));
        }


        private IEnumerator DestroyProjectileAfterTime(GameObject projectile, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(projectile);
        }
        private IEnumerator Recharge(float delay)
        {
            yield return new WaitForSeconds(delay);
            current++;
        }
        protected void PlaySound(string sound)
		{
			Debug.Log("playing sound " + sound);
		}
		protected void MakeParticles()
		{
			Debug.Log("particles");
		}
	}

    public class Pistol : Gun
    {
        [SerializeField] GameObject prefab;
        [SerializeField] Transform spawn;
        protected override void Initialize()
        {
            projectilePrefab = prefab;
            projectileSpawn = spawn;
            projectileVelocity = 30f;
            capacity = 6;
            current = capacity;
            reloadTime = 1f;
            ricochets = 0;

        }
        public override void Shoot()
        {
            FireWeapon();
        }
    } 
}
