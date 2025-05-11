// Brennan Wathke 5/10/2025
using UnityEngine;


public class FirePotScript : MonoBehaviour

{
    public Sprite offSprite;
    public Sprite[] activeSprites;
    public float spriteChangeInterval = 0.3f;
    public float activeDuration = 5f;
    public float offDuration = 3f;
    public float damage = 25f;
    public float initialDelay = 0f;

    private SpriteRenderer spriteRenderer;
    private int currentActiveIndex = 0;
    private float timer = 0f;
    private float stateTimer = 0f;
    private bool isActive = false;
    private Collider2D physicalCollider;
    private Collider2D triggerCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicalCollider = GetComponent<Collider2D>();
        triggerCollider = gameObject.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.enabled = false; 

        Invoke("StartFirePot", initialDelay);
    }

    private void StartFirePot()
    {
        SetActiveState(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        stateTimer += Time.deltaTime;

        if (isActive)
        {
            if (timer >= spriteChangeInterval)
            {
                ChangeSprite();
                timer = 0f;
            }

            if (stateTimer >= activeDuration)
            {
                SetActiveState(false);
            }
        }
        else
        {
            if (stateTimer >= offDuration)
            {
                SetActiveState(true);
            }
        }
    }

    private void ChangeSprite()
    {
        currentActiveIndex++;

        if (currentActiveIndex >= activeSprites.Length)
        {
            currentActiveIndex = 0;
        }
        spriteRenderer.sprite = activeSprites[currentActiveIndex];
    }

    private void SetActiveState(bool active)
    {
        isActive = active;

        if (isActive)
        {
            spriteRenderer.sprite = activeSprites[currentActiveIndex];
            triggerCollider.enabled = true;
        }
        else
        {

            spriteRenderer.sprite = offSprite;
            triggerCollider.enabled = false;
        }
        timer = 0f;
        stateTimer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player")) 
        {
            PlayerInformation.control.TakeDamage(damage);
        }
    }
}
