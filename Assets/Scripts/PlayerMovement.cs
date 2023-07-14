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

    private string currentLevel = ""; // Mevcut seviye
    private int levelBase = 1; // Baþlangýç seviye sayýsý

    private int levelNumber = 1; // Mevcut seviye numarasý

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void SetLevelNumber(int level)
    {
        levelNumber = level;
    }


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

        // "LevelX" adýndaki game objelerin sayýsýný alarak levelBase'i güncelle
        int levelCount = GameObject.FindGameObjectsWithTag("LevelCube").Length;
        levelBase = levelCount + 1; // Toplam seviye sayýsýna 1 ekleyerek levelBase'i güncelle

        // Kayýtlý seviye ilerlemesini kontrol et
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string savedLevel = PlayerPrefs.GetString("CurrentLevel");
            if (int.TryParse(savedLevel.Substring(5), out int savedLevelNumber))
            {
                levelBase = savedLevelNumber + 1; // Kayýtlý seviye numarasýna 1 ekleyerek levelBase'i güncelle
            }
        }
        else
        {
            PlayerPrefs.SetString("CurrentLevel", "Level1");
        }
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityR")) // Yerçekimi deðiþtirme alanýna girdiðinde
        {
            gravityScale *= -1f; // Yerçekimini ters çevir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("LevelCube")) // Seviye küpüne deðdiðinde
        {
            string levelName = other.gameObject.name; // Küpün ismini al
            int levelNumber;
            if (int.TryParse(levelName.Substring(5), out levelNumber)) // Level numarasýný çýkar
            {
                string targetLevel = "Level" + levelNumber;

                if (levelNumber <= levelBase)
                {
                    currentLevel = targetLevel; // Mevcut seviyeyi güncelle
                    SceneManager.LoadScene(targetLevel); // Ýlgili leveli yükle
                }
            }
        }





    }

    private void OnDestroy()
    {
        // Mevcut seviyeyi kaydet
        string currentLevelName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("CurrentLevel", currentLevelName);
    }
}
