using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public ParticleSystem particleEffectPrefab; // Particle efekti prefabý
    private float slowMotionFactor = 0.50f; // Yavaþlatma faktörü

    private ParticleSystem particleEffect; // Oluþturulan particle efekti referansý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowMotionFactor; // Oyun zamanýný yavaþlat
            particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity); // Particle efektini oluþtur ve referansý kaydet
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Oyun zamanýný normale döndür
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
