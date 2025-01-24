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
    private float jumptime = 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (!isGrounded && !isJumping)
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            jumptime = 0f;
            body.linearVelocity = new Vector2(body.linearVelocity.x, baseJumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            jumptime += Time.deltaTime;
            if (jumptime < maxJumpTime)
            {
                float extraJumpForce = Mathf.Lerp(baseJumpForce, maxJumpForce, jumptime / maxJumpTime);
                body.linearVelocity = new Vector2(body.linearVelocity.x, extraJumpForce);
            }
            else
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, maxJumpForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || jumptime >= maxJumpTime)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
