using UnityEngine;

public class HarmonicPlatformHorizontal : MonoBehaviour
{
    public float amplitude = 2f; // Hareket genliði
    public float frequency = 1f; // Hareket frekansý
    public float offsetX = 0f; // X ekseni baþlangýç konumu

    private Vector3 startPos; // Baþlangýç pozisyonu

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
