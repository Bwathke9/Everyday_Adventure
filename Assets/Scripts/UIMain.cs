using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class UIMain : MonoBehaviour
{
    // UI Element references
    private string healthDisplayName = "healthDisplay";
    private string powerUpLabelName = "powerUpDisplay";
    private string scoreLabelName = "scoreDisplay";
    private string levelLabelName = "levelDisplay";
    private string timerDisplayName = "timerDisplay";
    private string pauseButtonName = "pauseButton";
    private string pauseWindowName = "pauseWindow";
    private string resumeButtonName = "resumeButton";
    private string mainMenuButtonName = "mainMenuButton";
    private string exitButtonName = "exitGameButton";
    private string powerUpPopupName = "PowerUp_PopUp";

    // UI Element variables
    private ProgressBar healthDisplay;
    private ProgressBar powerUpDisplay;
    private VisualElement scoreDisplay;
    private VisualElement levelDisplay;
    private VisualElement timerDisplay;
    private Button pauseButton;

    private Label scoreOut;
    private Label levelOut;
    private Label timerOut;

    // Pause window UI elements
    private VisualElement pauseWindow;
    private Button resumeButton;
    private Button mainMenuButton;
    private Button exitGameButton;

    VisualElement leftVisualElement;
    VisualElement rightVisualElement;

    [NonSerialized] public UIDocument uIDocument;

    // PowerUp Popup
    private VisualElement powerUpPopup;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        leftVisualElement = uIDocument.rootVisualElement;

        // Initialize the PowerUp Popup
        powerUpPopup = uIDocument.rootVisualElement.Q<VisualElement>(powerUpPopupName);
        if (powerUpPopup != null)
        {
            powerUpPopup.style.display = DisplayStyle.None; // Ensure popup is initially hidden
        }
        else
        {
            Debug.LogError("PowerUp_PopUp is null");
        }

        // Initialize other UI elements
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
        exitGameButton = pauseWindow.Q<Button>(exitButtonName);
        pauseWindow.style.display = DisplayStyle.None;
        pauseButton.clicked += TogglePopUp;
        resumeButton.clicked += TogglePopUp;
        
        mainMenuButton.clicked += () => SceneManager.LoadScene("MainMenu");
        exitGameButton.clicked += ExitGame;

        scoreOut = new Label();
        levelOut = new Label();
        timerOut = new Label();

        // Display the current level
        levelOut.text = " " + PlayerInformation.control.level;

        scoreDisplay.Add(scoreOut);
        levelDisplay.Add(levelOut);
        timerDisplay.Add(timerOut);
    }

    // Modify this method to show the popup when the power-up is triggered
    public void ShowPowerUpPopup()
    {
        if (powerUpPopup != null)
        {
            powerUpPopup.style.display = DisplayStyle.Flex; // Show the PowerUp popup
            StartCoroutine(HidePowerUpPopupAfterDelay(5f)); // Hide after 5 seconds
        }
    }

    // Coroutine to hide the PowerUp Popup after 5 seconds
    private IEnumerator HidePowerUpPopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (powerUpPopup != null)
        {
            powerUpPopup.style.display = DisplayStyle.None; // Hide the PowerUp popup
        }
    }

    // Trigger Power-Up Popup when picked up
    public void OnPowerUpPickedUp()
    {
        // If power-up picked up successfully, show the popup
        ShowPowerUpPopup(); 

        // Optionally, you can add additional logic to destroy or deactivate the power-up object here.
        // For example:
        // Destroy(powerUpObject); // If you need to destroy the power-up object
        // Or disable the power-up visually if needed (e.g., deactivate the power-up object in the scene).
        StartCoroutine(DestroyPowerUpAfterPopup());
    }

    // Coroutine to destroy the power-up after the popup is hidden
    private IEnumerator DestroyPowerUpAfterPopup()
    {
        // Wait for the popup to disappear
        yield return new WaitForSeconds(5f);

        // Destroy or deactivate the power-up object
        // Destroy(powerUpObject); // Example: Destroy the power-up after it has been collected
        // Or disable the power-up object:
        // powerUpObject.SetActive(false);

        // After the power-up is handled, you can also hide the popup manually if needed
        powerUpPopup.style.display = DisplayStyle.None;
    }

    // Pause functionality
    private void TogglePopUp()
    {
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

    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }  
}