// By Adam Nixdorf
// This script is designed to track player information
using System;
using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour
{
    public static PlayerInformation control;

    public float currentHealth;
    public float maxHealth = 100;
    public int score;
    public int level;
    public float timer;
    public float powerUp;
    public string timeDisplay;
    public Transform respawnPoint;
    
    public bool isPaused = false;
    public bool isDead = false;

    private Animator myAnim;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        Debug.Log("PlayerInformation control is set to this instance");
        }
        else if (control != this)
        {
            Destroy(gameObject);
            Debug.Log("PlayerInformation control is destroyed");
            return;
        }

        currentHealth = maxHealth;
        score = 0;
        level = 1;
        powerUp = 0;
        timeDisplay = "00:00:000";
    }

    void Start()
    {
        myAnim = GetComponent<Animator>();

        // Respawn the player from the last level
        //RespawnPoint respawnPoint = FindObjectOfType<RespawnPoint>();
        if (respawnPoint != null)
        {
            Respawn();
            Debug.Log("Respawn on start");
        }
        else
        {
            Debug.LogWarning("No respawn point found in the scene.");
        }
           
        

    }


    void Update()
    {
       
        if (!isPaused)
        {
            timer += Time.deltaTime;
            timeDisplay = DisplayTime(timer);
            CheckHealth();
        }
        // if (Input.GetKeyDown(KeyCode.H)) {
        //     TakeDamage(10);
        // }
    }
    // This function is called when the player collects a power-up
    // todo: add a powerUp system
    public void SetPowerUp(float powerUpLevel)
    {
       this.powerUp = powerUpLevel;
        
    }

    // Used to check if player has died
    private void CheckHealth() {
        if (currentHealth <= 0 && !isDead) {
            isDead = true;
            if (score >= 50)
            {
                score -= 50;
            }
            else
            {
                score = 0;
            }
            StartCoroutine(RespawnDelay(1.4f));
        }
    }

    // Delaying player respawn
    private IEnumerator RespawnDelay(float delay) {
        myAnim.SetTrigger("Death");
        yield return new WaitForSeconds(delay);
        Respawn();
    }

    // Respawning player
    public void Respawn() {
        currentHealth = maxHealth;

        if (respawnPoint != null) 
        {
        transform.position = respawnPoint.position;
            Debug.Log("Player respawned at: " + respawnPoint.position);
            isDead = false;
            isPaused = false;
        }
        else {
            Debug.LogWarning("Need to set respawnPoint");
        }
    }

    public string DisplayTime(float timer)

    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        float milliseconds = (timer * 1000) % 1000;
        int secondsNow = Mathf.FloorToInt(seconds);
       
        string timeDisplay = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeDisplay;

    }
    // This function is called when the player is healed
    public void Heal()
    {
        currentHealth = maxHealth;
    }
    // This function is called when the player takes damage
    public void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        if (currentHealth <= 0)
        {
            this.currentHealth = 0;
            //GameOver();
        }
        myAnim.SetTrigger("Hurt");

    }
    public void SetHealth(float damage)
    {
       
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    // This function is called when the player collects a power-up
    public void SetScore(int score)
    {
        this.score += score;
    }
    public void ResetPlayerInfo()
    {
        currentHealth = maxHealth;
        score = 0;
        level = 1;
        powerUp = 0;
        timeDisplay = "00:00:000";
        timer = 0;
    }
}
