// By Adam Nixdorf

// This script is designed to supply and display a restore 
// health question in the level.

using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HealthQuestion : MonoBehaviour
{
    // assign the names of the UI elements in the UI Builder
    private string challengeQuestionName = "challengeQuestion";
    private string answerAName = "answerAButton";
    private string answerBName = "answerBButton";
    private string answerCName = "answerCButton";
    private string answerDName = "answerDButton";
    private string healthVisualElementName = "healthVisualElement";

    // assign variables to the UI elements
    private Label challengeQuestion;
    private Button answerA;
    private Button answerB;
    private Button answerC;
    private Button answerD;
    private VisualElement healthVisualElement;
    [SerializeField] private UIDocument healthQuiz;

    private void Start()
    {
        if (healthQuiz == null)
        {
            healthQuiz = GetComponent<UIDocument>();
        }
        if (healthQuiz == null)
        {
            Debug.LogError("HealthQuiz UIDocument is not assigned.");
            return;
        }
        healthVisualElement = healthQuiz.rootVisualElement.Q<VisualElement>(healthVisualElementName);
        //challengeQuestion = healthVisualElement.Q<Label>(challengeQuestionName);
        //answerA = healthVisualElement.Q<Button>(answerAName);
        //answerB = healthVisualElement.Q<Button>(answerBName);
        //answerC = healthVisualElement.Q<Button>(answerCName);
        //answerD = healthVisualElement.Q<Button>(answerDName);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
        ToggleHealtChallenge();
    }

    private void ToggleHealtChallenge()
    {
        bool isVisible = healthVisualElement.style.display == DisplayStyle.Flex;
        healthVisualElement.style.display = isVisible ? DisplayStyle.None : DisplayStyle.Flex;
    }
}
