using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerFalseScript : MonoBehaviour
{
    public void NextScene()
    {
        int id = PlayerPrefs.GetInt("CurrentID");
        id -= 1;
        PlayerPrefs.SetInt("CurrentID", id);
        SceneManager.LoadScene("SekretaerScene");
    }
    private void Start()
    {


        int curScore = PlayerPrefs.GetInt("currentScore");
        int addition = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID"));
        curScore += addition;
        PlayerPrefs.SetInt("currentScore", curScore);

        int statebudget = PlayerPrefs.GetInt("currentStatebudget");
        int addition2 = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID"));
        statebudget += addition2;
        PlayerPrefs.SetInt("currentStatebudget", statebudget);
    }

}
