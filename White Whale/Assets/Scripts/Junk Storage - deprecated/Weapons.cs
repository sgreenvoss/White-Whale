//using UnityEngine;
//using System.Collections;
//using Unity.VisualScripting;

//namespace Weapons
//{
//	public abstract class Gun : MonoBehaviour
//	{
//        protected bool isActive = true;
//        protected GameObject weaponPrefab;
//        protected GameObject projectilePrefab;
//        protected Transform projectileSpawn;
//        protected float projectileVelocity;
//        protected int capacity;
//        protected int current;
//        protected float reloadTime;
//        protected int ricochets;
//        protected int damage;

//        float projectileLifeTime = 3f;

//        protected virtual void OnEnable()
//        {
//            GameState.GameStateChanged += HandleGameStateChange;
//            HandleGameStateChange(GameState.CurrentState);
//        }
//        protected virtual void OnDisable()
//        {
//            GameState.GameStateChanged -= HandleGameStateChange;
//        }
//        private void HandleGameStateChange(GState state)
//        {
//            isActive = (state == GState.Diving);
//            weaponPrefab.GetComponent<Renderer>().enabled = isActive;
//        }

//        public abstract void Shoot();
//        public abstract void Initialize();
//        protected void FireWeapon()
//        {
//            if (!isActive) return;
//            // instantiate projectile
//            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
//            Projectile proj = projectile.GetComponent<Projectile>();
//         //   proj.Initialize(dmg: damage, _ricochets: ricochets);
//            // shoot projectile
//            projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward.normalized * projectileVelocity, ForceMode.Impulse);
//            current--;
//            // destroy after some time
//            StartCoroutine(DestroyProjectileAfterTime(projectile, projectileLifeTime));
//            // and reload gun
//            StartCoroutine(Recharge(reloadTime));
//        }


//        private IEnumerator DestroyProjectileAfterTime(GameObject projectile, float delay)
//        {
//            yield return new WaitForSeconds(delay);
//            if (projectile)
//            {
//                Destroy(projectile);
//            }
//        }
//        private IEnumerator Recharge(float delay)
//        {
//            yield return new WaitForSeconds(delay);
//            current++;
//        }
//        protected void PlaySound(string sound)
//		{
//			Debug.Log("playing sound " + sound);
//		}
//		protected void MakeParticles()
//		{
//			Debug.Log("particles");
//		}
//	}

//    public class Pistol : Gun
//    {
//        [SerializeField] GameObject _weaponPrefab;
//        [SerializeField] GameObject prefab;
//        [SerializeField] Transform spawn;
//        public override void Initialize()
//        {
//            weaponPrefab = _weaponPrefab;
//            projectilePrefab = prefab;
//            projectileSpawn = spawn;
//            projectileVelocity = 30f;
//            capacity = 6;
//            current = capacity;
//            reloadTime = 1f;
//            ricochets = 0;

//        }
//        public override void Shoot()
//        {
//            FireWeapon();
//        }
//    } 

//    public class Shotgun : Gun
//    {
//        [SerializeField] GameObject _weaponPrefab;
//        [SerializeField] GameObject prefab;
//        [SerializeField] Transform spawn;
//        public override void Initialize()
//        {
//            weaponPrefab = _weaponPrefab;
//            projectilePrefab = prefab;
//            projectileSpawn = spawn;
//            projectileVelocity = 40f;
//            capacity = 12;
//            current = capacity;
//            reloadTime = 3f;
//            ricochets = 2;

//        }
//        public override void Shoot()
//        {
//            FireWeapon();
//        }
//    }
//}
