// Brennan Wathke 4/26/25
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class OrbitPoint : MonoBehaviour
{
    public float orbitSpeed = 100f;

    private void Update() {
      OrbitAroundPoint();
    }

    private void OrbitAroundPoint() {

      float angle = orbitSpeed * Time.deltaTime;

      transform.Rotate(Vector3.forward, angle);
    }
    // add damage to player Adam Nixdorf

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInformation.control.TakeDamage(25);
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }

}


