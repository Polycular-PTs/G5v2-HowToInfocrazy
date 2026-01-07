using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private float timeToAutoDisableAButton = 5f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject circle;
    [SerializeField] private float offsetOfPlayer;

    [Header("User Inputs")]
    [SerializeField] private KeyCode keyForNextScene = KeyCode.E;
    [SerializeField] private KeyCode keyForOneSceneBack = KeyCode.Q;

    private TextMeshProUGUI textInButtonToChange;
    private TextMeshProUGUI textMeshToDisable;

    [SerializeField] TextMeshProUGUI textGeneralSceneInfo;
    
    [Header("Progress Bar Settings")]
    [SerializeField] private GameObject tutorialProgressSlider;
    [SerializeField] private float totalPointsForFullSlider;
    private int tutorialPoints;

    [Header("Tutorial Scenes")]
    [SerializeField] private GameObject currentSceneImage;
    public Sprite[] imagesOfScenes;

    [Header("Corresponding Buttons for each Scene")]
    [SerializeField] private GameObject[] buttonsInScene1;
    [SerializeField] private GameObject[] buttonsInScene2;
    [SerializeField] private GameObject[] buttonsInScene3;
    [SerializeField] private GameObject[] buttonsInScene4;

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

        float xPosCirc = circle.transform.position.x;
        float yPosCirc = circle.transform.position.y;

        if (xPos < xPosCirc +offsetOfPlayer && xPos > xPosCirc -offsetOfPlayer && yPos < yPosCirc +offsetOfPlayer && yPos > yPosCirc -offsetOfPlayer)
        {
            Debug.Log("Player is in Box" + "Click E to next scene");
            if (Input.GetKeyDown(keyForNextScene))
            {
                if (currentSceneNumber < imagesOfScenes.Length)
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
            ShowInGeneralSceneInfo("transition Scene with " + keyForOneSceneBack + " and " + keyForNextScene, 12);
        }
        else
        {
            ShowInGeneralSceneInfo("Hier bekommen sie bla erklärt",12);
        }

        UpdateTutorialProgress();
        
        //Debug.Log("x= " + xPos + "y= " + yPos);
    }

    public void ShowInGeneralSceneInfo(string sceneInfoText, int fontSize)
    {
        textGeneralSceneInfo.text = "Current Scene: " + currentSceneNumber +" von "+ imagesOfScenes.Length.ToString() + "\n" + sceneInfoText;
        textGeneralSceneInfo.fontSize = 12;
    }

    private void TransitionBackwardInTutorialScnen(int sceneNumber)
    {
        Debug.Log("Transition to the Scene before");
        currentSceneImage.GetComponent<UnityEngine.UI.Image>().sprite = imagesOfScenes[sceneNumber-2];
        currentSceneNumber -= 1;

        UpdateButtonsForCorresponingScene();
    }
    private void TransitionForwardInTutorialScene(int sceneNumber)
    {
        Debug.Log("Transition to next Scene");
        currentSceneImage.GetComponent<UnityEngine.UI.Image>().sprite = imagesOfScenes[sceneNumber];
        currentSceneNumber += 1;

        UpdateButtonsForCorresponingScene();
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
        string progressText = " ";
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

    private void UpdateButtonsForCorresponingScene()
    {
        switch (currentSceneNumber)
        {
            case 1:
                Debug.Log("Switch to Buttons of Scene 1");
                foreach (GameObject obj in buttonsInScene1)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in buttonsInScene2)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene3)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene4)
                {
                    obj.SetActive(false);
                }
                break;

            case 2:
                Debug.Log("Switch to Buttons of Scene 2");
                foreach (GameObject obj in buttonsInScene2)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in buttonsInScene1)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene3)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene4)
                {
                    obj.SetActive(false);
                }
                break;

            case 3:
                Debug.Log("Switch to Buttons of Scene 3");
                foreach (GameObject obj in buttonsInScene3)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in buttonsInScene1)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene2)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene4)
                {
                    obj.SetActive(false);
                }
                break;

            case 4:
                Debug.Log("Switch to Buttons of Scene 4");
                foreach (GameObject obj in buttonsInScene4)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in buttonsInScene1)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene2)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in buttonsInScene3)
                {
                    obj.SetActive(false);
                }
                break;
        }
    }
}
