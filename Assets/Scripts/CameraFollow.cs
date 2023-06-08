using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Oyuncunun Transform bile�eni

    public float smoothSpeed = 0.125f; // Kameran�n takip etme h�z�
    public Vector3 offset; // Kamera ve oyuncu aras�ndaki mesafe

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Oyuncunun pozisyonunu ve offset'i kullanarak hedef pozisyonu hesapla
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Kameran�n yumu�ak bir �ekilde hedefe do�ru hareket etmesini sa�la
        transform.position = smoothedPosition; // Kameran�n pozisyonunu g�ncelle
    }
}