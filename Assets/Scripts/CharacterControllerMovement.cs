using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    public float friction = 5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontalInput, 0f, 0f);
        moveDirection *= moveSpeed;

        // Sürtünme kuvvetini uygula
        moveDirection.x -= moveDirection.x * friction * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
