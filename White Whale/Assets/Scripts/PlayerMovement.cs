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
    [SerializeField] KeyCode diveKey = KeyCode.LeftShift;
    float jumpAmt = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        MyInput();

        if (Input.GetKey(jumpKey))
        {
            Jump();
        }
        if (Input.GetKey(diveKey))
        {
            Dive();
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
        ControlDrag();
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

    void Dive()
    {
        rb.AddForce(-transform.up * jumpAmt, ForceMode.Impulse);
    }
 

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }

}
