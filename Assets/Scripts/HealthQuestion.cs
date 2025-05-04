// By Adam Nixdorf

// This script is designed to supply and display a restore 
// health question in the level.

using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;


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
    private Button buttonA;
    private Button buttonB;
    private Button buttonC;
    private Button buttonD;
    private VisualElement healthVisualElement;
    [SerializeField] private UIDocument healthQuiz;

    // list of questions and answers

    public List<Question> questions = new List<Question>();

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
        challengeQuestion = healthVisualElement.Q<Label>(challengeQuestionName);
        buttonA = healthVisualElement.Q<Button>(answerAName);
        buttonB = healthVisualElement.Q<Button>(answerBName);
        buttonC = healthVisualElement.Q<Button>(answerCName);
        buttonD = healthVisualElement.Q<Button>(answerDName);
        LoadQuestions();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        PauseGame.pauseResumeGame();
        
        SetQuestion();
        Debug.Log("Collision detected with " + collision.gameObject.name);
        ToggleHealtChallenge();
    }

    private void ToggleHealtChallenge()
    {
        bool isVisible = healthVisualElement.style.display == DisplayStyle.Flex;
        healthVisualElement.style.display = isVisible ? DisplayStyle.None : DisplayStyle.Flex;
    }
    private void LoadQuestions()
    {
        // Load questions from a JSON file questions
        TextAsset jsonFile = Resources.Load<TextAsset>("questions");
        if (jsonFile == null)
        {
            Debug.LogError("No .json file found.");
            return;
        }
        Debug.Log("JSON file found: " + jsonFile.name);
        // Deserialize the JSON file into a QuestionList object
        Question[] questionArray =JsonHelper.FromJson<Question>(jsonFile.text);
        // Convert the array to a list
        for (int i = 0; i < questionArray.Length; i++)
        {
            Debug.Log("Question " + i + ": " + questionArray[i].questionText);
        }
        questions = new List<Question>(questionArray);

    }
    private void SetQuestion()
    {
        

        string questionText;
        string answerA;
        string answerB;
        string answerC;
        string answerD;
        int correctAnswerIndex;
        // get a random question from the list
        int randomIndex = UnityEngine.Random.Range(0, questions.Count);
        Debug.Log("Random index: " + randomIndex);
        Question question = questions[randomIndex];
        Debug.Log("Question: " + question.questionText);
        questionText = question.questionText;
        answerA = question.answerA;
        answerB = question.answerB;
        answerC = question.answerC;
        answerD = question.answerD;
        correctAnswerIndex = question.correctAnswerIndex;

        // Set the question text
        challengeQuestion.text = questionText;
        // Set the answer button texts
        buttonA.text = answerA;
        buttonB.text = answerB;
        buttonC.text = answerC;
        buttonD.text = answerD;
        // Add click event listeners to the buttons 
        buttonA.RegisterCallback<ClickEvent>(e => OnAnswerSelected("A"));
        buttonB.RegisterCallback<ClickEvent>(e => OnAnswerSelected("B"));
        buttonC.RegisterCallback<ClickEvent>(e => OnAnswerSelected("C"));
        buttonD.RegisterCallback<ClickEvent>(e => OnAnswerSelected("D"));


    }

    private void OnAnswerSelected(string v)
    {
        if (v == "A")
        {
            // Correct answer
            Debug.Log("Correct answer selected: " + v);
            // Add code to restore health here
            PlayerInformation.control.Heal();
        }
        else
        {
            // Incorrect answer
           Debug.Log("Incorrect answer selected: " + v);
            // Add code to handle incorrect answer here
        }
        healthVisualElement.style.display = DisplayStyle.None;
        PauseGame.pauseResumeGame();
    }
}
