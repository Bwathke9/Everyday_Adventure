using UnityEngine;

public class PauseGame
{
    public static void pauseResumeGame()
    {
        // Check if the game is paused
        // If it is, unpause it and set the time scale to 1
        // If it isn't, pause it and set the time scale to 0
        if (PlayerInformation.control.isPaused == true)
        {
            PlayerInformation.control.isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            PlayerInformation.control.isPaused = true;
            Time.timeScale = 0;
        }
    }
}
