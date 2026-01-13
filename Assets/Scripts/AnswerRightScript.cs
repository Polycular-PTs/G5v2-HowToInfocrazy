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
        if (PlayerPrefs.GetString("firstAnswerBoolean") == "true")
        {
            int curScore = PlayerPrefs.GetInt("currentScore");
            int addition = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID"));
            curScore += addition;
            PlayerPrefs.SetInt("currentScore", curScore);

            int statebudget = PlayerPrefs.GetInt("currentStatebudget");
            int addition2 = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID"));
            statebudget += addition2;
            PlayerPrefs.SetInt("currentStatebudget", statebudget);

            int opposition = PlayerPrefs.GetInt("currentOpposition");
            int addition3 = PlayerPrefs.GetInt("addOpposition" + PlayerPrefs.GetInt("clickedButtonID"));
            opposition += addition3;
            PlayerPrefs.SetInt("currentOpposition", opposition);

            int functionality = PlayerPrefs.GetInt("currentFunctionality");
            int addition4 = PlayerPrefs.GetInt("addFunctionality" + PlayerPrefs.GetInt("clickedButtonID"));
            functionality += addition4; 
            PlayerPrefs.SetInt("currentFunctionality", functionality);

            int id = PlayerPrefs.GetInt("CurrentID");
            QuestionsWithAnswers CurrentQ = Resources.Load<QuestionsWithAnswers>("Data/Frage" + id);
            a.clip = Resources.Load<VideoClip>(CurrentQ.video);
        }
        if (PlayerPrefs.GetString("firstAnswerBoolean") == "false")
        {
            PlayerPrefs.SetString("firstAnswerBoolean", "true");
        }

    }

   
}
