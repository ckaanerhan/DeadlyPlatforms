using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Oyuncunun hareket h�z�
    public float jumpForce = 5f; // Z�plama kuvveti
    public float gravityScale = 1f; // Yer�ekimi �l�e�i

    private Rigidbody rb;
    private bool isJumping = false;
    private GameObject currentCube;

    private int jumpCount = 0;
    private MeshRenderer cubeRenderer;
    public Material originalMaterial;
    public Material yellowMaterial;


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // rotasyonu dondur
        rb.useGravity = false; // Ba�lang��ta yer�ekimini devre d��� b�rak

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        cubeRenderer.material = originalMaterial;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Space tu�una bas�ld���nda ve z�plama durumunda de�ilse
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Yukar� do�ru z�plama kuvveti uygula
            jumpCount++;
        }

        if (jumpCount > 0)
        {
            cubeRenderer.material = yellowMaterial;
        }
        else
        {
            cubeRenderer.material = originalMaterial;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Esc tu�una bas�ld���nda
        {
            SceneManager.LoadScene("LevelSelectionScene"); // Seviye se�im sahnesine ge�i� yap
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Yatay (a/d) giri�i al

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f); // Hareket vekt�r�n� olu�tur

        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0f); // Hareketi uygula

        // Yer�ekimi �l�e�ini uygula
        Vector3 gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Oyuncu zemine temas etti�inde
        {
            isJumping = false;
            jumpCount = 0;
            cubeRenderer.material = originalMaterial; // Z�plama bitti�inde malzemeyi eski haline getir
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityRUp") && gravityScale > 0) // Yer�ekimi de�i�tirme alan�na girdi�inde
        {
            gravityScale *= -1f; // Yer�ekimini ters �evir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("GravityRDown") && gravityScale < 0) // Yer�ekimi de�i�tirme alan�na girdi�inde
        {
            gravityScale *= -1f; // Yer�ekimini ters �evir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("LevelCube")) // Seviye k�p�ne de�di�inde
        {
            string levelName = other.gameObject.name; // K�p�n ismini al
            int levelNumber = int.Parse(levelName.Substring(5)); // Level numaras�n� al

            SceneManager.LoadScene("Level" + levelNumber); // �lgili leveli y�kle
        }
    }
}
