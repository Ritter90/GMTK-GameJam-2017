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
    private Player playerInfo;

    public Animator animator;

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(InputManager.GetJumpInput(playerInfo.character) && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float horizonatalInput = InputManager.GetHorizontalInput(playerInfo.character);

        MovePlayer(horizonatalInput);
        
        EvaluatePlayerDirection(horizonatalInput);

        EvaluateJump();
    }

    private void MovePlayer(float horizonatalInput)
    {
        float playerSpeed = Mathf.Abs(playerRigidbody.velocity.x);
        
        if (horizonatalInput != 0)
        {
            if (Mathf.Abs(playerRigidbody.velocity.x + (acceleration * Time.fixedDeltaTime * horizonatalInput)) > maxSpeed)
            {
                playerRigidbody.velocity = new Vector2(maxSpeed * Mathf.Sign(playerRigidbody.velocity.x), playerRigidbody.velocity.y);
            }
            else
            {
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
        animator.SetFloat("velocity", Mathf.Abs(playerRigidbody.velocity.x));
    }

    private void EvaluatePlayerDirection(float horizonatalInput)
    {
        if(Mathf.Abs(playerRigidbody.velocity.x) > directionFlipThreshold)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(playerRigidbody.velocity.x) * Mathf.Abs(scale.x);
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
