using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // destroy game object?
            collision.gameObject.GetComponent<ABSFish>().Catch();
        }
    }

}
