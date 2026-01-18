using TMPro;
using UnityEngine;

public class ScriptOnButton : MonoBehaviour
{
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

    private void Start()
    {
        StartHandler();
    }
    private void StartHandler()
    {
        infoText.enabled = false;
        alreadyDiscovered = false;

        if (tutorialManager == null)
        {
            FindGameObjectByName("TutorialButtonManager");
        }
    }
    public void GiveInfoToTutorialManager()
    {
            tutorialManager.ShowInfo(infoText, textInButton, autoHideDelay, alreadyDiscovered);      
    }
    private void Update()
    {
        if (attachToObject)
        {
            AttachButtonToObject();
        }
    }
    private void AttachButtonToObject()
    {
        GetComponent<Transform>().position = attachmentTarget.position + attachmentOffset;
    }
    private void FindGameObjectByName(string nameOfTheObject)
    {
        tutorialManager = GameObject.Find(nameOfTheObject).GetComponent<TutorialManager>();
    }
}