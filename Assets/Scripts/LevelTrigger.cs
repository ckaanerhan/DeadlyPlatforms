using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public string levelNamePrefix = "Level"; // Seviye adý öneki

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Mevcut seviye numarasýný al, varsayýlan deðer 1

            int nextLevel = currentLevel + 1; // Bir sonraki seviye numarasýný hesapla

            string targetLevel = levelNamePrefix + nextLevel; // Yeni hedef seviye adýný oluþtur

            SceneManager.LoadScene(targetLevel); // Hedef seviyeyi yükle

            PlayerPrefs.SetInt("CurrentLevel", nextLevel); // Bir sonraki seviye numarasýný kaydet
        }
    }
}
