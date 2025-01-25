using UnityEngine;

public class PlatformerRaycast2D : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float groundRayLength = 0.2f; // Panjang ray untuk mendeteksi tanah
    public float wallRayLength = 0.5f;   // Panjang ray untuk mendeteksi dinding
    public LayerMask groundLayer;        // Layer untuk mendeteksi tanah atau dinding

    [Header("Ray Origin Offsets")]
    public Vector2 groundRayOffset = Vector2.zero;  // Posisi awal ray untuk tanah
    public Vector2 wallRayOffset = Vector2.zero;    // Posisi awal ray untuk dinding

    [Header("State")]
    public bool isGrounded;
    public bool isTouchingWall;

    void Update()
    {
        CheckGround();
        CheckWall();
    }

    // Mengecek apakah menyentuh tanah
    void CheckGround()
    {
        Vector2 rayOrigin = (Vector2)transform.position + groundRayOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundRayLength, groundLayer);
        isGrounded = hit.collider != null;

        // Visualisasi dengan DrawRay
        Debug.DrawRay(rayOrigin, Vector2.down * groundRayLength, isGrounded ? Color.green : Color.red);

        // Debug.Log
        if (isGrounded)
        {
            Debug.Log("Karakter menyentuh tanah: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Karakter tidak menyentuh tanah.");
        }
    }

    // Mengecek apakah menyentuh dinding
    void CheckWall()
    {
        Vector2 rayOrigin = (Vector2)transform.position + wallRayOffset;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, wallRayLength, groundLayer);
        isTouchingWall = hit.collider != null;

        // Visualisasi dengan DrawRay
        Debug.DrawRay(rayOrigin, direction * wallRayLength, isTouchingWall ? Color.green : Color.red);

        // Debug.Log
        if (isTouchingWall)
        {
            Debug.Log("Karakter menyentuh dinding: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Karakter tidak menyentuh dinding.");
        }
    }

    private void OnDrawGizmos()
    {
        // Visualisasi untuk Ground Ray
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector2 groundRayOrigin = (Vector2)transform.position + groundRayOffset;
        Gizmos.DrawLine(groundRayOrigin, groundRayOrigin + Vector2.down * groundRayLength);

        // Visualisasi untuk Wall Ray
        Gizmos.color = isTouchingWall ? Color.green : Color.blue;
        Vector2 wallRayOrigin = (Vector2)transform.position + wallRayOffset;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(wallRayOrigin, wallRayOrigin + direction * wallRayLength);
    }
}
