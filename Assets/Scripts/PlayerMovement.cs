using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Oyuncunun hareket hýzý
    public float jumpForce = 5f; // Zýplama kuvveti
    public float gravityScale = 1f; // Yerçekimi ölçeði

    private Rigidbody rb;
    private bool isJumping = false;
    private GameObject currentCube;

    private int jumpCount = 0;
    private MeshRenderer cubeRenderer;
    public Material originalMaterial;
    public Material yellowMaterial;


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // rotasyonu dondur
        rb.useGravity = false; // Baþlangýçta yerçekimini devre dýþý býrak

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        cubeRenderer.material = originalMaterial;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Space tuþuna basýldýðýnda ve zýplama durumunda deðilse
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Yukarý doðru zýplama kuvveti uygula
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

        if (Input.GetKeyDown(KeyCode.Escape)) // Esc tuþuna basýldýðýnda
        {
            SceneManager.LoadScene("LevelSelectionScene"); // Seviye seçim sahnesine geçiþ yap
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Yatay (a/d) giriþi al

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f); // Hareket vektörünü oluþtur

        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0f); // Hareketi uygula

        // Yerçekimi ölçeðini uygula
        Vector3 gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Oyuncu zemine temas ettiðinde
        {
            isJumping = false;
            jumpCount = 0;
            cubeRenderer.material = originalMaterial; // Zýplama bittiðinde malzemeyi eski haline getir
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityRUp") && gravityScale > 0) // Yerçekimi deðiþtirme alanýna girdiðinde
        {
            gravityScale *= -1f; // Yerçekimini ters çevir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("GravityRDown") && gravityScale < 0) // Yerçekimi deðiþtirme alanýna girdiðinde
        {
            gravityScale *= -1f; // Yerçekimini ters çevir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("LevelCube")) // Seviye küpüne deðdiðinde
        {
            string levelName = other.gameObject.name; // Küpün ismini al
            int levelNumber = int.Parse(levelName.Substring(5)); // Level numarasýný al

            SceneManager.LoadScene("Level" + levelNumber); // Ýlgili leveli yükle
        }
    }
}
