using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string targetLevel; // Hedef seviye adý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Burada herhangi bir iþlem yapmanýza gerek yok, PlayerMovement scripti bu triggeri algýlayacak ve iþlemleri gerçekleþtirecektir.
        }
    }
}
