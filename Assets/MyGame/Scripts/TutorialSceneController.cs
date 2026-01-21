using UnityEngine;

public class TutorialSceneController : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialScenes;       //The different images that are needed for the scenes

    [Header("Tutorial Scenes")]
    [SerializeField] public UnityEngine.UI.Image currentSceneImage;        //The image component in the scene that gets the image for the current scene

    public int currentSceneNumber = 0;     //The tutorial scene the user is currently in (first scene = 1)

    private void Start()
    {

        currentSceneNumber = 0;
        ObjectsForCurrentScene();
    }

    public void TransitionBackwardInTutorialScnen()
    {
        Debug.Log("Transition to the Scene before");

        if (currentSceneNumber > 0)
        {
            currentSceneNumber--;
        }
    }
    public void TransitionForwardInTutorialScene()
    {
        Debug.Log("Transition to next Scene");

        if (currentSceneNumber < tutorialScenes.Length - 1)
        {
            currentSceneNumber++;
        }     
    }

    public void ObjectsForCurrentScene()
    {
        foreach (GameObject scenes in tutorialScenes)
        {
            if (scenes != null)
            {
                scenes.SetActive(false);
            }
        }
        if (tutorialScenes[currentSceneNumber] != null)
        {
            tutorialScenes[currentSceneNumber].SetActive(true);
        }

        currentSceneImage.sprite = tutorialScenes[currentSceneNumber].GetComponentInChildren<UnityEngine.UI.Image>().sprite;
    }
}
