using UnityEngine;

public class DoubleJump : Movement
{
    private bool canDoubleJump = false;

    protected override void Update()
    {
        base.Update(); // Panggil logika movement dasar dari class Movement

        // Cek kondisi untuk melakukan double jump
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !isJumping && !canDoubleJump)
        {
            canDoubleJump = true;
            isJumping = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, baseJumpForce); // Lakukan double jump
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // Panggil logika dasar dari class Movement

        if (isGrounded)
        {
            canDoubleJump = false; // Reset kemampuan double jump saat menyentuh tanah
        }
    }
}
