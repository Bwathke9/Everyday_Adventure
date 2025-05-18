using UnityEngine;

public class StartTimer : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerInformation.control.isPaused == true)
            {
                PlayerInformation.control.isPaused = false;
            }

        }
        PlayerInformation.control.Heal();
    }
}
