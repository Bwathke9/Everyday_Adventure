// Brennan Wathke 4/19/2025 Server Connection Tests
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameDataControllerTests : MonoBehaviour
{
    private GameDataController gameDataController;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject();
        gameDataController = go.AddComponent<GameDataController>();
    }

    [UnityTest]
    public IEnumerator TestPostGameData()
    {
        string level = "level_one";
        string end_time = "2025-01-01 12:00:00";
        string level_time = "00:02:00";
        int level_score = 100;
        int questions_answered = 5;
        string initials = "ABC";

        bool isSuccess = false;
        string resultMessage = "";

        gameDataController.OnPostGameDataResult += (success, message) =>
        {
            isSuccess = success;
            resultMessage = message;
        };

        gameDataController.SubmitGameData(level, end_time, level_time, level_score, questions_answered, initials);
        
        yield return new WaitUntil(() => !string.IsNullOrEmpty(resultMessage));
        Assert.IsTrue(isSuccess, $"POST request to submit game data failed: {resultMessage}");
    }

    [UnityTest]
    public IEnumerator TestGetGameData()
    {
        string level = "level_one";
        bool isSuccess = false;
        string resultMessage = "";

        gameDataController.OnGetGameDataResult += (success, message) =>
        {
            isSuccess = success;
            resultMessage = message;
        };

        gameDataController.GetGameData(level);
        
        yield return new WaitUntil(() => !string.IsNullOrEmpty(resultMessage));
        Assert.IsTrue(isSuccess, $"GET request to retrieve game data failed: {resultMessage}");
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(gameDataController.gameObject);
    }
}