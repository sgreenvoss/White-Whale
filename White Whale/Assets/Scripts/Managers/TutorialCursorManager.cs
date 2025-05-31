using UnityEngine;

public class TutorialCursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;                  // Make it visible
    }
}
