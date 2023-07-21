using UnityEngine;

public class FinishCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.LoadNextLevel();
        }
        else
        {
            Debug.LogError("LevelManager not found.");
        }
    }
}
