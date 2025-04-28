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

    Vector3 moveDirection;

    [SerializeField] KeyCode diveKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.Space;

    float jumpAmt = 5f;
    float dashAmt = 40f;
    int numDash = 0;
    int dashMax = 1;
    float dashTime = 3f;
    private float dashTimer = 0f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.freezeRotation = true;
    }

    private void Update()
    {
        dashTimer += Time.deltaTime;
        
        MyInput();

        if (Input.GetKey(diveKey))
        {
            Dive();
        }
        if (Input.GetKeyDown(dashKey))
        {
            Debug.Log("dash");
            Dash();
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

    void Dive()
    {
        rb.AddForce(-transform.up * jumpAmt, ForceMode.Impulse);
    }

    void Dash()
    {
        rb.AddForce(transform.forward * dashAmt, ForceMode.Impulse);
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
