using TMPro;
using UnityEngine;

public class ScriptOnButton : MonoBehaviour
{
    [Header("Needed references")]
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI textInButton;
    [SerializeField] private TextMeshProUGUI correspondingTextMeshProUGUI;

    [Header("Not nedded -> If you want to follow an other objects movement")]
    [SerializeField] private bool isButtonAttachedToOtherObject = false;
    [SerializeField] private GameObject attachmentObject;
    [SerializeField] private Vector3 attachmentOffset;

    [Header("If you have a very long text the text takes longer to auto disable")]
    [SerializeField] private float extraDelayBeforeDisable;

    public bool alreadyEnabled;

    private void Start()
    {
        correspondingTextMeshProUGUI.enabled = false;
        alreadyEnabled = false;
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
}