using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathController : MonoBehaviour
{
    //public GameObject explosionPrefab; // Patlama efekti prefabý
    //public float respawnDelay = 2f; // Yeniden doðma gecikme süresi

    //private Vector3 startPosition; // Baþlangýç konumu

    //private void Start()
    //{
    //    startPosition = transform.position; // Baþlangýç konumunu kaydet
    //}

    //public void Die()
    //{
    //    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //    gameObject.SetActive(false); // Karakteri devre dýþý býrakarak yok edelim
    //    StartCoroutine(RespawnCoroutine()); // Belirli bir süre sonra karakteri tekrar oluþtur
    //}

    //private IEnumerator RespawnCoroutine()
    //{
    //    yield return new WaitForSeconds(respawnDelay); // Yeniden doðma gecikme süresi kadar beklet
    //    gameObject.SetActive(true); // Karakteri tekrar etkinleþtir
    //    transform.position = startPosition; // Baþlangýç konumuna geri yerleþtir
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
