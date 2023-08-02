using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public ParticleSystem particleEffectPrefab; // Particle efekti prefab�
    private float slowMotionFactor = 0.50f; // Yava�latma fakt�r�

    private ParticleSystem particleEffect; // Olu�turulan particle efekti referans�

    private int playersInsideCollider = 0; // Sayac� tan�mla

    private bool isSlowMotionActive = false; // Slow-motion aktif mi?

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInsideCollider == 0) // �lk oyuncuysa yava�latmay� ba�lat
            {
                Time.timeScale = slowMotionFactor; // Oyun zaman�n� yava�lat
                particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity); // Particle efektini olu�tur ve referans� kaydet
                isSlowMotionActive = true; // Slow-motion aktif
            }
            playersInsideCollider++; // Sayac� art�r
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInsideCollider--; // Sayac� azalt

            if (playersInsideCollider == 0) // Son oyuncuysa yava�latmay� sonland�r
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
                isSlowMotionActive = false; // Slow-motion aktif de�il
            }
        }
    }

    private void Update()
    {
        // ESC tu�una bas�ld���nda ve slow-motion aktifse yava�latmay� sonland�r
        if (isSlowMotionActive && Input.GetKeyDown(KeyCode.Escape))
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
            isSlowMotionActive = false; // Slow-motion aktif de�il
        }
    }
}
