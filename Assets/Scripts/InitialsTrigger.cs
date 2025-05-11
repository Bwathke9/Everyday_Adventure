// Brennan Wathke 5/11/2025
using UnityEngine;

public class InitialsTrigger : MonoBehaviour
{
    [SerializeField] private InitialsInput initialsPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!PlayerInformation.control.isPaused)
            {
                PauseGame.pauseResumeGame();
            }
            initialsPopup.ShowPopup();
        }
    }
}
