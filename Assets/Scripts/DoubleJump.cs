using UnityEngine;

public class DoubleJump : Movement
{
    private bool canDoubleJump = false;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !isJumping && !canDoubleJump)
        {
            canDoubleJump = true;
            isJumping = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, baseJumpForce);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (isGrounded)
        {
            canDoubleJump = false;
        }
    }
}
