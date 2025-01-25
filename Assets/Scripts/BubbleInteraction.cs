using UnityEngine;

public class BubbleInteraction : MonoBehaviour
{
    public Animator playerAnimator; // Animator untuk pemain
    public string breakBubbleAnimation = "Cut"; // Nama trigger animasi
    public KeyCode interactionKey = KeyCode.K; // Tombol interaksi
    private bool isNearBubble = false; // Status pemain dekat dengan gelembung
    private GameObject currentBubble; // Gelembung yang sedang berinteraksi

    private void Update()
    {
        if (isNearBubble && Input.GetKeyDown(interactionKey))
        {
            InteractWithBubble();
        }
    }

    private void InteractWithBubble()
    {
        if (playerAnimator != null && currentBubble != null)
        {
            // Jalankan animasi memecahkan gelembung
            playerAnimator.SetTrigger(breakBubbleAnimation);

            // Opsional: Memastikan NPC dilepaskan
            ReleaseNPC(currentBubble);

            // Hancurkan gelembung setelah animasi
            Destroy(currentBubble);
        }
    }

    private void ReleaseNPC(GameObject bubble)
    {
        Transform npc = bubble.transform.Find("NPC");
        if (npc != null)
        {
            npc.SetParent(null); // Lepaskan NPC dari gelembung
            npc.gameObject.SetActive(true); // Aktifkan NPC jika sebelumnya tidak aktif
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            isNearBubble = true;
            currentBubble = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            isNearBubble = false;
            currentBubble = null;
        }
    }
}
