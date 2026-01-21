using TMPro;
using UnityEngine;

public class ScriptOnButton : MonoBehaviour
{
    [Header("Reference to the: TutorialProgressManagerr")]
    public TutorialProgressTrack tutorialProgressManager;

    [Header("Reference to the: TutorialButtonManager")]
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Text field inside the button")]
    [SerializeField] private TextMeshProUGUI textInButton;

    [Header("TextMeshPro that is shown when clicked")]
    [SerializeField] private TextMeshProUGUI infoText;

    [Header("Optional attachment")]
    [SerializeField] private bool attachToObject;
    [SerializeField] private Transform attachmentTarget;
    [SerializeField] private Vector3 attachmentOffset;

    [Header("Auto Hide Delay")]
    [SerializeField] private float autoHideDelay = 5f;

    private bool alreadyDiscovered;
    private bool showing;
    private float hideTimer;

    private void Awake()
    {
        infoText.enabled = false;
        alreadyDiscovered = false;
        textInButton.text = "Show Info";
        ResolveTutorialManager();
    }

    private void Update()
    {
        HandleAttachment();
        AutoHideTimer();
    }

    private void AutoHideTimer()
    {
        if (showing)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f) { Hide(); }
        }
    }

    private void HandleAttachment()
    {
        if (attachToObject)
        {
            transform.position = attachmentTarget.position + attachmentOffset;
        }
    }

    public void OnButtonPressed()
    {
        if (!showing) { Show(); return; }

        if (showing) { Hide(); }   
    }

    private void Show()
    {
        Debug.Log("ShowButtonInfo");
        infoText.enabled = true;
        textInButton.text = "Hide Info";
        showing = true;
        hideTimer = tutorialManager.timeToAutoDisableAButton + autoHideDelay;

        if (!alreadyDiscovered)
        {
            alreadyDiscovered = true;
            NotifyTutorialManager();
        }
    }

    private void Hide()
    {
        infoText.enabled = false;
        textInButton.text = "Show Info";
        showing = false;
    }

    private void NotifyTutorialManager()
    {
        tutorialProgressManager.HintDiscovered();
    }

    private void ResolveTutorialManager()
    {
        if (tutorialManager == null)
        {
            tutorialManager = GameObject.Find("TutorialButtonManager").GetComponent<TutorialManager>();
        }

        if (tutorialProgressManager == null)
        {
            tutorialProgressManager = GameObject.Find("TutorialButtonManager").GetComponent<TutorialProgressTrack>();
        }
    }
}