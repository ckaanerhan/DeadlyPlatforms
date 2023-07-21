using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public ParticleSystem particleEffectPrefab; // Particle efekti prefab�
    private float slowMotionFactor = 0.50f; // Yava�latma fakt�r�

    private ParticleSystem particleEffect; // Olu�turulan particle efekti referans�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowMotionFactor; // Oyun zaman�n� yava�lat
            particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity); // Particle efektini olu�tur ve referans� kaydet
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Oyun zaman�n� normale d�nd�r
            if (particleEffect != null)
            {
                var particleMain = particleEffect.main;
                particleMain.stopAction = ParticleSystemStopAction.Callback;
                particleMain.loop = false;
                particleEffect.Stop();
                Destroy(particleEffect.gameObject, 2f); // 2 saniye sonra particle efektini yok et
            }
        }
    }
}
