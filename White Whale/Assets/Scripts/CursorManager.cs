using UnityEngine;
using System;

// Notifier
    // Raises OnCursorVisibilityChanged when cursor is shown / hidden
        // Observer: UIManager

public class CursorManager : MonoBehaviour
{

    // Notifies when cursor visibility changes
    public static event Action<bool> OnCursorVisibilityChanged;

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

        // Notifies Observers
        OnCursorVisibilityChanged?.Invoke(_isCursorVisible);



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
