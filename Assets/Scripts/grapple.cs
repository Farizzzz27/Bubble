using System.Collections;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject target; // The target object (e.g., a hook or a point to grapple to)
    public GameObject player; // The player object
    public float grappleSpeed = 5f; // Speed of movement towards the target
    private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D

    // Set gravity scale here or in the Inspector (if needed)
    public float normalGravity = 1f; // The normal gravity scale for the player
    public float grappleGravity = 0f; // The gravity scale while grappling

    private void Start()
    {
        // Get the Rigidbody2D component of the player
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object collided is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Wait for the player to press the "E" key
            if (Input.GetKey(KeyCode.E))
            {
                // Start the grappling logic
                StartCoroutine(Grappling());
            }
        }
    }

    private IEnumerator Grappling()
    {
        // Temporarily disable gravity during grappling
        playerRigidbody.gravityScale = grappleGravity;

        // Wait a moment before starting the movement
        yield return new WaitForSeconds(0.2f);

        // Move the player towards the target while it's farther than a certain distance (e.g., 0.1f)
        while (Vector2.Distance(player.transform.position, target.transform.position) > 0.1f)
        {
            // Move player closer to the target with the specified speed
            player.transform.position = Vector2.MoveTowards(
                player.transform.position, 
                target.transform.position, 
                grappleSpeed * Time.deltaTime
            );

            yield return null; // Wait for the next frame
        }

        // Once the player reaches the target, restore normal gravity
        playerRigidbody.gravityScale = normalGravity;

        // Optionally, you can add a debug log or other actions here
        Debug.Log("Grapple reached target and gravity restored!");
    }
}
