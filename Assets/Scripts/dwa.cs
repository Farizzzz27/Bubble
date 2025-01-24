using UnityEngine;

public class ResponsiveGravity : MonoBehaviour
{
    private Rigidbody2D body;

    public float gravity = -15f;       // Custom gravity force (negative for downward)
    public float maxFallSpeed = -20f; // Maximum falling speed
    public float jumpForce = 10f;     // Initial jump force
    public float moveSpeed = 5f;      // Horizontal movement speed

    private bool isGrounded = false;  // Check if character is on the ground

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0; // Disable default Unity gravity
    }

    private void Update()
    {
        // Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

        // Custom Gravity
        if (!isGrounded) // Apply gravity if not grounded
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            isGrounded = false; // Leave the ground
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Simple Ground Check
        if (collision.contacts[0].normal.y > 0.5f) // Checks if collision is from below
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Leaves ground
        isGrounded = false;
    }
}
