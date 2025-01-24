using UnityEngine;

public class VariableJump : MonoBehaviour
{
    private Rigidbody2D body;

    public float moveSpeed = 5f;         // Kecepatan gerak horizontal
    public float jumpForce = 10f;       // Gaya awal lompatan
    public float maxJumpTime = 1f;      // Durasi maksimum pemain bisa menekan tombol lompat
    public float gravity = -15f;        // Gaya gravitasi kustom
    public float maxFallSpeed = -20f;   // Kecepatan jatuh maksimum

    private bool isGrounded = false;    // Apakah karakter sedang menyentuh tanah
    private bool isJumping = false;     // Apakah karakter sedang melompat
    private float jumpTime = 0f;        // Waktu yang telah digunakan untuk melompat

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0; // Nonaktifkan gravitasi bawaan Unity
    }

    private void Update()
    {
        // Gerakan horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

        // Custom gravity ketika di udara
        if (!isGrounded && !isJumping)
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }

        // Mulai lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            jumpTime = 0f; // Reset timer lompatan
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }

        // Lanjutkan lompat selama tombol Space ditekan
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            jumpTime += Time.deltaTime;

            // Tambahkan gaya ke atas selama durasi tombol ditekan
            if (jumpTime < maxJumpTime)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            }
        }

        // Hentikan lompat jika Space dilepas atau mencapai durasi maksimum
        if (Input.GetKeyUp(KeyCode.Space) || jumpTime >= maxJumpTime)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah menyentuh tanah
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;  // Karakter menyentuh tanah
            isJumping = false;  // Reset status lompatan
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Tidak lagi menyentuh tanah
        isGrounded = false;
    }
}
