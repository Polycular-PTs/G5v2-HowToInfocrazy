using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
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
        SceneManager.LoadScene("Office");
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial_Office");
    }
}
