using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VierAntwortenScript: MonoBehaviour
{
    //public string AnswersName = "4Antworten";
    public GameObject[] answer;
    //public QuestionsWithAnswers[] allQuestions;
    //public int id=0;
    public string Answer_right_Scene;
    public string Answer_wrong_Scene;

    public Slider happinessSlider;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    // Answer_Right Build Index: 3
    // Answer_Wrong Build Index: 4

    //public void ShowQuestion()
    //{
    //    PlayerPrefs.GetString("CurrentQuestion", allQuestions[id].question);
    //    Debug.Log(allQuestions[id].question);
    //    id += 1;
    //    if (allQuestions.Length > id)
    //    {
    //        PlayerPrefs.SetString("CurrentQuestion", allQuestions[id].question);
    //        Debug.Log("Next question loaded");
    //    }
    //    else
    //    {
    //        Debug.Log("No more questions");
    //    }
    //}

    public void AnswerButton()
    {
        int correctAnswerID = PlayerPrefs.GetInt("CorrectRightAnswerID");
        //Debug.Log(correctAnswerID);
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(clicked.name);
        int clickedButtonID=0;
        for(int i=0; i<4; i++)
        {
            if (clicked.name == answer[i].name)
            {
                clickedButtonID = i;
                //Debug.Log("NewClickedButtonID");
            }
        }

        PlayerPrefs.SetInt("clickedButtonID", clickedButtonID);



        if (correctAnswerID == clickedButtonID)
        {
            SceneManager.LoadScene(Answer_right_Scene);
        }
        else
        {
            SceneManager.LoadScene(Answer_wrong_Scene);
        }

        //PlayerPrefs.GetString("CurrentQuestion", allQuestions[id].question);
        //Debug.Log(allQuestions[id].question);
        //id += 1;
        //if (allQuestions.Length > id)
        //{
        //    PlayerPrefs.SetString("CurrentQuestion", allQuestions[id].question);
        //    Debug.Log("Next question loaded");
        //}
        //else
        //{
        //    Debug.Log("No more questions");
        //}
    }

    private void Start()
    {
        int curScore = PlayerPrefs.GetInt("currentScore");
        if (curScore / 100f <= 1)
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

        //Debug.Log(PlayerPrefs.GetInt("CurrentID"));
        //int id2 = PlayerPrefs.GetInt("CurrentID");
        //string currentQuestion = PlayerPrefs.GetString("CurrentQuestion");
        //Debug.Log(currentQuestion);
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(answer[i].GetComponentInChildren<TextMeshProUGUI>().name);
            answer[i].GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("CurrentAnswer" + i);
            
        }
    }

    //void Awake()
    //{
    //    LoadAllVideos();
    //}

    //void LoadAllVideos()
    //{
    //    allQuestions = Resources.LoadAll<QuestionsWithAnswers>("Data");
    //    Debug.Log($"Geladene Fragen: {allQuestions.Length}");
    //}


    //public void GoToQuestions()
    //{
    //    SceneManager.LoadScene(AnswersName);
    //}

    //public void ShowAnswers()
    //{
    //    Debug.Log(x[id].answers);
    //    for(int x=0; x>4; x++)
    //    {
            
    //    }
    //    id += 1;
    //}
}
