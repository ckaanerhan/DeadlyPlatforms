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
    private Material originalMaterial;
    private Material yellowMaterial;

    public int LevelNumber;

    public int maxLevelUnlocked;

    private int currentLevelIndex; // Þu anki seviyenin indeksi
    private string currentLevelName; // Þu anki seviyenin adý


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = cubeRenderer.material;
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

        if (Input.GetKeyDown(KeyCode.Escape)) // Esc tuþuna basýldýðýnda
        {

            SceneManager.LoadScene("LevelSelectionScene");
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
            LevelCube levelCube = other.gameObject.GetComponent<LevelCube>();
            if (levelCube != null)
            {
                int levelNumber = levelCube.LevelNumber;

                // Seviye numarasýný kaydet
                LevelNumber = levelNumber;
                PlayerPrefs.SetInt("LevelNumber", levelNumber);

                // Seviye küpüne deðdiðimizde sahneyi yükle
                SceneManager.LoadScene("Level" + levelNumber);
            }
        }

        if (other.CompareTag("FinishCube")) // FinishCube tag'ine sahip bir nesneye deðdiðinde
        {
            
            if (int.TryParse(other.gameObject.name, out LevelNumber)) // Nesnenin isminden sayýyý al ve dönüþtür
            {
                Debug.Log("LevelNumber integer dönüþtürüldü");
                if (LevelNumber > maxLevelUnlocked)
                {
                    maxLevelUnlocked = LevelNumber;

                    // Yeni en yüksek seviye numarasýný PlayerPrefs'e kaydedelim.
                    PlayerPrefs.SetInt("MaxLevelUnlocked", maxLevelUnlocked);
                }
            }
        }

        if (other.CompareTag("ResetCube")) 
        {
            maxLevelUnlocked = 0;
            Debug.Log("hit");
        }
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Þu anki seviyenin adýný ve indeksini güncelle
        currentLevelName = SceneManager.GetActiveScene().name;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Max level unlocked deðerini güncelle
        maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);
    }


}
