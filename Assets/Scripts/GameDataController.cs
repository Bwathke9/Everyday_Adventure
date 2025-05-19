// Brennan Wathke 4/19/2025 Server Connection Script
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    private string apiUrl = "https://ea1.djbean.net/api.php";
    private string key = "hash";
    public Text scoreList;

    public event System.Action<bool, string> OnPostGameDataResult;
    public event System.Action<bool, string> OnGetGameDataResult;

    [System.Serializable]
    public class ScoreEntry
    {
    public string initials;
    public string level_time;
    public int level_score;
    }

    private void Start() 
    {
        GetTopScores();
    }

    private void GetTopScores() 
    {
        StartCoroutine(LoadTopScores());
    }

    public void GetTopShortestTimes() 
    {

        StartCoroutine(LoadTopShortestTimes());
    }

    // Create hashed key
    private string CreateHash(string data)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // Submit data
    public void SubmitGameData(string level, string endTime, string levelTime, int levelScore, int questionsAnswered, string initials)
    {
        StartCoroutine(PostGameData(level, endTime, levelTime, levelScore, questionsAnswered, initials));
    }

    private IEnumerator PostGameData(string level, string endTime, string levelTime, int levelScore, int questionsAnswered, string initials)
    {
        string apiKey = CreateHash(key + level + endTime);

        var dataLoad = new
            {
                level = level,
                apiKey = apiKey,
                data = new
                {
                    end_time = endTime,
                    level_time = levelTime,
                    level_score = levelScore,
                    questions_answered = questionsAnswered,
                    initials = initials 
                }
            };

            string jsonData = JsonConvert.SerializeObject(dataLoad);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            Debug.Log($"Request Result: {www.result}");
            Debug.Log($"Response Code: {www.responseCode}");

            try
            {
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + www.error);
                    OnPostGameDataResult?.Invoke(false, www.error);
                }
                else
                {
                    Debug.Log("Response: " + www.downloadHandler.text);
                    OnPostGameDataResult?.Invoke(true, www.downloadHandler.text);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Exception in PostGameData: {ex.Message}");
                OnPostGameDataResult?.Invoke(false, ex.Message);
            }
        }
    }

    // Retrieve data
    public void GetGameData(string level)
    {
        StartCoroutine(LoadGameData(level));
    }

    private IEnumerator LoadGameData(string level)
    {
        string apiKey = CreateHash(key + level);
        
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl + "?level=" + level + "&apiKey=" + apiKey))
        {
            
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                OnGetGameDataResult?.Invoke(false, www.error);
            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text);
                OnGetGameDataResult?.Invoke(true, www.downloadHandler.text);
            }
        }
    }

    // Retrieve top 10 scores
    private IEnumerator LoadTopScores() {

    string apiKey = CreateHash(key + "level_three");
    string apiUrlWithEndpoint = apiUrl + "?endpoint=topScores&apiKey=" + apiKey;

    using (UnityWebRequest www = UnityWebRequest.Get(apiUrlWithEndpoint)) {

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error: " + www.error);
            OnGetGameDataResult?.Invoke(false, www.error);

        } else {
            Debug.Log("Received: " + www.downloadHandler.text);
            OnGetGameDataResult?.Invoke(true, www.downloadHandler.text);
            DisplayTopScores(www.downloadHandler.text);
            }
        }
    }

        private void DisplayTopScores(string jsonResponse)
    {
        var topScores = JsonConvert.DeserializeObject<List<ScoreEntry>>(jsonResponse);
        StringBuilder scoreListBuilder = new StringBuilder();

        foreach (var score in topScores)
        {
            scoreListBuilder.AppendLine($"{score.initials} Time: {score.level_time} Score: {score.level_score}");
        }

        if (scoreList != null)
        {
            scoreList.text = scoreListBuilder.ToString();
        }
    }

    // Retrieve 10 shortest times
    private IEnumerator LoadTopShortestTimes() 
    {
        string apiKey = CreateHash(key + "level_three");
        string apiUrlWithEndpoint = apiUrl + "?endpoint=topShortestTimes&apiKey=" + apiKey;

        using (UnityWebRequest www = UnityWebRequest.Get(apiUrlWithEndpoint)) 
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.LogError("Error: " + www.error);
                OnGetGameDataResult?.Invoke(false, www.error);
            } 
            else 
            {
                Debug.Log("Received: " + www.downloadHandler.text);
                OnGetGameDataResult?.Invoke(true, www.downloadHandler.text);
                DisplayTopScores(www.downloadHandler.text);
            }
        }
    }

    public void SubmitPlayerStats()
    {
        string level = "level_three";
        string endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string levelTime = PlayerInformation.control.timeDisplay;
        int levelScore = PlayerInformation.control.score;
        int questionsAnswered = 0;
        string initials = "AAA";

        SubmitGameData(level, endTime, levelTime, levelScore, questionsAnswered, initials);
    }

    public void OnHighScoresButtonPressed()
    {
        GetTopScores();
    }

    public void OnShortestTimesButtonPressed()
    {
        GetTopShortestTimes();
    }
}