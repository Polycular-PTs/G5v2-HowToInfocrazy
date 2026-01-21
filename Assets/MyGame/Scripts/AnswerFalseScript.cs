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
    private void Start() //0 ist false und 1 ist true
    {
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 1)
        {
            PlayerPrefs.SetInt("firstAnswerBoolean", 0);
            ChangeValues("currentScore", "CurrentHappiness");
            ChangeValues("currentStatebudget", "CurrentBudget");
            ChangeValues("currentOpposition", "addOpposition");
            ChangeValues("currentFunctionality", "addFunctionality");
            //int curScore = PlayerPrefs.GetInt("currentScore");
            //int addition = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID"));
            //curScore += addition;
            //PlayerPrefs.SetInt("currentScore", curScore);

            //int statebudget = PlayerPrefs.GetInt("currentStatebudget");
            //int addition2 = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID"));
            //statebudget += addition2;
            //PlayerPrefs.SetInt("currentStatebudget", statebudget);

            //int opposition = PlayerPrefs.GetInt("currentOpposition");
            //int addition3 = PlayerPrefs.GetInt("addOpposition" + PlayerPrefs.GetInt("clickedButtonID"));
            //opposition += addition3;
            //PlayerPrefs.SetInt("currentOpposition", opposition);

            //int functionality = PlayerPrefs.GetInt("currentFunctionality");
            //int addition4 = PlayerPrefs.GetInt("addFunctionality" + PlayerPrefs.GetInt("clickedButtonID"));
            //functionality += addition4;
            //PlayerPrefs.SetInt("currentFunctionality", functionality);
        }
    }

    private void ChangeValues(string playerPrefName, string playerPrefName2)
    {
        int curValue = PlayerPrefs.GetInt(playerPrefName);
        int addition = PlayerPrefs.GetInt(playerPrefName2 + PlayerPrefs.GetInt("clickedButtonID"));
        curValue += addition;
        PlayerPrefs.SetInt(playerPrefName, curValue);
    }

}
