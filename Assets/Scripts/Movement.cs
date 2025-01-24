using System.Collections;
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

    public float freezeDuration = 2f;
    public float cooldown = 5f;
    private float cooldownTimer = 0f;
    private bool isFreezing = false;

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

        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer <= 0)
        {
            StartCoroutine(FreezeTime());
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
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

    private IEnumerator FreezeTime()
    {
        if (isFreezing) yield break;
        isFreezing = true;
        Debug.Log("TIme has stoped");

        Time.timeScale = 0.1f;
        cooldownTimer = cooldown;

        yield return new WaitForSecondsRealtime(freezeDuration);

        Time.timeScale = 1f;
        isFreezing = false;
    }
}
