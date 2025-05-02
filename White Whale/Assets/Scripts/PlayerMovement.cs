using DistantLands;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Skills;

public class PlayerMovement : MonoBehaviour
{
    PlayerSkills skills = new PlayerSkills();
    SkillTree _tree;

    float horizontalMovement;
    float verticalMovement;
    float drag = 10f;
    float movementMultiplier = 10f;

    Vector3 moveDirection;

    [SerializeField] KeyCode diveKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.Space;

    float jumpAmt = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _tree = new SkillTree(skills);
        // rb.freezeRotation = true;
    }

    private void Update()
    {
        
        MyInput();

        if (Input.GetKey(diveKey))
        {
            Dive();
        }
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _tree.Unlock("NewHand");
            Debug.Log("hand");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _tree.Unlock("dashMult");
            Debug.Log("dash");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            _tree.Unlock("speedMult");
            Debug.Log("speed");
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
        rb.AddForce(Vector3.down * jumpAmt, ForceMode.Impulse);
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
            col.GetComponent<ABSFish>().Catch();
        } 
    }
}
