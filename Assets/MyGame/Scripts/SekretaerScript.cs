using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SekretaerScript : MonoBehaviour
{
    //public int score = 100;
    public TextMeshProUGUI Questiontxt;
    public QuestionsWithAnswers[] allQuestions;
    public int id = 0;
    public Button answerButton;
    //private int statefriendliness = 100;
    public TextMeshProUGUI staatsfreundlichkeitText;

    //public Slider happinessSlider;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    public GameObject oppositionFill;
    public TextMeshProUGUI oppositionText;

    public GameObject functionalityFill;
    public TextMeshProUGUI functionalityText;
    private string whenOutOfQuestion = "You are out of questions";

    private const string vierAntwortenSzene = "4Antworten";
    private const string currentIDPlayerPrefName = "CurrentID";
    private const string currentQuestionPlayerPrefName = "CurrentQuestion";
    private const string currentScorePlayerPrefName = "currentScore";
    private const string currentStatebudgetPlayerPrefName = "currentStatebudget";
    private const string currentOppositionPlayerPrefName = "currentOpposition";
    private const string currentFunctionalityPlayerPrefName = "currentFunctionality";

    private const string currentAnswerForQuestionPlayerPrefName = "CurrentAnswer";
    private const string currentHappinessForQuestionPlayerPrefName = "CurrentHappiness";
    private const string currentBudgetForQuestionPlayerPrefName = "CurrentBudget";
    private const string currentOppositionForQuestionPlayerPrefName = "addOpposition";
    private const string currentFunctionalityForQuestionPlayerPrefName = "addFunctionality";
    private const string currentRightAnswerForQuestionPlayerPrefName = "CorrectRightAnswerID";

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowAnswerOptions()
    {
        id = PlayerPrefs.GetInt("CurrentID");
        if (allQuestions.Length > id)
        {
            PlayerPrefs.SetString(currentQuestionPlayerPrefName, allQuestions[id].question);
            LoadScene(vierAntwortenSzene);
            id += 1;
            PlayerPrefs.SetInt(currentIDPlayerPrefName, id);
        }
        else
        {
            //Debug.Log("No more questions");
        }

    }

    public void IDNullStellen()
    {
        PlayerPrefs.SetInt(currentIDPlayerPrefName, 0);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey(currentScorePlayerPrefName);
        PlayerPrefs.DeleteKey(currentStatebudgetPlayerPrefName);

        ResetGraphic(happinessFill.GetComponent<Transform>());
        ResetGraphic(statebudgetFill.GetComponent<Transform>());
    }

    private void ResetGraphic(Transform fillTransform)
    {
        Vector3 scale = fillTransform.localScale;
        scale.x = 1;
        fillTransform.localScale = scale;
    }

    private void ChangeGraphicAndText(int curValue, Transform fillTransform, TextMeshProUGUI text)
    {
        if (curValue <= 100f)
        {
            Vector3 scale = fillTransform.localScale;
            scale.x = curValue / 100f;
            fillTransform.localScale = scale;
        }
        text.text = curValue.ToString();
    }

    private void Start()
    {
        InitPlayerPrefsDefaults();
        LoadQuestionsToUI();
    }

    private void InitPlayerPrefsDefaults()
    {
        int curScore = PlayerPrefs.GetInt(currentScorePlayerPrefName);
        ChangeGraphicAndText(curScore, happinessFill.GetComponent<Transform>(), happinessText);

        int stateBudget = PlayerPrefs.GetInt(currentStatebudgetPlayerPrefName);
        ChangeGraphicAndText(stateBudget, statebudgetFill.GetComponent<Transform>(), statebudgetText);

        int opposition = PlayerPrefs.GetInt(currentOppositionPlayerPrefName);
        ChangeGraphicAndText(opposition, oppositionFill.GetComponent<Transform>(), oppositionText);

        int functionality = PlayerPrefs.GetInt(currentFunctionalityPlayerPrefName);
        ChangeGraphicAndText(functionality, functionalityFill.GetComponent<Transform>(), functionalityText);
    }

    private void LoadQuestionsToUI()
    {
        id = PlayerPrefs.GetInt(currentIDPlayerPrefName);

        if (!PlayerPrefs.HasKey(currentIDPlayerPrefName))
        {
            PlayerPrefs.SetInt(currentIDPlayerPrefName, 0);
        }
        if (allQuestions.Length > id)
        {
            PlayerPrefs.SetString(currentQuestionPlayerPrefName, allQuestions[id].question);
            PlayerPrefs.SetInt(currentRightAnswerForQuestionPlayerPrefName, allQuestions[id].idRightAnswer);


            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetString(currentAnswerForQuestionPlayerPrefName + i, allQuestions[id].answers[i]);
                PlayerPrefs.SetInt(currentHappinessForQuestionPlayerPrefName + i, allQuestions[id].happiness[i]);
                PlayerPrefs.SetInt(currentBudgetForQuestionPlayerPrefName + i, allQuestions[id].budget[i]);
                PlayerPrefs.SetInt(currentOppositionForQuestionPlayerPrefName + i, allQuestions[id].opposition[i]);
                PlayerPrefs.SetInt(currentFunctionalityForQuestionPlayerPrefName + i, allQuestions[id].functionality[i]);
            }
            Questiontxt.text = PlayerPrefs.GetString(currentQuestionPlayerPrefName);
        }
        else
        {
            Questiontxt.text = whenOutOfQuestion;
            answerButton.enabled = false;
        }
    }

    void Awake()
    {
        LoadAllVideos();
    }

    void LoadAllVideos()
    {
        allQuestions = Resources.LoadAll<QuestionsWithAnswers>("Data");
    }
}
