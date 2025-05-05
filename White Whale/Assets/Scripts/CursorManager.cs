using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Cursor Settings

    // Make cursor invisible during gameplay
    [SerializeField] private bool _isCursorVisible = false; 
    [SerializeField] private bool lockCursor = true;    // Lock cursor in place
    
    void Start()
    {
        UpdateCursorState();
    }


    void UpdateCursorState()
    {
        Cursor.visible = _isCursorVisible;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;

    }

    public void ShowCursor()
    {
        _isCursorVisible = true;
        lockCursor = false;
        UpdateCursorState();
    }

    public void HideCursor()
    {
        _isCursorVisible = false;
        lockCursor = true;
        UpdateCursorState();
    }



}
