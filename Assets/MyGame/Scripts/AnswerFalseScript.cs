using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerFalseScript : MonoBehaviour
{
    public ScoreServiceForAnswer scoreService;

    public void NextScene()
    {
        int id = PlayerPrefs.GetInt("CurrentID");
        id -= 1;
        PlayerPrefs.SetInt("CurrentID", id);
        SceneManager.LoadScene("SekretaerScene");
    }
    private void Start() //0 ist false und 1 ist true
    {
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 1)
        {
            PlayerPrefs.SetInt("firstAnswerBoolean", 0);
            scoreService.ChangeValues("currentScore", "CurrentHappiness");
            scoreService.ChangeValues("currentStatebudget", "CurrentBudget");
            scoreService.ChangeValues("currentOpposition", "addOpposition");
            scoreService.ChangeValues("currentFunctionality", "addFunctionality");
        }
    }
    //private void ChangeValues(string playerPrefName, string playerPrefName2)
    //{
    //    int curValue = PlayerPrefs.GetInt(playerPrefName);
    //    int addition = PlayerPrefs.GetInt(playerPrefName2 + PlayerPrefs.GetInt("clickedButtonID"));
    //    curValue += addition;
    //    PlayerPrefs.SetInt(playerPrefName, curValue);
    //}
}
