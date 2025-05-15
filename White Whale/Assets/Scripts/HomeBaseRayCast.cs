using UnityEngine;
using TMPro;

public class HomeBaseRayCast : MonoBehaviour
{
    public TMP_Text UpgradeText;
    public Camera PlayerCamera;

    public float maxInteractionDist = 10f;

    // public bool faceCamera;

    public Collider fridgeCollider; 
    private bool isTextInitialized = false;



   
    void Start()
    {

        if (UpgradeText == null)
        {
            Debug.LogError("TMP_Text component not found on UpgradeText!");
        }
        else
        {
            Debug.Log("Text component found");
        }

        UpgradeText.enabled = true;
        UpgradeText.text = "UPGRADES";

        isTextInitialized = true;
    }

    void Update()
    {
        
        Ray ray = PlayerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Debug.DrawRay(ray.origin, ray.direction * maxInteractionDist, Color.red);
        RaycastHit hitInfo;

        bool lookingAtFridge = false;

             
        if (Physics.Raycast(ray, out hitInfo, maxInteractionDist))
        {
            // Debug.Log($"Hit: {hitInfo.collider.name}");

            if (hitInfo.collider == fridgeCollider)
            {
                Debug.Log("Fridge hit!");
                lookingAtFridge = true;
            }
        }

        if(lookingAtFridge)
        {
            if (!UpgradeText.enabled)
            {
                UpgradeText.enabled = true;
                // Debug.Log("Looking at Fridge: UpgradeText Visible");
                Debug.Log("You should see the text");
            }
        }
        else
        {
            if (UpgradeText.enabled)
            {
                UpgradeText.enabled = false;
                // Debug.Log("Not Looking at Fridge: UpgradeText Hidden");
            }
        }

    }
}
