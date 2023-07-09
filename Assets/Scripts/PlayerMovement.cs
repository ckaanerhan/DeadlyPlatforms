using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Oyuncunun hareket hýzý
    public float jumpForce = 5f; // Zýplama kuvveti
    public float gravityScale = 1f; // Yerçekimi ölçeði

    private Rigidbody rb;
    private bool isJumping = false;
    private bool isTouchingWall = false;
    private GameObject currentCube;

    private int jumpCount = 0;
    private MeshRenderer cubeRenderer;
    private Material originalMaterial;
    private Material yellowMaterial;



    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // rotasyonu dondur
        rb.useGravity = false; // Baþlangýçta yerçekimini devre dýþý býrak

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = cubeRenderer.material;

        // Sarý renkteki malzemeyi oluþturun
        yellowMaterial = new Material(originalMaterial);
        yellowMaterial.color = Color.yellow;

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

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Yatay (a/d) giriþi al

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f); // Hareket vektörünü oluþtur

        // Küp hareketi
        

        if (!isTouchingWall)
        {
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0f); // Hareketi uygula
        }

        // Yerçekimi ölçeðini uygula
        Vector3 gravity = gravityScale * Physics.gravity;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Oyuncu zemine temas ettiðinde
        {
            isJumping = false;
            isTouchingWall = false;
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Wall")) // Duvara temas ettiðinde
        {
            isTouchingWall = true;
        }
      
    }

    
}
