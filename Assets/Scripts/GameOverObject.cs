using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverObject : MonoBehaviour
{


    public GameObject explosionPrefab; // Patlama efekti prefab�
    public float respawnDelay = 2f; // Yeniden do�ma gecikme s�resi

    public GameObject playerCube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Instantiate(explosionPrefab, playerCube.transform.position, Quaternion.identity);
            playerCube.gameObject.SetActive(false); // Karakteri devre d��� b�rakarak yok edelim
            StartCoroutine(RespawnCoroutine()); // Belirli bir s�re sonra karakteri tekrar olu�tur
            playerCube.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;

        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay); // Yeniden do�ma gecikme s�resi kadar beklet
        playerCube.gameObject.SetActive(true); // Karakteri tekrar etkinle�tir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerCube.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
}
