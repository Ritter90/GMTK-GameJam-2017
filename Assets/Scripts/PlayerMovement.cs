using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    //Player Movement Variables
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public bool jump;
    public float directionFlipThreshold = 0.01f;

    private Transform groundCheck;
    public bool grounded = false;

    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float horizonatalInput = Input.GetAxis("Horizontal");

        MovePlayer(horizonatalInput);

        EvaluatePlayerDirection(horizonatalInput);

        EvaluateJump();
    }

    private void MovePlayer(float horizonatalInput)
    {
        if (horizonatalInput * playerRigidbody.velocity.x < maxSpeed)
        {
            playerRigidbody.AddForce(Vector2.right * horizonatalInput * moveForce);
        }

        if (Mathf.Abs(playerRigidbody.velocity.x) > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * maxSpeed, playerRigidbody.velocity.y);
        }
    }

    private void EvaluatePlayerDirection(float horizonatalInput)
    {
        if(Mathf.Abs(playerRigidbody.velocity.x) > directionFlipThreshold)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(playerRigidbody.velocity.x);
            transform.localScale = scale;
        }
    }

    private void EvaluateJump()
    {
        if (jump)
        {
            playerRigidbody.AddForce(Vector2.up * jumpForce);

            jump = false;
        }
    }
}
