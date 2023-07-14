using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public ParticleSystem particleEffect1Prefab; // Birinci particle efekti prefabý
    public ParticleSystem particleEffect2Prefab; // Ýkinci particle efekti prefabý
    public ParticleSystem particleEffect3Prefab; // Birinci particle efekti prefabý
    public ParticleSystem particleEffect4Prefab; // Ýkinci particle efekti prefabý
    public ParticleSystem particleEffect5Prefab; // Birinci particle efekti prefabý
    private float slowMotionFactor = 0.50f; // Yavaþlatma faktörü

    private ParticleSystem particleEffect1; // Oluþturulan birinci particle efekti referansý
    private ParticleSystem particleEffect2; // Oluþturulan ikinci particle efekti referansý
    private ParticleSystem particleEffect3; // Oluþturulan birinci particle efekti referansý
    private ParticleSystem particleEffect4; // Oluþturulan ikinci particle efekti referansý
    private ParticleSystem particleEffect5; // Oluþturulan birinci particle efekti referansý


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = slowMotionFactor; // Oyun zamanýný yavaþlat
            particleEffect1 = Instantiate(particleEffect1Prefab, transform.position, Quaternion.identity); // Birinci particle efektini oluþtur ve referansý kaydet
            particleEffect2 = Instantiate(particleEffect2Prefab, transform.position, Quaternion.identity); // Ýkinci particle efektini oluþtur ve referansý kaydet
            particleEffect3 = Instantiate(particleEffect3Prefab, transform.position, Quaternion.identity); // Birinci particle efektini oluþtur ve referansý kaydet
            particleEffect4 = Instantiate(particleEffect4Prefab, transform.position, Quaternion.identity); // Ýkinci particle efektini oluþtur ve referansý kaydet
            particleEffect5 = Instantiate(particleEffect5Prefab, transform.position, Quaternion.identity); // Birinci particle efektini oluþtur ve referansý kaydet
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f; // Oyun zamanýný normale döndür
            if (particleEffect1 != null)
            {
                var particleMain1 = particleEffect1.main;
                particleMain1.stopAction = ParticleSystemStopAction.Callback;
                particleMain1.loop = false;
                particleEffect1.Stop();
            }
            if (particleEffect2 != null)
            {
                var particleMain2 = particleEffect2.main;
                particleMain2.stopAction = ParticleSystemStopAction.Callback;
                particleMain2.loop = false;
                particleEffect2.Stop();
            }
            if (particleEffect3 != null)
            {
                var particleMain3 = particleEffect3.main;
                particleMain3.stopAction = ParticleSystemStopAction.Callback;
                particleMain3.loop = false;
                particleEffect3.Stop();
            }
            if (particleEffect4 != null)
            {
                var particleMain4 = particleEffect4.main;
                particleMain4.stopAction = ParticleSystemStopAction.Callback;
                particleMain4.loop = false;
                particleEffect4.Stop();
            }
            if (particleEffect5 != null)
            {
                var particleMain5 = particleEffect5.main;
                particleMain5.stopAction = ParticleSystemStopAction.Callback;
                particleMain5.loop = false;
                particleEffect5.Stop();
            }
            
        }
    }
}
