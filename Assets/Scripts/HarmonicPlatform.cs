using UnityEngine;

public class HarmonicPlatform : MonoBehaviour
{
    public float amplitude = 2f; // Hareket genliði
    public float frequency = 1f; // Hareket frekansý
    public float offsetY = 0f; // Y ekseni baþlangýç konumu

    private Vector3 startPos; // Baþlangýç pozisyonu

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
