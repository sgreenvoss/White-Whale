using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootPress;
    public static Action shootHold;
    public static Action reload;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Debug.Log("shooting");
            shootPress?.Invoke();
        }
        if (Input.GetKey(shootKey))
        {
            shootHold?.Invoke();
        }
        if (Input.GetKeyDown(reloadKey))
        {
            reload?.Invoke();
        }
    }
}
