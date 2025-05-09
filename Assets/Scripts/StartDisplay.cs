// By Adam Nixdorf
using UnityEngine;

public class StartDisplay : MonoBehaviour
{
    private string startMessage = "Welcome to EVERYDAY ADVENTURE! \n To travel through the level use " +
        "D to move right, A to move left, and SPACE to jump. Collect fruit to increase your score but " +
        "watch out for obstacles along the way. Each level will have a chance for you to restore your health." +
        "Levels 1&2 will have a mini game at the end for a chance to earn a power-up for the next level. You will" +
        "get a bonus for getting through the levels quickly so keep an eye on that timer.";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (updateWindow.instance != null)
        {
            updateWindow.instance.DisplayUpdateWindow(startMessage);
            Debug.Log("updateWindow instance is not null" + startMessage);
        }
        else
        {
            Debug.LogError("updateWindow instance is null");
            return;
        }
        // Destroy the object after displaying the message so it doesn't show again
        Destroy(gameObject);
    }
}
