using UnityEngine;

public class MultiAxisPingPong : MonoBehaviour
{
    // Speed factor for horizontal movement
    public float xSpeed = 1.0f;
    // Amplitude for horizontal motion (how far on the x-axis from start)
    public float xAmplitude = 2.0f;

    // Speed factor for vertical movement
    public float ySpeed = 1.0f;
    // Amplitude for vertical motion (how far on the y-axis from start)
    public float yAmplitude = 2.0f;

    // Object starting position
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Compute a new x-offset.
        // Mathf.PingPong returns a value between 0 and xAmplitude.
        // Subtracting xAmplitude/2 centers the oscillation around the start position.
        float newX = Mathf.PingPong(Time.time * xSpeed, xAmplitude) - xAmplitude / 2f;

        // Compute a new y-offset using the same principle.
        float newY = Mathf.PingPong(Time.time * ySpeed, yAmplitude) - yAmplitude / 2f;

        // Update the object's position by adding the new offsets to its original position.
        transform.position = new Vector3(startPosition.x + newX, startPosition.y + newY, startPosition.z);
    }
}
