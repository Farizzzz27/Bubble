using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;

    public float speed = 5f;
    public float gravity = -15f;
    public float maxFallSpeed = -20f;
    public float baseJumpForce = 5f;
    public float maxJumpForce = 8f;
    public float maxJumpTime = 0.5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private float jumpTime = 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    private void Update()
    {
        // Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Apply Gravity
        if (!isGrounded && !isJumping)
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                StartJump();
            }
            else if (!canDoubleJump && !isGrounded)
            {
                PerformDoubleJump();
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            ContinueJump();
        }

        if (Input.GetKeyUp(KeyCode.Space) || jumpTime >= maxJumpTime)
        {
            isJumping = false;
        }
    }

    private void StartJump()
    {
        isJumping = true;
        isGrounded = false;
        jumpTime = 0f;
        body.linearVelocity = new Vector2(body.linearVelocity.x, baseJumpForce);
    }

    private void ContinueJump()
    {
        jumpTime += Time.deltaTime;
        if (jumpTime < maxJumpTime)
        {
            float extraJumpForce = Mathf.Lerp(baseJumpForce, maxJumpForce, jumpTime / maxJumpTime);
            body.linearVelocity = new Vector2(body.linearVelocity.x, extraJumpForce);
        }
        else
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, maxJumpForce);
        }
    }

    private void PerformDoubleJump()
    {
        canDoubleJump = true;
        isJumping = true;
        body.linearVelocity = new Vector2(body.linearVelocity.x, baseJumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            isJumping = false;
            canDoubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
