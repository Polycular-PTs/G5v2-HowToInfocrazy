using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private float timeToAutoDisableAButton = 5f;   //After how much time the Butten changes to closed automatically

    [SerializeField] private GameObject player;     //The figure the user can intercat with and controll
    [SerializeField] private GameObject placeOfSceneChange;     //The GameObject that is at the position the plyer has to be to change the TutorialScene
    [SerializeField] private float offsetOfPlayer;      //How far the player can be away of the position 

    [Header("User Inputs")]
    [SerializeField] private KeyCode keyForNextScene = KeyCode.E;
    [SerializeField] private KeyCode keyForOneSceneBack = KeyCode.Q;

    private TextMeshProUGUI textInButtonToChange;
    private TextMeshProUGUI textMeshToDisable;

    [Header("TMP Pro Objects")]
    [SerializeField] TextMeshProUGUI textGeneralSceneInfo;
    
    [Header("Progress Bar Settings")]
    [SerializeField] private GameObject tutorialProgressSlider;
    [SerializeField] private float totalPointsForFullSlider;
    private int tutorialPoints;

    [Header("Tutorial Scenes")]
    [SerializeField] private UnityEngine.UI.Image currentSceneImage;
    
    [SerializeField] private GameObject[] tutorialScenes;

    [SerializeField] private string[] sceneSpesificTextArray = new string[4];

    [Header("Butten that starts the Game")]
    [SerializeField] private GameObject buttonForStartingTheGame;

    private int currentSceneNumber = 0;

    private void Start()
    {
        buttonForStartingTheGame.SetActive(false);

        currentSceneNumber = 0;
        TransitionForwardInTutorialScene(currentSceneNumber);

        UpdateTutorialProgress();
    }

    private void Update()
    {
        Debug.Log("CurrentSceneNumber" + currentSceneNumber);
        float xPos = player.transform.position.x;
        float yPos = player.transform.position.y;

        float xPosCirc = placeOfSceneChange.transform.position.x;
        float yPosCirc = placeOfSceneChange.transform.position.y;

        if (xPos < xPosCirc +offsetOfPlayer && xPos > xPosCirc -offsetOfPlayer && yPos < yPosCirc +offsetOfPlayer && yPos > yPosCirc -offsetOfPlayer)
        {
            Debug.Log("Player is in Box" + "Click E to next scene");
            if (Input.GetKeyDown(keyForNextScene))
            {
                if (currentSceneNumber < tutorialScenes.Length)
                {
                    TransitionForwardInTutorialScene(currentSceneNumber);
                }
            }
            if (Input.GetKeyDown(keyForOneSceneBack))
            {
                if (currentSceneNumber > 1)
                {
                    TransitionBackwardInTutorialScnen(currentSceneNumber);
                }
            }
            ShowInGeneralSceneInfo("Transition Scene with " + keyForOneSceneBack + " and " + keyForNextScene);
        }
        else
        {
            ShowInGeneralSceneInfo("");
        }

        UpdateTutorialProgress();
    }

    public void ShowInGeneralSceneInfo(string sceneInfoText)
    {
        string generalText = "Current Scene: " + currentSceneNumber + " von "+ tutorialScenes.Length.ToString() + "\n" + sceneInfoText;
        string sceneSpesificText = sceneSpesificTextArray[currentSceneNumber-1];
        
        textGeneralSceneInfo.text = generalText + "\n" + "\n" + sceneSpesificText;
    }

    private void TransitionBackwardInTutorialScnen(int sceneNumber)
    {
        Debug.Log("Transition to the Scene before");
        currentSceneNumber -= 1;

        ObjectsForCorresponingScene();
    }
    private void TransitionForwardInTutorialScene(int sceneNumber)
    {
        Debug.Log("Transition to next Scene");
        currentSceneNumber += 1;

        ObjectsForCorresponingScene();
    }

    public void ShowInfo(TextMeshProUGUI textMeshInfo, TextMeshProUGUI textInButton, float extraDelayForLongText, bool alreadyEnabled)
    {
        if (textMeshInfo.enabled == false)
        {
            textMeshInfo.enabled = true;
            textInButton.text = "Hide Info";

            if (alreadyEnabled == false)
            {
                tutorialPoints += 1;
            }
            textMeshInfo.GetComponentInParent<ScriptOnButton>().alreadyEnabled = true;

            Debug.Log("already enabled: " + textMeshInfo.GetComponentInParent<ScriptOnButton>().alreadyEnabled);
            Debug.Log("TutorialPoints: " + tutorialPoints);
            Debug.Log("Text Object was enabled");
        }
        else
        {
            textMeshInfo.enabled = false;
            textInButton.text = "Show Info";
            Debug.Log("Text Object was disabled");
        }

        textInButtonToChange = textInButton;
        textMeshToDisable = textMeshInfo;

        Invoke("DisableText", timeToAutoDisableAButton + extraDelayForLongText);
    }

    private void DisableText()
    {
        if (textMeshToDisable.enabled == true)
        {
            textMeshToDisable.enabled = false;
            textInButtonToChange.text = "Show Info";
            Debug.Log("Text Object was automatically disabled");
        }
    }

    private void UpdateTutorialProgress()
    {
        string progressText = "";
        float sliderValue = 0f;
        if(tutorialPoints < totalPointsForFullSlider)
        {
            sliderValue = tutorialPoints / totalPointsForFullSlider;
            progressText = "You have found " + tutorialPoints + " of " + totalPointsForFullSlider + " hints";
        }
        if(tutorialPoints >= totalPointsForFullSlider)
        {
            sliderValue = 1;
            progressText = "You have found all the hints. You can now start the game:";

            buttonForStartingTheGame.SetActive(true);
        }
        tutorialProgressSlider.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
        tutorialProgressSlider.GetComponentInChildren<TextMeshProUGUI>().text = progressText;
    }

    private void ObjectsForCorresponingScene()
    {
        foreach (GameObject scenes in tutorialScenes)
        {
            if (scenes != null)
            {
                scenes.SetActive(false);
            }      
        }
        if (tutorialScenes[currentSceneNumber - 1] != null)
        {
            tutorialScenes[currentSceneNumber - 1].SetActive(true);                 
        }

        currentSceneImage.sprite = tutorialScenes[currentSceneNumber - 1].GetComponentInChildren<UnityEngine.UI.Image>().sprite;
    }
}
