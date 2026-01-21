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

    public GameObject oppositionFill;
    public TextMeshProUGUI oppositionText;

    public GameObject functionalityFill;
    public TextMeshProUGUI functionalityText;

    string scorePlayerPrefName = "currentScore";
    string budgetPlayerPrefName = "currentStatebudget";
    string oppositionPlayerPrefName = "currentOpposition";
    string functionalityPlayerPrefName = "currentFunctionality";
    string correctAnswerIDPlayerPrefName = "CorrectRightAnswerID";
    string currentAnswerPlayerPrefName = "CurrentAnswer";
    string clickedButtonIDPlayerPrefName = "clickedButtonID";

    public void AnswerButton()
    {
        int correctAnswerID = PlayerPrefs.GetInt(correctAnswerIDPlayerPrefName);
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        int clickedButtonID = System.Array.IndexOf(answer, clicked);

        PlayerPrefs.SetInt(clickedButtonIDPlayerPrefName, clickedButtonID);

        if (correctAnswerID == clickedButtonID) //Noch einbauen, dass Bool true werden muss
        {
            SceneManager.LoadScene(Answer_right_Scene);
        }
        else
        {
            SceneManager.LoadScene(Answer_wrong_Scene);
        }
    }

    private void Start()
    {
        UpdateStatBar(scorePlayerPrefName, happinessFill, happinessText);
        UpdateStatBar(budgetPlayerPrefName, statebudgetFill, statebudgetText);
        UpdateStatBar(oppositionPlayerPrefName, oppositionFill, oppositionText);
        UpdateStatBar(functionalityPlayerPrefName, functionalityFill, functionalityText);

        for (int i = 0; i < 4; i++)
        {
            answer[i].GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString(currentAnswerPlayerPrefName + i);
            
        }
    }

    private void UpdateStatBar(string prefKey, GameObject fill, TextMeshProUGUI textObj)
    {
        int curValue = PlayerPrefs.GetInt(prefKey);
        if (curValue <= 100f)
        {
            Transform fillTransform = fill.GetComponent<Transform>();
            Vector3 scale = fillTransform.localScale;
            scale.x = curValue / 100f;
            fillTransform.localScale = scale;
        }
        textObj.text = curValue.ToString();
    }
}
