// Brennan Wathke 5/10/2025
using UnityEngine;
using System.Collections;
using System;

public class GameEnd : MonoBehaviour
{
    private GameDataController controller;
    public event Action<bool, string> OnSubmissionResultEvent;

    private void Awake()
    {
        controller = FindObjectOfType<GameDataController>();
        if (controller == null)
        {
            Debug.LogError("GameEnd: Failed to find GameDataController in the scene!");
        }
    }

    public void SubmitStatsWithInitials(string initials)
    {
        string formattedTime = FormatTimeForDB(PlayerInformation.control.timeDisplay);
        StartCoroutine(SubmitPlayerStatsCoroutine(initials, formattedTime));
    }

    private string FormatTimeForDB(string originalTime)
    {
        string[] timeParts = originalTime.Split(':');

        if (timeParts.Length >= 2)
        {
            string minutes = timeParts[0].PadLeft(2, '0');
            string[] secondParts = timeParts[1].Split('.');
            string seconds = secondParts[0].PadLeft(2, '0');
            string milliseconds = "00";

            if (secondParts.Length > 1)
            {
                milliseconds = secondParts[1];

                if (milliseconds.Length > 2)
                {
                    milliseconds = milliseconds.Substring(0, 2);
                }
                else if (milliseconds.Length < 2)
                {
                    milliseconds = milliseconds.PadRight(2, '0');
                }
            }
            string formattedTime = $"{minutes}:{seconds}.{milliseconds}";

            return formattedTime;
        }
        return originalTime;
    }

    private IEnumerator SubmitPlayerStatsCoroutine(string initials, string formattedTime)
    {
        if (controller != null)
        {
            bool submissionComplete = false;
            bool submissionSuccess = false;
            string submissionResult = "";

            void HandleSubmissionResult(bool success, string message)
            {
                Debug.Log($"Submission Result - Success: {success}, Message: {message}");

                OnSubmissionResultEvent?.Invoke(success, message);

                submissionComplete = true;
                submissionSuccess = success;
                submissionResult = success 
                    ? "Submission Successful" 
                    : $"Submission Failed: {message}";
            }

            controller.OnPostGameDataResult -= HandleSubmissionResult;
            controller.OnPostGameDataResult += HandleSubmissionResult;
            
            controller.SubmitGameData(
                "level_three", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                formattedTime, 
                PlayerInformation.control.score, 
                0, 
                initials
            );

            float waitTimer = 0f;
            while (!submissionComplete && waitTimer < 5f)
            {
                waitTimer += Time.deltaTime;
                yield return null;
            }

            controller.OnPostGameDataResult -= HandleSubmissionResult;

            if (submissionComplete)
            {
                if (submissionSuccess)
                {
                    Debug.Log($"GameEnd: {submissionResult}");
                }
                else
                {
                    Debug.LogError($"GameEnd: {submissionResult}");
                }
            }
            else
            {
                Debug.LogError("GameEnd: Submission timed out");
            }
        }
    }
}
