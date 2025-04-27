// Brennan Wathke 4/26/25
using UnityEngine;

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
}
