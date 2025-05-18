//Script by Derek Wolfe
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void MainMenuStartGame()
    {
        if (PlayerInformation.control != null)
        {
            PlayerInformation.control.isPaused = false;
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Level1");
        
    }

    public void MainMenuHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void ReturnToMainMenu()
    {
        if (PlayerInformation.control != null)
        {
           SceneLoader.instance.ResetGame();
        }
        SceneManager.LoadScene("MainMenu");

    }

    public void ExitGame () {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}