using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneScript : MonoBehaviour
{
    // Fungsi untuk memulai permainan
    public void PlayGame()
    {
        // Pindah ke scene cutscene (ganti dengan nama scene cutscene kamu)
        SceneManager.LoadScene("PantaiCutscene");
    }

    // Fungsi untuk keluar dari game
    public void ExitGame()
    {
        // Keluar dari game
        Application.Quit();

        // Jika dijalankan di editor Unity, akan menampilkan pesan
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
