using DistantLands;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Skills;
using System.Collections;

public class Player : MonoBehaviour
{
    PlayerSkills skills;
    [SerializeField] GameObject FlashlightPrefab;

    float horizontalMovement;
    float verticalMovement;
    float drag = 10f;
    float movementMultiplier = 10f;
    float _dashamt;
    float _velocity;

    float jumpAmt;

    Vector3 moveDirection;

    [SerializeField] KeyCode jumpKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.Space;

    Rigidbody rb;
    Light flashlight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        skills = PlayerSkills.Instance;
        Debug.Log("GAME STATE: " + GameState.CurrentState);
        if (GameState.CurrentState == GState.HomeBase) {
            // set speed in home base to regular values
            _velocity = skills.baseVelocity;
            _dashamt = 0f;
            jumpAmt = 0f;
        } 
        else
        {
            _velocity = skills.velocity;
            _dashamt = skills.dashAmt;
            jumpAmt = 5f;
        }
    }

    private void Update()
    {
        
        MyInput();

        if (Input.GetKey(jumpKey))
        {
            Jump();
        }

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

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpAmt, ForceMode.Impulse);
    }

    void Dash()
    {
        rb.AddForce(transform.forward * _dashamt, ForceMode.Impulse);
    }
 

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * _velocity * movementMultiplier, ForceMode.Acceleration);
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
