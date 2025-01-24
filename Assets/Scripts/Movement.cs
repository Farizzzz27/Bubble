using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody2D body; // Diubah ke protected

    public float speed = 5f;
    public float gravity = -15f;
    public float maxFallSpeed = -20f;
    public float baseJumpForce = 5f;
    public float maxJumpForce = 8f;
    public float maxJumpTime = 0.5f;

    protected bool isGrounded = false;
    protected bool isJumping = false;
    protected float jumptime = 0f;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    protected virtual void Update()
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
