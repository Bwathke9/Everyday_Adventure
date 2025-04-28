// By Adam Nixdorf
// This script is designed to track player information
using System;
using UnityEngine;

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
    int lastSecond = -5;
    public bool isPaused = false;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        currentHealth = maxHealth;
        score = 50;
        level = 1;
        powerUp = 55;
        timeDisplay = "00:00:000";
    }


    void Update()
    {
       
        if (!isPaused)
        {
            timer += Time.deltaTime;
            timeDisplay = DisplayTime(timer);
        }

    }
    // This function is called when the player collects a power-up
    // todo: add a powerUp system
    private void SetPowerUp(int v)
    {
       float randomPower = UnityEngine.Random.Range(0f, v);
        if (powerUp > 0)
        {
            powerUp -= randomPower;
        }
        else
        {
            powerUp += randomPower;
        }
        powerUp = Mathf.Clamp(powerUp, 0, 100);
    }

    public string DisplayTime(float timer)

    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        float milliseconds = (timer * 1000) % 1000;
        int secondsNow = Mathf.FloorToInt(seconds);
        
        if (secondsNow == 35 && lastSecond != 35)
        {
            SetHealth(45);
            SetPowerUp(45);
            score += 1;
            lastSecond = 35;
        }
        else if (secondsNow == 45 && lastSecond != 45)
        {
            SetHealth(45);
            SetPowerUp(45);
            score += 1;
            lastSecond = 45;
        }
        else if (secondsNow == 55 && lastSecond != 55)
        {
            SetHealth(45);
            SetPowerUp(45);
            score += 1;
            lastSecond = 55;
        }
        else if (secondsNow == 60 && lastSecond != 60)
        {
            SetHealth(45);
            SetPowerUp(45);
            score += 1;
            lastSecond = 60;
        }
        string timeDisplay = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeDisplay;

    }
    // This function is called when the player is healed
    public void Heal()
    {
        currentHealth = maxHealth;
    }
    // This function is called when the player takes damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //GameOver();
        }
    }
    private void SetHealth(float damage)
    {
        // randomly select a number between 0 and the damage amount 
        float randomDamage = UnityEngine.Random.Range(0f, damage);
        if ( currentHealth > 0)
        {
            currentHealth -= randomDamage;
        }
        else
        {
            currentHealth += randomDamage;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    // This function is called when the player collects a power-up
    public void SetScore(int score)
    {
        this.score = score;
    }
}
