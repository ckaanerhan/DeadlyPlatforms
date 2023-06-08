using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Oyuncunun Transform bileþeni

    public float smoothSpeed = 0.125f; // Kameranýn takip etme hýzý
    public Vector3 offset; // Kamera ve oyuncu arasýndaki mesafe

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Oyuncunun pozisyonunu ve offset'i kullanarak hedef pozisyonu hesapla
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Kameranýn yumuþak bir þekilde hedefe doðru hareket etmesini saðla
        transform.position = smoothedPosition; // Kameranýn pozisyonunu güncelle
    }
}