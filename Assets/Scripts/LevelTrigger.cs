using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string targetLevel; // Hedef seviye ad�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Burada herhangi bir i�lem yapman�za gerek yok, PlayerMovement scripti bu triggeri alg�layacak ve i�lemleri ger�ekle�tirecektir.
        }
    }
}
