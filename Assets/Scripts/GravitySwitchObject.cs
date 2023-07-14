using UnityEngine;

public class GravitySwitchObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = !rb.useGravity; // Yerçekimini tersine çevir
            }
        }
    }
}
