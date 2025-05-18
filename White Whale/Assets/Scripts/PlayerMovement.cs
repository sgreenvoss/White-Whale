using DistantLands;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Skills;

public class Player : MonoBehaviour
{
    PlayerSkills skills;

    float horizontalMovement;
    float verticalMovement;
    float drag = 10f;
    float movementMultiplier = 10f;

    Vector3 moveDirection;

    [SerializeField] KeyCode diveKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.Space;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        skills = PlayerSkills.Instance;
    }

    private void Update()
    {
        
        MyInput();

        if (Input.GetKeyDown(dashKey))
        {
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

    void Dash()
    {
        rb.AddForce(transform.forward * skills.dashAmt, ForceMode.Impulse);
    }
 

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * skills.velocity * movementMultiplier, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        
        if (col.CompareTag("Fish"))
        {
            // changed to only damage the fish on collision.
            col.GetComponent<ABSFish>().Damage(1);
        } 
    }
}
