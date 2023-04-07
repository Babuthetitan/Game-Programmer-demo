using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;
    public float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;

    public float groundDistance;

    Rigidbody2D rb;
    Animator anim;

    Vector2 inputVector;
    public bool isGrounded = false;
    public bool facingRight = true;

    private void Start()
    {
        movementSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

        if (inputVector.x != 0)
        {
            anim.SetTrigger("isRunning");

            if (inputVector.x > 0)
            {
                gameObject.transform.localScale = new Vector2(1, transform.localScale.y);
                facingRight = true;
            }

            if (inputVector.x < 0)
            {
                gameObject.transform.localScale = new Vector2(-1, transform.localScale.y);
                facingRight = false;
            }
        }

        else
        {
            anim.SetTrigger("isIdle");
        }
    }

    public void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            Jump();
            AkSoundEngine.PostEvent("player_jump", gameObject);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(Mathf.RoundToInt(inputVector.x) * movementSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnRunStart()
    {
        movementSpeed = runSpeed;
    }

    private void OnRunFinish()
    {
        movementSpeed = walkSpeed;
    }
}
