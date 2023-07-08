using UnityEngine;

public class MoveableCube : MonoBehaviour
{
    private Rigidbody rb;
    private bool isBeingPushed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (isBeingPushed)
        {
            // Küpü hareket ettirme iþlemini burada yapabilirsiniz
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = false;
        }
    }
}