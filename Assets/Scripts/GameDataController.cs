// Brennan Wathke 4/19/2025 Server Connection Script
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GameDataController : MonoBehaviour
{
    private string apiUrl = "https://ea1.djbean.net/api.php";
    private string key = "hash";

    public event System.Action<bool, string> OnPostGameDataResult;
    public event System.Action<bool, string> OnGetGameDataResult;

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
}