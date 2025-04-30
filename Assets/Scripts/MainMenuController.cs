using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void MainMenuStartGame()
    {
        SceneManager.LoadScene("Level1");
    }


    public void MainMenuLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void MainMenuHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame () {
        Application.Quit();
    }

}