using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Movement Variables
    public float moveForce = 365f;
    public float acceleration = 4f;
    public float slowRate = 2f;
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
        float playerSpeed = Mathf.Abs(playerRigidbody.velocity.x);

        if (horizonatalInput != 0)
        {
            //if (horizonatalInput * playerRigidbody.velocity.x < maxSpeed)
            //{
            //    playerRigidbody.AddForce(Vector2.right * horizonatalInput * moveForce);
            //}

            //if (Mathf.Abs(playerRigidbody.velocity.x) > maxSpeed)
            //{
            //    playerRigidbody.velocity = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * maxSpeed, playerRigidbody.velocity.y);
            //}

            if (Mathf.Abs(playerRigidbody.velocity.x + (acceleration * Time.fixedDeltaTime * horizonatalInput)) > maxSpeed)
            {
                playerRigidbody.velocity = new Vector2(maxSpeed * Mathf.Sign(playerRigidbody.velocity.x), playerRigidbody.velocity.y);
            }
            else
            {
                //playerRigidbody.velocity = new Vector2((playerRigidbody.velocity.x + (acceleration * Time.fixedDeltaTime * horizonatalInput)) * Mathf.Sign(playerRigidbody.velocity.x), playerRigidbody.velocity.y);
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x + (acceleration * Time.fixedDeltaTime * horizonatalInput), playerRigidbody.velocity.y);
            }
        }
        else
        {
            if (playerSpeed < (slowRate * Time.fixedDeltaTime))
            {
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            }
            else
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x - (slowRate * Time.fixedDeltaTime * Mathf.Sign(playerRigidbody.velocity.x)), playerRigidbody.velocity.y);
            }
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
