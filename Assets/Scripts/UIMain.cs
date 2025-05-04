// By Adam Nixdorf
// This script is designed to display player information

using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
    //assign the names of the UI elements in the UI Builder
    private string healthDisplayName = "healthDisplay";    
    private string powerUpLabelName = "powerUpDisplay";
    private string scoreLabelName = "scoreDisplay";
    private string levelLabelName = "levelDisplay";
    private string timerDisplayName = "timerDisplay";
    private string pauseButtonName = "pauseButton";
    private string pauseWindowName = "pauseWindow";
    private string resumeButtonName = "resumeButton";
    private string mainMenuButtonName = "mainMenuButton";

    //assign variables to the UI elements
    private ProgressBar healthDisplay;    
    private ProgressBar powerUpDisplay;
    private VisualElement scoreDisplay;
    private VisualElement levelDisplay;
    private VisualElement timerDisplay;
    private Button pauseButton;
    

    private Label scoreOut;
    private Label levelOut;
    private Label timerOut;

    //assign variables of the pause window elements in the UI Builder
    private VisualElement pauseWindow;
    private Button resumeButton;
    private Button mainMenuButton;

    VisualElement leftVisualElement;
    VisualElement rightVisualElement;  

    [NonSerialized] private UIDocument uIDocument;

    void Start()
    {

       
        uIDocument = GetComponent<UIDocument>();
        leftVisualElement = uIDocument.rootVisualElement;
        healthDisplay = leftVisualElement.Q<ProgressBar>(healthDisplayName);        
        powerUpDisplay = leftVisualElement.Q<ProgressBar>(powerUpLabelName);
        

        if (healthDisplay != null)
        {
            healthDisplay.value = PlayerInformation.control.currentHealth;
        }
        else
        {
            Debug.LogError("healthDisplay is null");
        }
        if (powerUpDisplay != null)
        {
            powerUpDisplay.value = PlayerInformation.control.powerUp;
        }
        else
        {
            Debug.LogError("powerUpDisplay is null");
        }
        scoreDisplay = leftVisualElement.Q<VisualElement>(scoreLabelName);

        rightVisualElement = GetComponent<UIDocument>().rootVisualElement;
        levelDisplay = rightVisualElement.Q<VisualElement>(levelLabelName);
        timerDisplay = rightVisualElement.Q<VisualElement>(timerDisplayName);
        pauseButton = rightVisualElement.Q<Button>(pauseButtonName);

        pauseWindow = uIDocument.rootVisualElement.Q<VisualElement>(pauseWindowName);
        resumeButton = pauseWindow.Q<Button>(resumeButtonName);
        mainMenuButton = pauseWindow.Q<Button>(mainMenuButtonName);
        pauseWindow.style.display = DisplayStyle.None;
        pauseButton.clicked += TogglePopUp;
        resumeButton.clicked += TogglePopUp;
        
        mainMenuButton.clicked += () => SceneManager.LoadScene("MainMenu");
        

        scoreOut = new Label();
        levelOut = new Label();
        timerOut = new Label();
        
        // Display the current level
        levelOut.text = " " + PlayerInformation.control.level;
        


        scoreDisplay.Add(scoreOut);
        levelDisplay.Add(levelOut);
        timerDisplay.Add(timerOut);
    }

    private void TogglePopUp()
    {
        // Toggle the pause window visibility
        bool isVisible = pauseWindow.style.display == DisplayStyle.Flex;
        pauseWindow.style.display = isVisible ? DisplayStyle.None : DisplayStyle.Flex;

        PauseGame.pauseResumeGame();
    }

    void Update()
    {
        if (PlayerInformation.control != null)
        {
            if (healthDisplay != null)
            {
                healthDisplay.value = PlayerInformation.control.currentHealth;                
            }

            if (powerUpDisplay != null)
            {
                powerUpDisplay.value = PlayerInformation.control.powerUp;
            }
            
            timerOut.text = " " + PlayerInformation.control.timeDisplay;
        }
        scoreOut.text = " " + PlayerInformation.control.score;
        timerOut.text = " " + PlayerInformation.control.timeDisplay;
    }

    
}
