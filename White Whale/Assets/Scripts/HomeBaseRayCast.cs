using UnityEngine;
using TMPro;

public class HomeBaseRayCast : MonoBehaviour
{
    public TMP_Text UpgradeText;
    public TMP_Text InfoText;
    public Camera PlayerCamera;

    public float maxInteractionDist = 10f;

    // public bool faceCamera;

    public Collider fridgeCollider; 
    public Collider OfficeCollider;
    // private bool isTextInitialized = false;



   
    void Start()
    {

        if (UpgradeText == null)
        {
            Debug.LogError("TMP_Text component not found on UpgradeText!");
        }
        else
        {
            Debug.Log("Upgrade Text component found");
        }
        if (InfoText == null)
        {
            Debug.LogError("TMP_Text component not found on InfoText!");
        }
        else
        {
            Debug.Log("Info Text component found");
        }


        UpgradeText.enabled = true;
        UpgradeText.text = "UPGRADES";
        InfoText.enabled = true;
        InfoText.text = "INFO";
        // isTextInitialized = true;
    }

    void Update()
    {
        
        Ray ray = PlayerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Debug.DrawRay(ray.origin, ray.direction * maxInteractionDist, Color.red);
        RaycastHit hitInfo;

        bool lookingAtFridge = false;
        bool lookingAtOffice = false;

             
        if (Physics.Raycast(ray, out hitInfo, maxInteractionDist))
        {
            // Debug.Log($"Hit: {hitInfo.collider.name}");

            if (hitInfo.collider == fridgeCollider)
            {
                Debug.Log("Fridge hit!");
                lookingAtFridge = true;
            }
            if (hitInfo.collider == OfficeCollider)
            {
                Debug.Log("Fridge hit!");
                lookingAtOffice = true;
            }
        }

        if(lookingAtFridge)
        {
            if (!UpgradeText.enabled)
            {
                UpgradeText.enabled = true;

                Debug.Log("You should see the text");
            }
        }
        else
        {
            if (UpgradeText.enabled)
            {
                UpgradeText.enabled = false;

            }
        }

        if(lookingAtOffice)
        {
            if (!InfoText.enabled)
            {
                InfoText.enabled = true;

                Debug.Log("You should see the info text");
            }
        }
        else
        {
            if (InfoText.enabled)
            {
                InfoText.enabled = false;

            }
        }

    }
}
