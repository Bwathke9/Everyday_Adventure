using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f; // Speed at which the camera follows the player
    public float yOffset = 1f; // Offset in the y-axis to keep the player in view
    public Transform view;
 
    private void Update()
    {
        // Check if the player object is assigned
        if (view == null)
        {
            view = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogError("Player object is not assigned in the CameraFollow script.");
           
        }
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(view.position.x, view.position.y + yOffset, -10);
        // Move the camera towards the target position
        transform.position = Vector3.Slerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);

    }
    private void Start()
    {
        // Find the player object by tag
        view = GameObject.FindGameObjectWithTag("Player").transform;
        if (view == null)
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }

}
