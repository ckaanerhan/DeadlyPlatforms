using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private GameObject currentCube;
    public float speed = 5f; // Oyuncunun hareket hýzý

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MoveableCube"))
        {
            currentCube = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MoveableCube"))
        {
            currentCube = null;
        }
    }

    public void MoveCube(float moveHorizontal)
    {
        if (currentCube != null)
        {
            // Küpü hareket ettirme iþlemini burada yapabilirsiniz
            Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);
            currentCube.transform.position += movement * speed * Time.deltaTime;
        }
    }
}