using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public string levelNamePrefix = "Level"; // Seviye ad� �neki

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Mevcut seviye numaras�n� al, varsay�lan de�er 1

            int nextLevel = currentLevel + 1; // Bir sonraki seviye numaras�n� hesapla

            string targetLevel = levelNamePrefix + nextLevel; // Yeni hedef seviye ad�n� olu�tur

            SceneManager.LoadScene(targetLevel); // Hedef seviyeyi y�kle

            PlayerPrefs.SetInt("CurrentLevel", nextLevel); // Bir sonraki seviye numaras�n� kaydet
        }
    }
}
