using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverObject : MonoBehaviour
{


    public GameObject explosionPrefab; // Patlama efekti prefabý
    public float respawnDelay = 2f; // Yeniden doðma gecikme süresi

    public GameObject playerCube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Instantiate(explosionPrefab, playerCube.transform.position, Quaternion.identity);
            playerCube.gameObject.SetActive(false); // Karakteri devre dýþý býrakarak yok edelim
            StartCoroutine(RespawnCoroutine()); // Belirli bir süre sonra karakteri tekrar oluþtur
            playerCube.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;

        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay); // Yeniden doðma gecikme süresi kadar beklet
        playerCube.gameObject.SetActive(true); // Karakteri tekrar etkinleþtir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerCube.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
}
