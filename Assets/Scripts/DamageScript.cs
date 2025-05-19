using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private float damageAmount = 15f; // Amount of damage to deal
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInformation.control.TakeDamage(damageAmount);
        }
    }

}
