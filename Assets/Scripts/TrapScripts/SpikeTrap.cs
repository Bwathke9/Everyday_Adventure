using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInformation.control.TakeDamage(damage);
        }
    }
}
