using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    public InputActionReference closeAction;

    bool introShown = false;
    bool resourceBShown = false;
    bool trophyShown = false;

    void OnEnable()
    {
        closeAction.action.Enable();
        closeAction.action.performed += OnClosePressed;
    }

    void OnDisable()
    {
        closeAction.action.performed -= OnClosePressed;
        closeAction.action.Disable();
    }

    void Start()
    {
        ShowIntroTutorial();
    }

    public void ShowIntroTutorial()
    {
        if (introShown) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "Welcome! Use your controller to interact with objects and collect resources. Press A on your controller to close popups.";
        introShown = true;
    }

    public void ShowResourceBTutorial()
    {
        if (resourceBShown) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "New Resource Unlocked! Resource B can now be collected.";
        resourceBShown = true;
    }

    public void ShowTrophyTutorial()
    {
        if (trophyShown) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "Achievement Unlocked! You've earned a trophy for reaching this milestone.";
        trophyShown = true;
    }

    void OnClosePressed(InputAction.CallbackContext context)
    {
        if (tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(false);
        }
    }
}

