using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody rb;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * movementSpeed;
    }
}
