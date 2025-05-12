using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool isCollected = false;
    public AudioClip pickupSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isCollected = true;

            // add score to player Adam Nixdorf
            PlayerInformation.control.SetScore(10);

            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        }

}
