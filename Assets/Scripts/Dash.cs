using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f; // Kecepatan dash
    [SerializeField] private float dashDuration = 0.2f; // Durasi dash
    [SerializeField] private float dashCooldown = 1f; // Waktu jeda antar dash

    private Rigidbody2D body;
    private Movement movementScript;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimeLeft;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        // Cek apakah pemain sedang bergerak berdasarkan kecepatan horizontal
        bool isMoving = Mathf.Abs(body.linearVelocity.x) > 0.01f;

        // Jika klik kanan ditekan, pemain bergerak, dan dash tersedia
        if (Input.GetKey(KeyCode.Q) && canDash && isMoving && !isDashing)
        {
            StartDash();
        }

        // Jika sedang dash
        if (isDashing)
        {
            DashMovement();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration;

        // Mematikan gravitasi sementara saat dash
        body.gravityScale = 0;
        movementScript.enabled = false; // Nonaktifkan script Movement sementara
        Invoke(nameof(EndDash), dashDuration);
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void DashMovement()
    {
        if (dashTimeLeft > 0)
        {
            // Tentukan arah dash berdasarkan kecepatan horizontal
            float dashDirection = Mathf.Sign(body.linearVelocity.x);
            body.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);
            dashTimeLeft -= Time.deltaTime;
        }
    }

    private void EndDash()
    {
        isDashing = false;
        body.gravityScale = movementScript.gravity / Physics2D.gravity.y; // Pulihkan gravitasi
        movementScript.enabled = true; // Aktifkan kembali script Movement
        body.linearVelocity = Vector2.zero; // Hentikan gerakan setelah dash
    }

    private void ResetDash()
    {
        canDash = true; // Reset kemampuan dash
    }
}
