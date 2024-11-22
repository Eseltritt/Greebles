using UnityEngine;

public class SlugWander : MonoBehaviour
{
    public float moveSpeed = 1f;               // Speed at which the slug moves
    public float directionChangeInterval = 3f; // Time in seconds before changing direction
    public float rotationSpeed = 2f;           // Speed at which the slug rotates toward new direction
    public float obstacleDetectionRange = 2f;  // Range for detecting obstacles in front
    public LayerMask obstacleLayer;            // Layer mask to specify what counts as an obstacle

    private Vector3 targetDirection;           // The direction in which the slug should move
    private float timeSinceDirectionChange;

    void Start()
    {
        ChooseRandomDirection();
    }

    void Update()
    {
        // Detect obstacles in the way and avoid them if necessary
        if (IsObstacleInPath())
        {
            AvoidObstacle();
        }
        else
        {
            // Move in the current direction if path is clear
            Move();

            // Change direction at regular intervals
            timeSinceDirectionChange += Time.deltaTime;
            if (timeSinceDirectionChange >= directionChangeInterval)
            {
                ChooseRandomDirection();
                timeSinceDirectionChange = 0;
            }
        }
    }

    void Move()
    {
        // Rotate smoothly towards the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Move forward in the facing direction
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    bool IsObstacleInPath()
    {
        // Cast a ray in the forward direction to detect obstacles
        return Physics.Raycast(transform.position, transform.forward, obstacleDetectionRange, obstacleLayer);
    }

    void AvoidObstacle()
    {
        // Choose a new random direction to avoid the obstacle
        ChooseRandomDirection();
    }

    void ChooseRandomDirection()
    {
        // Choose a random angle on the Y-axis and set it as the target direction
        float randomAngle = Random.Range(0f, 360f);
        targetDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the obstacle detection range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * obstacleDetectionRange);
    }
}
