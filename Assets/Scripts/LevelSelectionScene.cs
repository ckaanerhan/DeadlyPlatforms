using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionScene : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int maxLevelUnlocked; // Oynanabilir en y�ksek seviye numaras�n� tutmak i�in public bir de�i�ken

    public float holdTimeRequired = 3f; // The duration the "R" key needs to be held in seconds
    private bool isHoldingR = false;

    private void Start()
    {
        // Kaydedilen en y�ksek seviye numaras�n� y�kle veya varsay�lan olarak 1 ayarla
        maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);

        // Set the maxLevelUnlocked value in PlayerMovement script to match the value in LevelSelectionScene script
        playerMovement.maxLevelUnlocked = maxLevelUnlocked;

        // T�m k�pleri d�ng� ile kontrol edelim
        LevelCube[] levelCubes = FindObjectsOfType<LevelCube>();
        foreach (LevelCube levelCube in levelCubes)
        {
            // E�er k�p�n seviye numaras�, oynanabilir en y�ksek seviye numaras�ndan b�y�kse
            // k�p� g�r�nmez yap ve Collider'�n� devre d��� b�rak
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
