using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public ParticleSystem particleEffectPrefab; // Particle efekti prefabý
    private float slowMotionFactor = 0.50f; // Yavaþlatma faktörü

    private ParticleSystem particleEffect; // Oluþturulan particle efekti referansý

    private int playersInsideCollider = 0; // Sayacý tanýmla

    private bool isSlowMotionActive = false; // Slow-motion aktif mi?

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInsideCollider == 0) // Ýlk oyuncuysa yavaþlatmayý baþlat
            {
                Time.timeScale = slowMotionFactor; // Oyun zamanýný yavaþlat
                particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity); // Particle efektini oluþtur ve referansý kaydet
                isSlowMotionActive = true; // Slow-motion aktif
            }
            playersInsideCollider++; // Sayacý artýr
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInsideCollider--; // Sayacý azalt

            if (playersInsideCollider == 0) // Son oyuncuysa yavaþlatmayý sonlandýr
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
                isSlowMotionActive = false; // Slow-motion aktif deðil
            }
        }
    }

    private void Update()
    {
        // ESC tuþuna basýldýðýnda ve slow-motion aktifse yavaþlatmayý sonlandýr
        if (isSlowMotionActive && Input.GetKeyDown(KeyCode.Escape))
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
            isSlowMotionActive = false; // Slow-motion aktif deðil
        }
    }
}
