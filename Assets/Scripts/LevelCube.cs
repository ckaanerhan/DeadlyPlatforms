using UnityEngine;

public class LevelCube : MonoBehaviour
{
    public int LevelNumber; // Seviye numarasý
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private void Start()
    {
        // Get the maxLevelUnlocked value from the PlayerMovement script
        int maxLevelUnlocked = playerMovement.maxLevelUnlocked;

        // Check if the LevelNumber is greater than the maxLevelUnlocked
        // If yes, disable the gameObject
        if (LevelNumber > maxLevelUnlocked)
        {
            gameObject.SetActive(false);
        }
    }
}
