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
    private Material originalMaterial;
    private Material yellowMaterial;

    private string currentLevel = ""; // Mevcut seviye
    private int levelBase = 1; // Ba�lang�� seviye say�s�

    private int levelNumber = 1; // Mevcut seviye numaras�

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
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // rotasyonu dondur
        rb.useGravity = false; // Ba�lang��ta yer�ekimini devre d��� b�rak

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = cubeRenderer.material;

        // Sar� renkteki malzemeyi olu�turun
        yellowMaterial = new Material(originalMaterial);
        yellowMaterial.color = Color.yellow;

        // "LevelX" ad�ndaki game objelerin say�s�n� alarak levelBase'i g�ncelle
        int levelCount = GameObject.FindGameObjectsWithTag("LevelCube").Length;
        levelBase = levelCount + 1; // Toplam seviye say�s�na 1 ekleyerek levelBase'i g�ncelle

        // Kay�tl� seviye ilerlemesini kontrol et
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string savedLevel = PlayerPrefs.GetString("CurrentLevel");
            if (int.TryParse(savedLevel.Substring(5), out int savedLevelNumber))
            {
                levelBase = savedLevelNumber + 1; // Kay�tl� seviye numaras�na 1 ekleyerek levelBase'i g�ncelle
            }
        }
        else
        {
            PlayerPrefs.SetString("CurrentLevel", "Level1");
        }
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityR")) // Yer�ekimi de�i�tirme alan�na girdi�inde
        {
            gravityScale *= -1f; // Yer�ekimini ters �evir
            jumpForce *= -1f;
        }
        else if (other.CompareTag("LevelCube")) // Seviye k�p�ne de�di�inde
        {
            string levelName = other.gameObject.name; // K�p�n ismini al
            int levelNumber;
            if (int.TryParse(levelName.Substring(5), out levelNumber)) // Level numaras�n� ��kar
            {
                string targetLevel = "Level" + levelNumber;

                if (levelNumber <= levelBase)
                {
                    currentLevel = targetLevel; // Mevcut seviyeyi g�ncelle
                    SceneManager.LoadScene(targetLevel); // �lgili leveli y�kle
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
