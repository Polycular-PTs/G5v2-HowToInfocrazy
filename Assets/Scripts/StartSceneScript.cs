using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("CurrentID");
        PlayerPrefs.DeleteKey("currentStatebudget");
        PlayerPrefs.DeleteKey("currentOpposition");
        PlayerPrefs.DeleteKey("currentFunctionality");
        PlayerPrefs.DeleteKey("CurrentLeakID1");
        PlayerPrefs.DeleteKey("CurrentLeakID2");
        PlayerPrefs.DeleteKey("CurrentLeakID3");
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
