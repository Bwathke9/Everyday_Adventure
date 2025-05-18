//Brennan Wathke 5/08/2025 Handles the RPS minigame scene
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RPSGame : MonoBehaviour
{
    public Text resultText;
    public Text playerRounds;
    public Text CPURounds;
    public Text currentRound;
    public Text CPUChoiceText;
    public Button rockButton;
    public Button paperButton;
    public Button scissorsButton;
    public Button continueButton;

    private int playerScore = 0;
    private int CPUScore = 0;
    private int rounds = 0;

    void Start()
    {
        rockButton.onClick.AddListener(() => PlayerChoice("Rock"));
        paperButton.onClick.AddListener(() => PlayerChoice("Paper"));
        scissorsButton.onClick.AddListener(() => PlayerChoice("Scissors"));

        ResetGame();
    }

    private void ResetGame()
    {
        playerScore = 0;
        CPUScore = 0;
        rounds = 0;
        playerRounds.text = "0";
        CPURounds.text = "0";
        currentRound.text = rounds + "/3";
        CPUChoiceText.text = "";

        UpdateScoreRounds();

        resultText.text = "Choose Rock, Paper, or Scissors!";
    }

    private void PlayerChoice(string playerChoice) 
    {
        if (rounds < 3)
        {
            string CPUChoice = GetCPUChoice();
            CPUChoiceText.text = CPUChoice;
            string result = DetermineWinner(playerChoice, CPUChoice);
            resultText.text = result;

            if (result == "You win!")
            {
                playerScore++;
            }
            else if (result == "CPU wins!")
            {
                CPUScore++;
            }

            UpdateScoreRounds();

            if (rounds == 3)
            {
                EndGame();
            }
        }

    }

    private void UpdateScoreRounds()
    {
        playerRounds.text = playerScore.ToString();
        CPURounds.text = CPUScore.ToString();
        currentRound.text = rounds + "/3";
    }

    private string GetCPUChoice()
    {
        int randomValue = Random.Range(0, 3);
        switch (randomValue)
        {
            case 0: return "Rock";
            case 1: return "Paper";
            case 2: return "Scissors";
            default: return "Rock";
        }
    }

    private string DetermineWinner(string playerChoice, string CPUChoice)
    {
        if (playerChoice == CPUChoice)
            return "It's a tie!";
        
        if ((playerChoice == "Rock" && CPUChoice == "Scissors") ||
            (playerChoice == "Paper" && CPUChoice == "Rock") ||
            (playerChoice == "Scissors" && CPUChoice == "Paper"))
        {
            rounds++;
            return "You win!";
        }
        rounds++;
        return "CPU wins!";
    }

    private void EndGame()
    {
        if (playerScore > CPUScore)
        {
            resultText.text = "You won the best of three! +200 Points";
            PlayerInformation.control.score = PlayerInformation.control.score + 200;
        }

        else if (CPUScore > playerScore)
        {
            resultText.text = "CPU won the best of three!";
        }
        else
        {
            resultText.text = "The game ended in a tie!";
        }

        rockButton.interactable = false;
        paperButton.interactable = false;
        scissorsButton.interactable = false;

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(ContinueButtonClicked);
    }

    private void ContinueButtonClicked()
    {
      SceneManager.LoadScene("Level3");
    }

}
