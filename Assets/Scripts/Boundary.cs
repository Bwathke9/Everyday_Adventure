// By Adam Nixdorf

// This script is designed to supply a boundary for the player 

using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInformation.control.TakeDamage(100);
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }
}
