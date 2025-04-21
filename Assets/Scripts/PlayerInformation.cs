using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    public PlayerInformation control;
    public int currentHealth;
    public int maxHealth = 5;
    public int score;
    public int level;
    public float powerUp;
    public float timer;
    private GUIStyle titlePage = new GUIStyle();
    public Sprite fullHeart;
    public Sprite emptyHeart;



    void Awake ()
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
        
    }
    void Start()
    {
        currentHealth = maxHealth;
        score = 0;
        
    }
    private void OnGUI()
    {
       

        titlePage.fontSize = 50;
        titlePage.normal.textColor = Color.red;
        titlePage.fontStyle = FontStyle.Bold;
        titlePage.alignment = TextAnchor.UpperCenter;
        for (int i = 0; i <= maxHealth; i++)
        {
            if (i < currentHealth)
            {
                GUI.DrawTexture(new Rect(50 + (i * 15), 10, 40, 40), fullHeart.texture);
            }
            else if (i >= currentHealth)
            {
                GUI.DrawTexture(new Rect(20 + (i * 10), 10, 40, 40), emptyHeart.texture);
            }
        }
        GUI.Label(new Rect(10, 10, 100, 50), "Health: " );
        GUI.Label(new Rect(10, 30, 100, 50), "Score: " + score);
        GUI.Label(new Rect(10, 50, 100, 50), "Power-Up: " + score);
        GUI.Label(new Rect(325, 10, 200, 100), "Everyday Adventure", titlePage);
        GUI.Label(new Rect(750, 10, 100, 50), "Level: " + level);
        GUI.Label(new Rect(750, 30, 100, 50), "Timer: " + timer);
        GUI.Button(new Rect(750, 50, 100, 40), "Pause");
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
        currentHealth = Mathf.Clamp(currentHealth, 0, 5);

       
    }

    //public void UpdateHearts()
    //{
    //    SetHeartContainers();
    //    SetFilledHearts();
    //}

    //private void SetHeartContainers()
    //{
    //    for (int i = 0; i < heartContainers.Length; i++)
    //    {
    //        if (i < 5)
    //        {
    //            heartContainers[i].SetActive(true);
    //        }
    //        else
    //        {
    //            heartContainers[i].SetActive(false);
    //        }
    //    }
    //}
    //private void SetFilledHearts()
    //{
    //    for (int i = 0; i < heartFills.Length; i++)
    //    {
    //        if (i < currentHealth)
    //        {
    //            heartFills[i].fillAmount = 1;
    //        }
    //        else
    //        {
    //            heartFills[i].fillAmount = 0;
    //        }
    //    }
    //    if (health % 1 != 0)
    //    {
    //        int lastPos = Mathf.FloorToInt(health);
    //        heartFills[lastPos].fillAmount = health - lastPos;
    //    }
    //}

  
    
}
