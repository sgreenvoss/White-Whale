using UnityEngine;

public class Projectile : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            print("Pish");
            // destroy game object?
        }
    }
}
