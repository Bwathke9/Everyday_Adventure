using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameDataController : MonoBehaviour
{
    private string apiUrl = "https://ea1.djbean.net/api.php";

    public event System.Action<bool, string> OnPostGameDataResult;
    public event System.Action<bool, string> OnGetGameDataResult;

    // Submit data
    public void SubmitGameData(string level, string endTime, string levelTime, int levelScore, int questionsAnswered, string initials)
    {
        StartCoroutine(PostGameData(level, endTime, levelTime, levelScore, questionsAnswered, initials));
    }

    private IEnumerator PostGameData(string level, string endTime, string levelTime, int levelScore, int questionsAnswered, string initials)
    {
        string jsonData = JsonUtility.ToJson(new
        {
        level = level,
        end_time = endTime,
        level_time = levelTime,
        level_score = levelScore,
        questions_answered = questionsAnswered,
        initials = initials  
        });

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonData))
        {

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.method = UnityWebRequest.kHttpVerbPOST;

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
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl + "?level=" + level))
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