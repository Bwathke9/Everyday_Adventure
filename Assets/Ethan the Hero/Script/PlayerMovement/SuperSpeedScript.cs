using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using EthanTheHero;

public class SuperSpeedScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement != null && Input.GetKeyDown(KeyCode.Space))
        {
            // Now call the ActivateSuperSpeed method directly from PlayerMovement
            playerMovement.ActivateSuperSpeed();
        }
    }
}
