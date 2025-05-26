using UnityEngine;
using System.Collections;
using System;
public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletData bulletData;
    [SerializeField] private ParticleSystem bulletParticle;

    int numRicochets = 0;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
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

        if (collision.gameObject.CompareTag("Shark"))
        {
            Debug.Log("collision with shark detected");
            ABSFish shark = collision.gameObject.GetComponent<ABSFish>();
            bulletParticle.Play();

            if (shark != null)
            {
                Debug.Log("Shark hit!!");
                shark.Damage(bulletData.damage);
            }
            else
            {
                Debug.Log("Shark is Null");
            }
        }


        if (numRicochets >= bulletData.ricochets)
        {
           // if (bulletData.explosions)
           // {
           //     Debug.Log("EXPLOSION!!!");
           // }
            Destroy(gameObject);
        }
        numRicochets++;
    }

}
