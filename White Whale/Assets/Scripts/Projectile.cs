using UnityEngine;
using System.Collections;
public class Projectile : MonoBehaviour
{
    private int damage;
    private int ricochets;
    private int ric_count;

  //  public void Initialize(int dmg, int _ricochets)
  //  {
  //      damage = dmg;
  //      ricochets = _ricochets;
  //  }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            ABSFish fish = collision.gameObject.GetComponent<ABSFish>();

            if (fish != null)
            {
                fish.Damage(damage);
            }
        }
 //       if (ric_count >= ricochets)
  //      {
   //         Destroy(gameObject);
    //    }
     //   ric_count++;
    }

}
