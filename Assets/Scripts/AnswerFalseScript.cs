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
            int curScore = PlayerPrefs.GetInt("currentScore");
            int addition = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID"));
            curScore += addition;
            PlayerPrefs.SetInt("currentScore", curScore);

            int statebudget = PlayerPrefs.GetInt("currentStatebudget");
            int addition2 = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID"));
            statebudget += addition2;
            PlayerPrefs.SetInt("currentStatebudget", statebudget);
            PlayerPrefs.SetInt("firstAnswerBoolean", 0);

            int opposition = PlayerPrefs.GetInt("currentOpposition");
            int addition3 = PlayerPrefs.GetInt("addOpposition" + PlayerPrefs.GetInt("clickedButtonID"));
            opposition += addition3;
            PlayerPrefs.SetInt("currentOpposition", opposition);

            int functionality = PlayerPrefs.GetInt("currentFunctionality");
            int addition4 = PlayerPrefs.GetInt("addFunctionality" + PlayerPrefs.GetInt("clickedButtonID"));
            functionality += addition4;
            PlayerPrefs.SetInt("currentFunctionality", functionality);
        }
    }

}
