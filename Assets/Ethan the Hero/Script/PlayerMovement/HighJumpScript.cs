using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using EthanTheHero;

public class HighJumpScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement != null && Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.ActivateHighJump();
        }
    }
}