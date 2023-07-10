using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Play"))
            {
                SceneManager.LoadScene("Level1");
            }
            else if (gameObject.CompareTag("Credits"))
            {
                SceneManager.LoadScene("Credits");
            }
            else if (gameObject.CompareTag("Quit"))
            {
                 Application.Quit();
            }
            else if (gameObject.CompareTag("Menu"))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
