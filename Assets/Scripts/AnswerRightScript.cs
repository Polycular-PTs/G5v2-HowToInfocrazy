using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class AnswerRightScript : MonoBehaviour
{
    public VideoPlayer a;

    public void NextScene()
    {
        SceneManager.LoadScene("Office");   
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 1)
        {
            ChangeValues("currentScore", "CurrentHappiness");
            ChangeValues("currentStatebudget", "CurrentBudget");
            ChangeValues("currentOpposition", "addOpposition");
            ChangeValues("currentFunctionality", "addFunctionality");

            int id = PlayerPrefs.GetInt("CurrentID");
            QuestionsWithAnswers CurrentQ = Resources.Load<QuestionsWithAnswers>("Data/Frage" + id);
            a.clip = Resources.Load<VideoClip>(CurrentQ.video);
        }
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 0)
        {
            PlayerPrefs.SetInt("firstAnswerBoolean", 1);
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
