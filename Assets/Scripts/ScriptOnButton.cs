using TMPro;
using UnityEngine;

public class ScriptOnButton : MonoBehaviour
{
    [Header("Reference to the: TutorialButtonManager")]
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Text field1: TextMeshPro inside the button")]
    [SerializeField] private TextMeshProUGUI textInButton;
    [Header("Text field2: TextMeshPro that is shown when clicked")]
    [SerializeField] private TextMeshProUGUI correspondingTextMeshProUGUI;

    [Header("Only needed if you want to follow an other objects movement")]
    [SerializeField] private bool isButtonAttachedToOtherObject = false;
    [SerializeField] private GameObject attachmentObject;
    [SerializeField] private Vector3 attachmentOffset;

    [Header("Time the text takes longer to auto disable")]
    [SerializeField] private float extraDelayBeforeDisable;

    [Header("The butten has already been used?")]
    public bool alreadyEnabled;

    private void Start()
    {
        StartHandler();
    }
    private void StartHandler()
    {
        correspondingTextMeshProUGUI.enabled = false;
        alreadyEnabled = false;

        if (tutorialManager == null)
        {
            FindGameObjectByName("TutorialButtonManager");
        }
    }
    public void GiveInfoToTutorialManager()
    {
            tutorialManager.ShowInfo(correspondingTextMeshProUGUI, textInButton, extraDelayBeforeDisable, alreadyEnabled);      
    }
    private void Update()
    {
        if (isButtonAttachedToOtherObject)
        {
            AttachButtonToObject();
        }
    }
    private void AttachButtonToObject()
    {
        GetComponent<Transform>().position = attachmentObject.GetComponentInParent<Transform>().position + attachmentOffset;
    }
    private void FindGameObjectByName(string nameOfTheObject)
    {
        tutorialManager = GameObject.Find(nameOfTheObject).GetComponent<TutorialManager>();
    }
}