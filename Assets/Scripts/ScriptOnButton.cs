using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptOnButton : MonoBehaviour
{
    public TutorialProgressTrack tutorialProgress;

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

    public bool alreadyDiscovered;
    private bool showing;
    private float hideTimer;

    private void Awake()
    {
        infoText.enabled = false;
        alreadyDiscovered = false;
        textInButton.text = "Show Info";

        if (tutorialManager == null)
        {
            FindGameObjectByNameTutorialManager("TutorialButtonManager");
        }

        if (tutorialProgress == null) 
        {
            FindGameObjectByNameTutorialProgressTrack("TutorialButtonManager");
        }
    }

    private void Update()
    {
        if (attachToObject)
        {
            transform.position = attachmentTarget.position + attachmentOffset;
        }  

    }

    public void OnButtonPressed()
    {
        if (!showing) { Show(); }

        if (showing) { Hide(); }   
    }

    private void Show()
    {
        Debug.Log("ShowButtonInfo");
        infoText.enabled = true;
        textInButton.text = "Hide Info";
        showing = true;

        if (!alreadyDiscovered)
        {
            alreadyDiscovered = true;
            tutorialProgress.HintDiscovered();
        }

        Invoke("Hide", tutorialManager.timeToAutoDisableAButton + autoHideDelay);
    }

    private void Hide()
    {
        infoText.enabled = false;
        textInButton.text = "Show Info";
        showing = false;
    }

    private void FindGameObjectByNameTutorialManager(string nameOfTheObject)
    {
        tutorialManager = GameObject.Find(nameOfTheObject).GetComponent<TutorialManager>();
    }

    private void FindGameObjectByNameTutorialProgressTrack(string nameOfTheObject)
    {
        tutorialProgress = GameObject.Find(nameOfTheObject).GetComponent<TutorialProgressTrack>();
    }
}