// By Adam Nixdorf
using UnityEngine;

public class StartDisplay : MonoBehaviour
{
    private string startMessage = "Welcome to EVERYDAY ADVENTURE! \n To travel through the levels with Ethan use " +
        "D to move right, A to move left, and SPACE to jump. Ethan has Ninja abilities and can jump at times" +
        "when he is hanging onto the edge. Collect fruit to increase your score but " +
        "watch out for obstacles along the way. Watch for hearts, they will give you a chance for you to " +
        "restore your health. Levels 1&2 will have a mini game at the end for a chance to earn Bonus points. " +
        "Enter your initials at the end of level 3 and see if you make our to 10 leader board. Compare your time " +
        "and score with the top 10 and see where you stack up.";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (updateWindow.instance != null)
        {
            updateWindow.instance.DisplayUpdateWindow(startMessage);
            //Debug.Log("updateWindow instance is not null" + startMessage);
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
