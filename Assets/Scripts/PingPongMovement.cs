using UnityEngine;

public class PingPOngMovement : MonoBehaviour
{

    [SerializeField] public float speed = 1.5f;

    [SerializeField] public float height = 2.0f;

    private Vector3 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.PingPong(Time.time * speed, height);

        transform.position = startPosition + new Vector3(0, newY, 0);
    }
}
