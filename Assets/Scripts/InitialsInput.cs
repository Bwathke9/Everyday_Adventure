// Brennan Wathke 5/11/2025
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections;

public class InitialsInput : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private InputField initialsInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Text resultText;

    private GameEnd gameEndScript;

    private void Awake()
    {
        popupPanel.SetActive(false);
        
        gameEndScript = FindObjectOfType<GameEnd>();

        if (submitButton != null)
        {
            submitButton.onClick.RemoveAllListeners();
            submitButton.onClick.AddListener(SubmitInitials);
        }
        else
        {
            Debug.LogError("Submit button is not assigned!");
        }

        if (gameEndScript == null)
        {
            Debug.LogError("GameEnd script not found in the scene!");
        }
    }

    public void ShowPopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Popup panel is null!");
        }

        if (initialsInputField != null)
        {
            initialsInputField.text = "";
            initialsInputField.Select();
            initialsInputField.ActivateInputField();
        }
        else
        {
            Debug.LogError("Initials input field is null!");
        }
    }

    private void SubmitInitials()
    {
        string initials = initialsInputField.text.ToUpper();

        if (IsValidInitials(initials))
        {
            if (gameEndScript != null)
            {
                StartCoroutine(SubmitAndTransition(initials));
            }
            else
            {
                Debug.LogError("Cannot submit initials: GameEnd script not found");
            }
        }
        else
        {
            if (resultText != null)
            {
                resultText.text = "Please enter exactly 3 uppercase letters";
            }
            else
            {
                Debug.LogError("Result text is null!");
            }
        }
    }

    private IEnumerator SubmitAndTransition(string initials)
    {
        bool submissionComplete = false;

        void OnSubmissionComplete(bool success, string message)
        {
            Debug.Log($"Submission Result - Success: {success}, Message: {message}");
            submissionComplete = true;
        }

        gameEndScript.OnSubmissionResultEvent -= OnSubmissionComplete;
        gameEndScript.OnSubmissionResultEvent += OnSubmissionComplete;

        gameEndScript.SubmitStatsWithInitials(initials);

        float waitTimer = 0f;
        while (!submissionComplete && waitTimer < 10f)
        {
            waitTimer += Time.deltaTime;
            yield return null;
        }

        gameEndScript.OnSubmissionResultEvent -= OnSubmissionComplete;
        PlayerInformation.control.ResetPlayerInfo();
        SceneManager.LoadScene("Highscores");
    }

    private bool IsValidInitials(string input)
    {
        return Regex.IsMatch(input, @"^[A-Z]{3}$");
    }

    private void Update()
    {
        if (popupPanel != null && popupPanel.activeSelf)
        {
            initialsInputField.text = Regex.Replace(initialsInputField.text, @"[^A-Za-z]", "").ToUpper();
        }
    }
}