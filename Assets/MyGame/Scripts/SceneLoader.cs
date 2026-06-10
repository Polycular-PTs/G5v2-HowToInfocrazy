using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private const string SCENE_OFFICE = "Office";
    private const string SCENE_TUTORIAL_OFFICE = "Tutorial_Office";
    private string[] keysToDelete = { "currentScore", "CurrentID", "currentStatebudget", "currentOpposition", "currentFunctionality", "CurrentLeakID1", "CurrentLeakID2", "CurrentLeakID3" };

    private void Start()
    {
        ResetGameState();
    }

    private void ResetGameState()
    {
        for(int i=0; i < keysToDelete.Length; i++)
        {
            PlayerPrefs.DeleteKey(keysToDelete[i]);
        }
    }

    public void StartGame()
    {
        LoadScenes(SCENE_OFFICE);
    }
    public void StartTutorial()
    {
        LoadScenes(SCENE_TUTORIAL_OFFICE);
    }

    public void LoadScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
