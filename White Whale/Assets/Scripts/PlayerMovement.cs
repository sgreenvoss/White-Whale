using DistantLands;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMovement;
    float verticalMovement;
    float drag = 10f;
    float movementMultiplier = 10f;

    public float moveSpeed = 10f;
    PlayerFish harpoon;

    Vector3 moveDirection;

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode diveKey = KeyCode.LeftShift;
    [SerializeField] KeyCode fishKey = KeyCode.Mouse0;

    float jumpAmt = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        harpoon = GetComponent<PlayerFish>();
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
        if (Input.GetKeyDown(fishKey))
        {
            harpoon.Shoot();
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        
        if (col.CompareTag("Fish"))
        {
            col.GetComponent<ABSFish>().Catch();
        } 
    }
}
