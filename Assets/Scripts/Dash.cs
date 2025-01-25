using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f; // Kecepatan dash
    [SerializeField] private float dashDuration = 0.2f; // Durasi dash
    [SerializeField] private float dashCooldown = 1f; // Waktu jeda antar dash
    [SerializeField] private float dashDistance = 5f; // Jarak dash maksimum
    [SerializeField] private float postDashStopTime = 0.5f; // Waktu berhenti setelah dash

    private Rigidbody2D body;
    private Movement movementScript;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimeLeft;
    private Vector2 dashStartPos; // Posisi awal saat mulai dash
    private bool isPostDashStopping = false; // Menandakan apakah sedang berhenti setelah dash

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
        if (Input.GetKey(KeyCode.Q) && canDash && isMoving && !isDashing && !isPostDashStopping)
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
        dashStartPos = body.position; // Simpan posisi awal saat mulai dash

        // Mematikan gravitasi sementara saat dash
        body.gravityScale = 0;
        movementScript.enabled = false; // Nonaktifkan script Movement sementara
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

            // Cek apakah dash sudah mencapai jarak tertentu
            float distanceTraveled = Vector2.Distance(dashStartPos, body.position);
            if (distanceTraveled >= dashDistance)
            {
                EndDash();
            }
        }
    }

    private void EndDash()
    {
        isDashing = false;
        body.gravityScale = movementScript.gravity / Physics2D.gravity.y; // Pulihkan gravitasi
        body.linearVelocity = Vector2.zero; // Hentikan gerakan setelah dash
        StartPostDashStop();
    }

    private void StartPostDashStop()
    {
        isPostDashStopping = true;
        movementScript.enabled = false; // Tetap nonaktifkan Movement sementara
        Invoke(nameof(EndPostDashStop), postDashStopTime);
    }

    private void EndPostDashStop()
    {
        isPostDashStopping = false;
        movementScript.enabled = true; // Aktifkan kembali Movement setelah berhenti sejenak
    }

    private void ResetDash()
    {
        canDash = true; // Reset kemampuan dash
    }
}