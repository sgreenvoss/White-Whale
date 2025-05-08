using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int damage_dealt = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            ABSFish fish = collision.gameObject.GetComponent<ABSFish>();

            if (fish != null)
            {
                fish.Damage(damage_dealt);
            }
        }
    }

}
