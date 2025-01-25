using UnityEngine;
using UnityEngine.SceneManagement; 
public class Spike : MonoBehaviour
{
    public string mainMenuSceneName = "Mainmenu"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(mainMenuSceneName); 
        }
    }
}
