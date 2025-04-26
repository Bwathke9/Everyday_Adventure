// By Adam Nixdorf
// This script is designed to track player information
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
        SetHealth();

        if (!isPaused)
        {
            timer += Time.deltaTime;
            timeDisplay = DisplayTime(timer);
        }

    }

    public string DisplayTime(float timer)

    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        float milliseconds = (timer * 1000) % 1000;
        int secondsNow = Mathf.FloorToInt(seconds);
        int lastSecond = -5;
        if (secondsNow == 35 && lastSecond != 35)
        {
            score += 1;
            lastSecond = 35;
        }
        string timeDisplay = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeDisplay;

    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //GameOver();
        }
    }
    private void SetHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

}
