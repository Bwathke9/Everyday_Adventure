// By Adam Nixdorf

// This script is designed to supply a boundary for the player 

using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInformation.control.TakeDamage(100);
        Debug.Log("Player has left the boundary and took damage.");
    }
}
