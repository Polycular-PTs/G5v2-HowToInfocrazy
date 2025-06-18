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
        int curScore = PlayerPrefs.GetInt("currentScore");
        int addition = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID"));
        curScore += addition;
        PlayerPrefs.SetInt("currentScore", curScore);

        int statebudget = PlayerPrefs.GetInt("currentStatebudget");
        int addition2 = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID"));
        statebudget += addition2;
        PlayerPrefs.SetInt("currentStatebudget", statebudget);

        int id = PlayerPrefs.GetInt("CurrentID");
        QuestionsWithAnswers CurrentQ = Resources.Load<QuestionsWithAnswers>("Data/Frage" + id);
        a.clip = Resources.Load<VideoClip>(CurrentQ.video);
    }

   
}
