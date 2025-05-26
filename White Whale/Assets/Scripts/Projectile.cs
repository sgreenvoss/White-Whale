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

        //Enemy Collion
    if (collision.gameObject.CompareTag("Shark"))
    {
        bulletParticle.Play();
        Debug.Log("Shark got hit!!");
        

        SharkAI shark = collision.gameObject.GetComponent<SharkAI>();
        Debug.Log("Shark Speed"+ shark.speed);
        if (shark != null)
        {
  
            if (!shark.IsAggro)
            {
  
                shark.ChangeState(new SharkAttack(shark));
                Debug.Log("Shark should now be chansing you :O");
            }
            else
            {
                shark.Damage(bulletData.damage);
                Debug.Log("Shark took Damage!!");
            }
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
