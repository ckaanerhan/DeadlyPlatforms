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

    public int LevelNumber;

    public int maxLevelUnlocked;

    private int currentLevelIndex; // �u anki seviyenin indeksi
    private string currentLevelName; // �u anki seviyenin ad�


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

            SceneManager.LoadScene("LevelSelectionScene");
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
            LevelCube levelCube = other.gameObject.GetComponent<LevelCube>();
            if (levelCube != null)
            {
                int levelNumber = levelCube.LevelNumber;

                // Seviye numaras�n� kaydet
                LevelNumber = levelNumber;
                PlayerPrefs.SetInt("LevelNumber", levelNumber);

                // Seviye k�p�ne de�di�imizde sahneyi y�kle
                SceneManager.LoadScene("Level" + levelNumber);
            }
        }

        if (other.CompareTag("FinishCube")) // FinishCube tag'ine sahip bir nesneye de�di�inde
        {
            
            if (int.TryParse(other.gameObject.name, out LevelNumber)) // Nesnenin isminden say�y� al ve d�n��t�r
            {
                Debug.Log("LevelNumber integer d�n��t�r�ld�");
                if (LevelNumber > maxLevelUnlocked)
                {
                    maxLevelUnlocked = LevelNumber;

                    // Yeni en y�ksek seviye numaras�n� PlayerPrefs'e kaydedelim.
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
        // �u anki seviyenin ad�n� ve indeksini g�ncelle
        currentLevelName = SceneManager.GetActiveScene().name;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Max level unlocked de�erini g�ncelle
        maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);
    }


}
