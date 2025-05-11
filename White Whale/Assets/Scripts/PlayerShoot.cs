using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;

    private void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Debug.Log("shooting");
            shootInput?.Invoke();
        }
    }
}
