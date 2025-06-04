using UnityEngine;
using System.Collections;
using System;

using DistantLands;
// using UnityEditor.ShaderGraph.Internal;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletData bulletData;
    [SerializeField] private ParticleSystem bulletParticle;
    Vector3 goAhead;
    private void Start()
    {
        goAhead = transform.forward * 1f;
    }
    private void FixedUpdate()
    {
        transform.position += goAhead;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        
        ParticleSystem colParticle = Instantiate(bulletParticle, collision.gameObject.transform.position, Quaternion.LookRotation(collision.gameObject.transform.position));
        colParticle.Play();
        
        if (collision.gameObject.CompareTag("Fish"))
        {
            ABSFish fish = collision.gameObject.GetComponent<ABSFish>();
            // bulletParticle.Play();

            if (fish != null)
            {
                fish.Damage(bulletData.damage);
            }
        }

        if (collision.gameObject.CompareTag("Shark"))
        {
            Debug.Log("collision with shark detected");
            if (WaypointSystem.attackPlayer == false)
            {   
                WaypointSystem.attackPlayer = true;
                AudioManager.HandleAttack();
                Debug.Log("Shark is chasing you :0");
            }
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

        if (collision.gameObject.CompareTag("Whale"))
        {
            Debug.Log("collision with whale detected");

            if (WaypointSystem.attackPlayer == false)
            {   
                WaypointSystem.attackPlayer = true;
                AudioManager.HandleAttack();
                Debug.Log("Whale is chasing you :0");
            }
            // if (WaypointSystem.attackPlayer == false)
            // {   
            //     Debug.Log("Turnign on attack Player");
            //     WaypointSystem.attackPlayer = true;
            //     Debug.Log("Whale is chasing you :0");
            // }

            ABSFish Whale = collision.gameObject.GetComponent<ABSFish>();
            bulletParticle.Play();
            Debug.Log("Whale Hit!!");
            Whale.Damage(bulletData.damage);
        }


        Destroy(gameObject);
        
    }

}
