using UnityEngine;

public class HarmonicPlatformHorizontal : MonoBehaviour
{
    public float amplitude = 2f; // Hareket genli�i
    public float frequency = 1f; // Hareket frekans�
    public float offsetX = 0f; // X ekseni ba�lang�� konumu

    private Vector3 startPos; // Ba�lang�� pozisyonu

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float x = startPos.x + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector3(x + offsetX, transform.position.y, transform.position.z);
    }
}
