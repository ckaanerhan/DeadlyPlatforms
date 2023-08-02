using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionScene : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int maxLevelUnlocked; // Oynanabilir en yüksek seviye numarasýný tutmak için public bir deðiþken

    public float holdTimeRequired = 3f; // The duration the "R" key needs to be held in seconds
    private bool isHoldingR = false;

    private void Start()
    {
        // Kaydedilen en yüksek seviye numarasýný yükle veya varsayýlan olarak 1 ayarla
        maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);

        // Set the maxLevelUnlocked value in PlayerMovement script to match the value in LevelSelectionScene script
        playerMovement.maxLevelUnlocked = maxLevelUnlocked;

        // Tüm küpleri döngü ile kontrol edelim
        LevelCube[] levelCubes = FindObjectsOfType<LevelCube>();
        foreach (LevelCube levelCube in levelCubes)
        {
            // Eðer küpün seviye numarasý, oynanabilir en yüksek seviye numarasýndan büyükse
            // küpü görünmez yap ve Collider'ýný devre dýþý býrak
            if (levelCube.LevelNumber > maxLevelUnlocked)
            {
                levelCube.gameObject.SetActive(false);
                levelCube.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isHoldingR = true;
            StartCoroutine(WaitForReset());
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isHoldingR = false;
        }
    }

    private System.Collections.IEnumerator WaitForReset()
    {
        float timer = 0f;

        while (isHoldingR)
        {
            timer += Time.deltaTime;

            // If the R key has been held for the required holdTime, reset maxLevelUnlocked
            if (timer >= holdTimeRequired)
            {
                ResetMaxLevelUnlocked();
                isHoldingR = false;
                yield break;
            }

            yield return null;
        }
    }

    private void ResetMaxLevelUnlocked()
    {
        // Reset the maxLevelUnlocked to 1
        PlayerPrefs.SetInt("MaxLevelUnlocked", 1);

        // Reload the current scene to update the LevelCube objects based on the new maxLevelUnlocked value
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }
}
