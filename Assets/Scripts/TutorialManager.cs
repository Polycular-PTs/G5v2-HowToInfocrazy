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
    [SerializeField] private KeyCode keyForNextScene = KeyCode.E;       //The key that has to be pressed to get to the next tutorialScene
    [SerializeField] private KeyCode keyForOneSceneBack = KeyCode.Q;        //The key that has to be pressed to get to the tutorialScene before the current

    [Header("TMP Pro Objects")]
    [SerializeField] TextMeshProUGUI textGeneralSceneInfo;      //The TMPro object in the scene that should have the info for the current scene in it
    
    [Header("Progress Bar Settings")]
    [SerializeField] private GameObject tutorialProgressSlider;     //The UI slider that shows how many hints of the total amount the user has already found
    [SerializeField] private float totalPointsForFullSlider;        //How many hints the user has to find to be able to start the game and have a full slider
    private int tutorialPoints;     //How many hints the user has found

    [Header("Tutorial Scenes")]
    [SerializeField] private UnityEngine.UI.Image currentSceneImage;        //The image component in the scene that gets the image for the current scene
    
    [SerializeField] private GameObject[] tutorialScenes;       //The different images that are needed for the scenes

    [SerializeField] private string[] sceneSpesificTextArray = new string[4];       //The text that explains the user something general about the scene he is in

    [Header("Butten that starts the Game")]
    [SerializeField] private GameObject buttonForStartingTheGame;       //The button the user has to press to get to the game

    private int currentSceneNumber = 0;     //The tutorial scene the user is currently in (first scene = 1)

    private TextMeshProUGUI textInButtonToChange;       //The text in every clickable hint-button that changes between: two states ("Show Info" and "Hide Info") when clicked
    private TextMeshProUGUI textMeshToDisable;      //The TMPro of every clickable hint-button that contains the info of the button that is shown when clicked

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
            textMeshInfo.GetComponentInParent<ScriptOnButton>().alreadyDiscovered = true;

            Debug.Log("already enabled: " + textMeshInfo.GetComponentInParent<ScriptOnButton>().alreadyDiscovered);
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
