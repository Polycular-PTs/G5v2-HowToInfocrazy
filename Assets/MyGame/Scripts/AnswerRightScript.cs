using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class AnswerRightScript : MonoBehaviour
{
    public VideoPlayer a;
    public ScoreServiceForAnswer scoreService;
    string officeSceneName = "Office";

    public void NextScene()
    {
        LoadScene(officeSceneName);   
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 1)
        {
            scoreService.ChangeValues("currentScore", "CurrentHappiness");
            scoreService.ChangeValues("currentStatebudget", "CurrentBudget");
            scoreService.ChangeValues("currentOpposition", "addOpposition");
            scoreService.ChangeValues("currentFunctionality", "addFunctionality");

            int id = PlayerPrefs.GetInt("CurrentID");
            QuestionsWithAnswers CurrentQ = Resources.Load<QuestionsWithAnswers>("Data/Frage" + id);
            a.clip = Resources.Load<VideoClip>(CurrentQ.video);
        }
        if (PlayerPrefs.GetInt("firstAnswerBoolean") == 0)
        {
            PlayerPrefs.SetInt("firstAnswerBoolean", 1);
        }

    }
}
