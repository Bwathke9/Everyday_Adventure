// By Adam Nixdorf
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class updateWindow : MonoBehaviour
{
    //assign the names of the UI elements in the UI Builder
    public static updateWindow instance;
    private string updateInfoWindowName = "updateInfoWindow";
    private string updateVisElementName = "updateVisElement";
    private string updateLabelName = "updateLabel";
    private string continueButtonName = "continueButton";

    //assign variables to the UI elements
    private string updateLabelText;
    private Label updateLabel;
    private Button continueButton;
    private VisualElement updateVisElement;
    private VisualElement updateInfoWindow;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        updateInfoWindow = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>(updateInfoWindowName);
        updateVisElement = updateInfoWindow.Q<VisualElement>(updateVisElementName);
        updateLabel = updateVisElement.Q<Label>(updateLabelName);
        continueButton = updateInfoWindow.Q<Button>(continueButtonName);
    }
    // call this function to display the update window make sure to pass in the text you want to display.
    internal void DisplayUpdateWindow(string displayText)
    {
        if (PlayerInformation.control.isPaused == false)
        {
            PauseGame.pauseResumeGame();
        }

        updateInfoWindow.style.display = DisplayStyle.Flex; // Show the update info window

        //Debug.Log("Displaying update window with text: " + displayText);

        if (updateLabel == null)
        {
            Debug.LogError("updateLabel is null");
        }
        if (updateInfoWindow == null)
        {
            Debug.LogError("updateInfoWindow is null");
        }
        if (continueButton == null)
        {
            Debug.LogError("continueButton is null");
        }
        if (updateVisElement == null)
        {
            Debug.LogError("updateVisElement is null");
        }
        if (updateLabel == null || updateInfoWindow == null || continueButton == null)
        {
            return; // Exit if any of the elements are null
        }
        updateLabel.text = displayText;

        continueButton.clicked += () =>
        {
            updateInfoWindow.style.display = DisplayStyle.None;


            if (PlayerInformation.control.isPaused == true)
            {
                PauseGame.pauseResumeGame();
            }
        };
    }
}

    

