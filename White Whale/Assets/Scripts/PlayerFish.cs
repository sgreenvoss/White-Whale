using UnityEngine;

public class PlayerFish : MonoBehaviour
{
    float radius = 1f;

    public void Shoot() {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.position, 20f);
        foreach (RaycastHit hit in hits)
        {
            Debug.Log("a hit");
            ABSFish fish = hit.collider.GetComponent<ABSFish>();
            if (fish != null)
            {
                fish.Catch();
            }
        }

    }
}
