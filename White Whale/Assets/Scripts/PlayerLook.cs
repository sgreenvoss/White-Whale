using UnityEngine;

public class playerLook : MonoBehaviour
{
    // camera sensitivity
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    Camera cam;
    Transform cameraTransform;

    float mouseX;
    float mouseY;
    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private Quaternion initialPlayerRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store the initial rotation to preserve starting orientation
        initialPlayerRotation = transform.localRotation;
    }

    private void Update()
    {
        MyInput();
        // cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.localRotation = initialPlayerRotation * rotation;

    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

    }
}