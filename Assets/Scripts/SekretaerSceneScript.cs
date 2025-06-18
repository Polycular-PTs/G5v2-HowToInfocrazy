using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SekretaerSceneScript : MonoBehaviour
{
    //public int score = 100;
    public TextMeshProUGUI Questiontxt;
    public QuestionsWithAnswers[] allQuestions;
    public int id = 0;
    public Button AnswerButton;
    //private int statefriendliness = 100;
    public TextMeshProUGUI staatsfreundlichkeitText;

    public Slider happinessSlider;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    public void ShowQuestion()
    {
        id = PlayerPrefs.GetInt("CurrentID");
        //Debug.Log(id);
        if (allQuestions.Length > id)
        {
            PlayerPrefs.SetString("CurrentQuestion", allQuestions[id].question);
            //Debug.Log("Next question loaded");
            SceneManager.LoadScene("4Antworten");
            id += 1;
            //Debug.Log(id);
            PlayerPrefs.SetInt("CurrentID", id);
            //Debug.Log(PlayerPrefs.GetInt("CurrentID"));
        }
        else
        {
            Debug.Log("No more questions");
        }

    }

    public void IDNullStellen()
    {
        PlayerPrefs.SetInt("CurrentID", 0);
        Debug.Log("ID wurde auf 0 gestellt");
    }

    public void ResetFriendliness()
    {
        PlayerPrefs.SetInt("currentScore", 100);
        happinessSlider.maxValue = 1;
        Debug.Log("Staatsfreundlichkeit wurde zurückgesetzt");
    }

    private void Start()
    {
        int curScore = PlayerPrefs.GetInt("currentScore");
        Debug.Log("CurrentScore2:" + curScore);
        if (curScore < 100f)
        {
            Transform fillTransform = happinessFill.GetComponent<Transform>();
            Vector3 scale = fillTransform.localScale;
            scale.x = curScore / 100f;
            fillTransform.localScale = scale;
        }

        happinessSlider.value = curScore / 100f;
        happinessText.text = curScore.ToString();


        int stateBudget = PlayerPrefs.GetInt("currentStatebudget");
        if (stateBudget < 100f)
        {
            Transform fillTransform = statebudgetFill.GetComponent<Transform>();
            Vector3 scale = fillTransform.localScale;
            scale.x = stateBudget / 100f;
            fillTransform.localScale = scale;
        }

        statebudgetText.text = stateBudget.ToString();

        //staatsfreundlichkeitText.text = PlayerPrefs.GetInt("staatsfriendliness").ToString();

        //if (!PlayerPrefs.HasKey("statefriendliness"))
        //{
        //    PlayerPrefs.SetInt("statefriendliness", statefriendliness);
        //}

        //if (!PlayerPrefs.HasKey("currentScore"))
        //{
        //    PlayerPrefs.SetInt("currentScore", score);
        //}


        id = PlayerPrefs.GetInt("CurrentID");

        //Debug.Log(PlayerPrefs.GetInt("CurrentID"));
        if (!PlayerPrefs.HasKey("CurrentID"))
        {
            PlayerPrefs.SetInt("CurrentID", 0);
        }
        if (allQuestions.Length > id)
        {
            PlayerPrefs.SetString("CurrentQuestion", allQuestions[id].question);
            PlayerPrefs.SetInt("CorrectRightAnswerID", allQuestions[id].idRightAnswer);


            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetString("CurrentAnswer" + i, allQuestions[id].answers[i]);
                PlayerPrefs.SetInt("CurrentHappiness" + i, allQuestions[id].happiness[i]);
                PlayerPrefs.SetInt("CurrentBudget" + i, allQuestions[id].budget[i]);
            }



            Questiontxt.text = PlayerPrefs.GetString("CurrentQuestion");
        }
        else
        {
            Debug.Log("No more questions");
            Questiontxt.text = "You are out of questions";
            AnswerButton.enabled=false;
        }
    }

    void Awake()
    {
        LoadAllVideos();
    }

    //public string videoClipPath;
    //public VideoClip LoadedVideoClip { get; private set; }

    void LoadAllVideos()
    {
        allQuestions = Resources.LoadAll<QuestionsWithAnswers>("Data");
        Debug.Log($"Geladene Fragen: {allQuestions.Length}");
    }
}
