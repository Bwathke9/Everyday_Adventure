//using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;
using System;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public float damage = 25f; // Amount of damage to deal

    [SerializeField]
    private float speed = 1f; // Speed of the spike head

    [SerializeField]
    private Vector2 distance = Vector2.zero; // Distance to move

    [SerializeField]
    private float waitTime = 1f; // Time to wait before moving again

    [SerializeField]
    private Transform target; // Target to move towards

    [SerializeField]
    private bool playOnStart = false; // Whether to start moving on start

    [SerializeField]
    private bool loop = false; // Whether to loop the movement

    private Vector2 startPos; // Starting position of the spike head
    private Vector2 endPos;
    private float curDistance;
    private float moveSpeed;
    private bool posDirection;
    private float timer;

    void Start()
    {
        // Set the starting position
        startPos = transform.position;
        // Set the end position based on the distance
        endPos = startPos + distance;
        // moveSpeed is the percentage to move per second using the speed variable
        // and lerp to move between the start and end positions
        moveSpeed = speed / Vector2.Distance(startPos,endPos);
        curDistance = 0f;
        timer = 0f;
        posDirection = true;
      
    }

    private void Update()
    {
        if (!playOnStart)
        {
            return;
        }

        if (timer >= 0f)
        {
            timer -= Time.deltaTime;
            return;
        }
        MoveObject();
    }

    private void MoveObject()
    {
        if (posDirection)
        {
            curDistance += moveSpeed * Time.deltaTime;
        }
        else
        {
            curDistance -= moveSpeed * Time.deltaTime;
        }
        transform.position = Vector2.Lerp(startPos, endPos, curDistance);

        if (posDirection)
        {
            if (curDistance >= 1.0f)
            {
                curDistance = 1.0f;
                posDirection = false;
                if (loop)
                {
                    timer = waitTime;
                }
                else
                {
                    playOnStart = false;
                }
                timer = waitTime;
            }
        }
        else
        {
            if (curDistance <= 0.0f)
            {
                curDistance = 0.0f;
                posDirection = true;
                if (loop)
                {
                    timer = waitTime;
                }
                else
                {
                    playOnStart = false;
                }
                timer = waitTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInformation.control.TakeDamage(damage);
        }

    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Collision stay detected with: " + collision.gameObject.name);
        float damageTimer = 10f;
        // Check if the object is the player
        if (collision.CompareTag("Player"))
        {
            //if (damageTimer >= 0)
            //{
            //    WaitForSeconds wait = new WaitForSeconds(damageTimer);
            //    // Wait for the specified time
            //    damageTimer -= 1;
            //    Debug.Log("Damage timer: " + damageTimer);                
            //}
            //else
            //{
                // Deal damage to the player
                PlayerInformation.control.TakeDamage(damage/100);
                // Reset the timer
                damageTimer = 1f;
                Debug.Log("Stay Damage dealt to player: " + damage);
            //}
        }
    }

}
