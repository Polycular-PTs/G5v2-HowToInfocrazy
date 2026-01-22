using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TutorialSceneController tutorialSceneController;

    [SerializeField] public float timeToAutoDisableAButton = 5f;   //After how much time the Butten changes to closed automatically

    [SerializeField] private GameObject player;     //The figure the user can intercat with and controll
    [SerializeField] private GameObject placeOfSceneTransition;     //The GameObject that is at the position the plyer has to be to change the TutorialScene
    [SerializeField] private float offsetOfPlayer;      //How far the player can be away of the position 

    [Header("User Inputs")]
    [SerializeField] private KeyCode keyForNextScene = KeyCode.E;       //The key that has to be pressed to get to the next tutorialScene
    [SerializeField] private KeyCode keyForOneSceneBack = KeyCode.Q;        //The key that has to be pressed to get to the tutorialScene before the current

    [Header("TMP Pro Objects")]
    [SerializeField] TextMeshProUGUI textGeneralSceneInfo;      //The TMPro object in the scene that should have the info for the current scene in it

    [SerializeField] private string[] sceneSpesificTextArray = new string[4];       //The text that explains the user something general about the scene he is in

    private void Update()
    {
        if (IsPlayerInTransitionArea())
        {
            //Debug.Log("Player is in Box" + "Click E to next scene");
            if (Input.GetKeyDown(keyForNextScene))
            {
                tutorialSceneController.TransitionForwardInTutorialScene();
                tutorialSceneController.ObjectsForCurrentScene();
            }
            if (Input.GetKeyDown(keyForOneSceneBack))
            {

                tutorialSceneController.TransitionBackwardInTutorialScnen();
                tutorialSceneController.ObjectsForCurrentScene();
            }
            ShowInGeneralSceneInfo("Transition Scene with " + keyForOneSceneBack + " and " + keyForNextScene);
        }
        else
        {
            ShowInGeneralSceneInfo(" ");
        }           
    }

    public void ShowInGeneralSceneInfo(string sceneInfoText)
    {
        int sceneUserNumber = tutorialSceneController.currentSceneNumber + 1;
        string generalText = "Current Scene: " + sceneUserNumber + " von "+ tutorialSceneController.tutorialScenes.Length.ToString() + "\n" + sceneInfoText;
        string sceneSpesificText = sceneSpesificTextArray[tutorialSceneController.currentSceneNumber];
        
        textGeneralSceneInfo.text = generalText + "\n" + "\n" + sceneSpesificText;
    }

    private bool IsPlayerInTransitionArea()
    {
        return Vector2.Distance(player.transform.position, placeOfSceneTransition.transform.position)
        <= offsetOfPlayer;
    }
}
