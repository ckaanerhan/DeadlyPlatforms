using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathController : MonoBehaviour
{
    //public GameObject explosionPrefab; // Patlama efekti prefab�
    //public float respawnDelay = 2f; // Yeniden do�ma gecikme s�resi

    //private Vector3 startPosition; // Ba�lang�� konumu

    //private void Start()
    //{
    //    startPosition = transform.position; // Ba�lang�� konumunu kaydet
    //}

    //public void Die()
    //{
    //    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //    gameObject.SetActive(false); // Karakteri devre d��� b�rakarak yok edelim
    //    StartCoroutine(RespawnCoroutine()); // Belirli bir s�re sonra karakteri tekrar olu�tur
    //}

    //private IEnumerator RespawnCoroutine()
    //{
    //    yield return new WaitForSeconds(respawnDelay); // Yeniden do�ma gecikme s�resi kadar beklet
    //    gameObject.SetActive(true); // Karakteri tekrar etkinle�tir
    //    transform.position = startPosition; // Ba�lang�� konumuna geri yerle�tir
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
