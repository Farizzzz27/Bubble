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
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (!isGrounded)
        {
            float newVerticalVelocity = body.linearVelocity.y + gravity * Time.deltaTime;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(newVerticalVelocity, maxFallSpeed));
        }
        
        if(Input.GetKey(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
            Debug.Log("Player menyentuh tanah");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log("Player terbang");
    }

    
}

