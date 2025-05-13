using EthanTheHero;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HighJumpPowerUp : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private VisualElement popup;
    private UIDocument uiDocument;
    public UIMain uiMain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>();

        uiDocument = GameObject.Find("uIMain").GetComponent<UIDocument>();

        StartCoroutine(WaitForUIDocumentInitialization());
    }

    private IEnumerator WaitForUIDocumentInitialization()
    {
        yield return null;

        var root = uiDocument.rootVisualElement;

        popup = root.Query<VisualElement>("PowerUp_PopUp").First();

        if (popup == null)
        {
            Debug.LogError("Popup element (PowerUp_PopUp) not found in the UI");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerMovement != null)
        {
            playerMovement.ActivateHighJump();

            if (popup != null)
            {
                StartCoroutine(ShowPopup());
            }

            HidePowerUpObject();
        }        
    }

    private IEnumerator ShowPopup()
    {
        popup.style.display = DisplayStyle.Flex;

        yield return new WaitForSeconds(5f);

        popup.style.display = DisplayStyle.None;
    }
    
    private void HidePowerUpObject()
    {
        var renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.enabled = false;
        }
        else
        {
            popup.style.display = DisplayStyle.None;
        }
    }
}
