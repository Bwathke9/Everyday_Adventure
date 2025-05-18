using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    public float powerUpLevel= 0;
    public float powerUpDuration = 10;
    public static PowerUpIndicator powerUpIndicator;
    private float powerUpEndTime;

    void Update()
    {
       
        if (powerUpLevel > 0)
        {
            powerUpLevel = (Mathf.Clamp01((powerUpEndTime - Time.time) / powerUpDuration) * 100);
            // Update the power-up indicator UI here
           
            // For example, you can use a UI element to show the power-up level
            PlayerInformation.control.SetPowerUp(powerUpLevel);
            // If the power-up level reaches 0, hide the indicator

            if (powerUpLevel <= 0)
            {
                powerUpLevel = 0;
                // Hide the power-up indicator
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Activate super speed when the player collides with the power-up
            powerUpLevel = 100;
            powerUpEndTime = Time.time + powerUpDuration;      
           
        }
    }

}
