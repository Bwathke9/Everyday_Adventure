using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInformation.control.TakeDamage(100);
    }
}
