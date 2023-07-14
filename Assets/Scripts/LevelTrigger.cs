using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string targetLevel; // Hedef seviye adı

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Burada herhangi bir işlem yapmanıza gerek yok, PlayerMovement scripti bu triggeri algılayacak ve işlemleri gerçekleştirecektir.
        }
    }
}
