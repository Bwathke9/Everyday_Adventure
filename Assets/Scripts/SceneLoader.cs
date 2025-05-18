using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public GameObject Ethan;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Debug.Log("SceneLoader instance set to this instance");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            //Debug.Log("SceneLoader instance destroyed");
            return;
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Debug.Log("SceneLoader Awake completed");

    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var respawnPoint = FindFirstObjectByType<RespawnPoint>();

        if (respawnPoint != null)
        {

            if (PlayerInformation.control == null)
            {
                if (Ethan != null)
                {
                    GameObject player = Instantiate(Ethan, respawnPoint.transform.position, respawnPoint.transform.rotation);
                    DontDestroyOnLoad(player);
                    var info = player.GetComponent<PlayerInformation>();
                    PlayerInformation.control = info;
                    info.respawnPoint = respawnPoint.transform;
                    info.Respawn();
                    info.currentHealth = info.maxHealth;
                    //Debug.Log("New player instantiated");
                }
            }  
            else
            {

                PlayerInformation.control.respawnPoint = respawnPoint.transform;
                //Debug.Log("Respawn point set to: " + respawnPoint.transform.position);
                PlayerInformation.control.transform.position = respawnPoint.transform.position;
                //PlayerInformation.control.respawnPoint = respawnPoint.transform;
            }        
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetGame()
    {
        if (PlayerInformation.control != null)
        {
            PlayerInformation.control.ResetPlayerInfo();
            PlayerInformation.control.isDead = false;
            PlayerInformation.control.isPaused = false;
            Time.timeScale = 1;
            PlayerInformation.control.transform.position = PlayerInformation.control.respawnPoint.position;
            UnityEditor.EditorApplication.isPlaying = true;
        }       
        
    }
}

