using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    // This will hold the center of the circle (i.e., the object’s starting position)
    private Vector2 centerPosition;

    // The radius of our circular path.
    public float radius = 1.0f;

    // Angular speed in degrees per second.
    public float angularSpeed = 50.0f;

    // Current angle in degrees.
    private float currentAngle = 0f;

    void Start()
    {
        // Store the object's initial position as the center of the orbit.
        centerPosition = transform.position;
    }

    void Update()
    {
        // Increase the angle over time (frame rate–independent).
        currentAngle += angularSpeed * Time.deltaTime;

        // Convert angle to radians (for trigonometric functions).
        float angleRad = currentAngle * Mathf.Deg2Rad;

        // Calculate x and y positions relative to the center stored at Start().
        float x = centerPosition.x + Mathf.Cos(angleRad) * radius;
        float y = centerPosition.y + Mathf.Sin(angleRad) * radius;

        // Update the position of the object.
        transform.position = new Vector2(x, y);
    }
}
