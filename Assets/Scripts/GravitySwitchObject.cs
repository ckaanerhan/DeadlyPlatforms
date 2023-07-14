using UnityEngine;

public class GravitySwitchObject : MonoBehaviour
{
    public ParticleSystem particleEffect; // Particle effect prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = !rb.useGravity; // Toggle gravity

                if (particleEffect != null)
                {
                    var particleMain = particleEffect.main;
                    particleMain.gravityModifierMultiplier = rb.useGravity ? 1f : -1f; // Update particle effect's gravity based on the player's gravity
                }
            }
        }
    }
}
