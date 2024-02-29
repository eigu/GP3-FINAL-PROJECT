using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplier = 2f; //edit as needed
    public Rigidbody2D rigidBody;
    private Vector2 movementInput;
    public Animator anim;

    public int HP;
    public int SP;
    // method/s regarding attack on basis of sp will follow

    private bool isAttacking = false;

    private Vector2 lastMovementDirection = Vector2.zero; // for storing last direction faced when walking stops

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        anim.SetFloat("Horizontal", movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.sqrMagnitude);

        if (Keyboard.current.pKey.isPressed)
        {

            isAttacking = true;
            anim.SetBool("IsAttacking", isAttacking);
        }
  

        if (movementInput.magnitude > 0.1f)
        {
            lastMovementDirection = movementInput.normalized; 
        }
        else 
        {
            anim.SetFloat("Horizontal", lastMovementDirection.x);
            anim.SetFloat("Vertical", lastMovementDirection.y);
        }

    }

    private void FixedUpdate()
    {
        float currentMoveSpeed = moveSpeed;
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentMoveSpeed *= sprintMultiplier;
        }

        if (movementInput == Vector2.zero)
        {
            rigidBody.velocity = Vector2.zero;
        }
        else
        {
            rigidBody.velocity = movementInput.normalized * currentMoveSpeed;
        }

        isAttacking = false;
        anim.SetBool("IsAttacking", isAttacking);
    }


    private void OnMove(InputValue inputValue)
    {
        
        movementInput = inputValue.Get<Vector2>();
    }

}
