using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using EthanTheHero;

public class SuperSpeedPowerUp : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private VisualElement popup; // Reference to the PowerUp Popup UI element
    private UIDocument uiDocument;
    public UIMain uiMain; // Reference to the UIMain script

    void Start()
    {
        // Get the PlayerMovement component from the player
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();

        // Find the UIDocument attached to the uIMain GameObject
        uiDocument = GameObject.Find("uIMain").GetComponent<UIDocument>();

        // Start the coroutine to wait for UI initialization
        StartCoroutine(WaitForUIDocumentInitialization());
    }

    private IEnumerator WaitForUIDocumentInitialization()
    {
        // Wait for a frame to make sure the UI is fully loaded
        yield return null;

        // Get the root visual element from the UIDocument
        var root = uiDocument.rootVisualElement;

        // Get the PowerUp_PopUp element by its name, without the "#"
        popup = root.Query<VisualElement>("PowerUp_PopUp").First();

        // If popup is null, log an error
        if (popup == null)
        {
            Debug.LogError("Popup element (PowerUp_PopUp) not found in the UI.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player") && playerMovement != null)
        {
            // Activate super speed when the player collides with the power-up
            playerMovement.ActivateSuperSpeed();

            // Start showing the popup for 5 seconds
            if (popup != null)
            {
                StartCoroutine(ShowPopup());
            }

            // Hide the power-up object instead of destroying it
            HidePowerUpObject();
        }
    }

    // Coroutine to show the PowerUp popup and hide it after 5 seconds
    private IEnumerator ShowPopup()
    {
        // Show the popup
        popup.style.display = DisplayStyle.Flex;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide the popup after 5 seconds
        popup.style.display = DisplayStyle.None;
    }

    // Method to hide the power-up object without destroying it
    private void HidePowerUpObject()
    {
        // If it's a 3D object, disable the renderer to hide it visually
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;  // This will hide the object visually but keep it active in the scene
        }
        else
        {
            // If it's a UI element, hide its visual display
            popup.style.display = DisplayStyle.None;  // Hide the popup UI element
        }
    }
}
