// Brennan Wathke 5/2/2025 Handles the TTT minigame scene
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TTTGame : MonoBehaviour
{
    enum Player { None, PlayerX, PlayerO }
    Player[,] board = new Player[3, 3];
    Player currentPlayer = Player.PlayerX;

    public Button[] buttons;
    public Text resultText;
    public Button continueButton;

    void Start()
    {
        ResetGame();
        resultText.text = "Make your move!";
    }

    // Handling button press actions
    public void ButtonClicked(int index)

    {
        if (board[index / 3, index % 3] != Player.None) return;

        board[index / 3, index % 3] = currentPlayer;
        buttons[index].GetComponentInChildren<Text>().text = currentPlayer == Player.PlayerX ? "X" : "O";
        
        if (CheckForWin())
        {
            resultText.text = currentPlayer == Player.PlayerX ? "You have won!" : "You have lost!";
            SetButtonsInteractable(false);
            ShowContinueButton();
            return;
        }

        if (CheckForDraw())
        {
            resultText.text = "Its a tie!";
            SetButtonsInteractable(false);
            ShowContinueButton();
            return;
        }

        currentPlayer = currentPlayer == Player.PlayerX ? Player.PlayerO : Player.PlayerX;

        if (currentPlayer == Player.PlayerO) 
        {
          resultText.text = "Opponent's turn...";
          SetButtonsInteractable(false);
          StartCoroutine(CPUMoveAfterDelay(3f));
        }
    }

    private IEnumerator CPUMoveAfterDelay(float delay)
    {
      yield return new WaitForSeconds(delay);
      MakeCPUMove();
      SetButtonsInteractable(true);
      resultText.text = "Make your move!";
    }

    // CPU picks random open space
    void MakeCPUMove()
    {
        int index;
        do
        {
            index = Random.Range(0, 9);
        } while (board[index / 3, index % 3] != Player.None);
        ButtonClicked(index);
    }

    // Checking for 3 in a row
    bool CheckForWin()

    {
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
            {
                return true;
            }
        }

        if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
            (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
        {
            return true;
        }
        return false;
    }

    // Check for full board no win
    bool CheckForDraw()

    {
        foreach (var cell in board)
        {
            if (cell == Player.None) return false;
        }
        return true;
    }

    // Clear board
    public void ResetGame()
    {
        board = new Player[3, 3];
        foreach (Button button in buttons)
        {
            button.GetComponentInChildren<Text>().text = "";
        }

        resultText.text = "";
        currentPlayer = Player.PlayerX;
    }

    private void SetButtonsInteractable(bool interactable)
    {
      foreach (Button button in buttons)
      {
        button.interactable = interactable;
      }
    }

    private void ShowContinueButton()
    {
      continueButton.gameObject.SetActive(true);
      continueButton.onClick.AddListener(ContinueButtonClicked);
    }

    private void ContinueButtonClicked()
    {
      SceneManager.LoadScene("Level2");
    }
}
