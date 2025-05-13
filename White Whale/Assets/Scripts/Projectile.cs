using UnityEngine;
using System.Collections;
using System;
public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletData bulletData;
    [SerializeField] private ParticleSystem bulletParticle;
    private void Awake()
    {
        Debug.Log("Bullet is here: " + transform.position.ToString());
    }
    int numRicochets = 0;
    private void OnCollisionEnter(Collision collision)
    {
        numRicochets++;
        if (collision.gameObject.CompareTag("Fish"))
        {
            ABSFish fish = collision.gameObject.GetComponent<ABSFish>();
            bulletParticle.Play();

            if (fish != null)
            {
                fish.Damage(bulletData.damage);
            }
        }
        if (numRicochets >= bulletData.ricochets)
        {
            if (bulletData.explosions)
            {
                Debug.Log("EXPLOSION!!!");
            }
            Destroy(gameObject);
        }
        numRicochets++;
    }

}
