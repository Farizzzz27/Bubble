using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    public float gravity = -15f;
    public float maxFallSpeed = -20f;
    public float jumpForce = 10f;
    private bool isGrounded = false;

    private bool canDoubleJump = false; // Menentukan apakah pemain bisa melakukan double jump

    // Variabel tambahan untuk dash
    [SerializeField] private float dashSpeed = 15f; // Kecepatan saat dash
    [SerializeField] private float dashDuration = 0.2f; // Durasi dash dalam detik
    private bool isDashing = false; // Apakah pemain sedang dalam dash
    private float dashTime; // Timer untuk dash

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Jika tidak sedang dash, kontrol gerakan normal
        if (!isDashing)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }

        if (!isGrounded)
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                isGrounded = false;
                canDoubleJump = true; // Memberikan akses untuk double jump setelah lompatan pertama
            }
            else if (canDoubleJump) // Kondisi untuk melakukan double jump
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                canDoubleJump = false; // Mencegah double jump lebih dari sekali
            }
        }

        // Logika dash saat klik kanan
        if (Input.GetMouseButtonDown(1) && !isDashing) // Klik kanan untuk dash
        {
            StartDash(horizontalInput);
        }

        // Mengatur durasi dash
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                StopDash();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            canDoubleJump = false; // Reset double jump saat pemain menyentuh tanah
            Debug.Log("Player menyentuh tanah");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log("Player terbang");
    }

    // Metode untuk memulai dash
    private void StartDash(float horizontalInput)
    {
        isDashing = true;
        dashTime = dashDuration;

        // Kecepatan dash ke arah input horizontal (kiri/kanan)
        body.linearVelocity = new Vector2((horizontalInput != 0 ? horizontalInput : 1) * dashSpeed, body.linearVelocity.y);
        Debug.Log("Player mulai dash");
    }

    // Metode untuk menghentikan dash
    private void StopDash()
    {
        isDashing = false;
        Debug.Log("Player berhenti dash");
    }
}
