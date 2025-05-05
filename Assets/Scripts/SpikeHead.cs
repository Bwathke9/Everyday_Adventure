using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public float damage = 25f; // Amount of damage to deal

    [SerializeField]
    private float speed = 1f; // Speed of the spike head

    [SerializeField]
    private float distance = 5f; // Distance to move

    [SerializeField]
    private float waitTime = 1f; // Time to wait before moving again

    [SerializeField]
    private Transform target; // Target to move towards

    [SerializeField]
    private bool playOnStart = false; // Whether to start moving on start

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        
    }

}
