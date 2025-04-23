using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMovement;
    float verticalMovement;
    float drag = 10f;
    float movementMultiplier = 10f;

    public float moveSpeed = 10f;
    Vector3 moveDirection;

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    float jumpAmt = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        MyInput();
        ControlDrag();
        if (Input.GetKey(jumpKey))
        {
            Jump();
        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
    void ControlDrag()
    {
        rb.linearDamping = drag;
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpAmt, ForceMode.Impulse);
    }
 

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }

}
