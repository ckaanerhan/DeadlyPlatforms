using UnityEngine;

public class HarmonicPlatform : MonoBehaviour
{
    public float amplitude = 2f; // Hareket genli�i
    public float frequency = 1f; // Hareket frekans�
    public float offsetY = 0f; // Y ekseni ba�lang�� konumu

    private Vector3 startPos; // Ba�lang�� pozisyonu

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float y = startPos.y + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector3(transform.position.x, y + offsetY, transform.position.z);
    }
}
