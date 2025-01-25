using UnityEngine;

public class InteractToDestroy : MonoBehaviour
{
    private bool isPlayerInRange = false;  // Menyimpan status apakah pemain berada dalam jangkauan collider
    public Animator animator;              // Reference ke Animator untuk animasi interaksi
    public string interactAnimationTrigger = "Interact"; // Nama trigger untuk animasi interaksi

    void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa jika yang masuk collider adalah pemain
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Menandai pemain keluar dari collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        // Memeriksa jika pemain menekan tombol 'J' dan berada dalam jangkauan collider
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.J))
        {
            // Memicu animasi interaksi
            if (animator != null)
            {
                animator.SetTrigger(interactAnimationTrigger);
            }

            // Menghancurkan objek setelah animasi selesai (tunggu beberapa detik jika animasi perlu waktu)
            Destroy(gameObject, 1f);  // Waktu tunggu sesuai durasi animasi
        }
    }
}
