using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Oyuncunun hareket h�z�
    public float jumpForce = 5f; // Z�plama kuvveti
    public float gravityScale = 1f; // Yer�ekimi �l�e�i

    private Rigidbody rb;
    private bool isJumping = false;
    private bool isTouchingWall = false;
    private GameObject currentCube;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // rotasyonu dondur
        rb.useGravity = false; // Ba�lang��ta yer�ekimini devre d��� b�rak
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Space tu�una bas�ld���nda ve z�plama durumunda de�ilse
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Yukar� do�ru z�plama kuvveti uygula
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Yatay (a/d) giri�i al

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f); // Hareket vekt�r�n� olu�tur

        // K�p hareketi
        

        if (!isTouchingWall)
        {
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0f); // Hareketi uygula
        }

        // Yer�ekimi �l�e�ini uygula
        Vector3 gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Oyuncu zemine temas etti�inde
        {
            isJumping = false;
            isTouchingWall = false;
        }
        else if (collision.gameObject.CompareTag("Wall")) // Duvara temas etti�inde
        {
            isTouchingWall = true;
        }
      
    }

    
}
