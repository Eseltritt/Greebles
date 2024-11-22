using UnityEngine;

public class SimpleCatController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed
    public float rotationSpeed = 720f; // Rotation speed (degrees per second)

    void Update()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude > 0.1f)
        {
            // Move the cat
            transform.position += movement * moveSpeed * Time.deltaTime;

            // Rotate the cat to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
