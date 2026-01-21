using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Interfaces;

public class OfficeScript : MonoBehaviour
{
    public int score = 100;
    //public Slider happinessSlider;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;
    private bool firstAnswer;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    public GameObject oppositionFill;
    public TextMeshProUGUI oppositionText;

    public GameObject functionalityFill;
    public TextMeshProUGUI functionalityText;

    private void ChangeGraphic(int curValue, Transform fillTransform)
    {
        if (curValue <= 100f)
        {
            Vector3 scale = fillTransform.localScale;
            scale.x = curValue / 100f;
            fillTransform.localScale = scale;
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentScore"))
        {
            PlayerPrefs.SetInt("currentScore", 100);
            //Transform fillTransform = happinessFill.GetComponent<Transform>();
            //Vector3 scale = fillTransform.localScale;
            //scale.x = 1;
            //fillTransform.localScale = scale;
            ChangeGraphic(100, happinessFill.GetComponent<Transform>());
            //happinessSlider.maxValue = 1;
            PlayerPrefs.SetString("firstAnswerBoolean", "true");
        }
        else
        {
            int curScore = PlayerPrefs.GetInt("currentScore");
            Debug.Log("CurrentScore1:" + curScore);
            //happinessSlider.value = curScore / 100f;

            ChangeGraphic(curScore, happinessFill.GetComponent<Transform>());
            happinessText.text = curScore.ToString();
            if (curScore <= 0) { SceneManager.LoadScene("Defeat"); }
            //if (curScore < 100f)
            //{
            //    Transform fillTransform = happinessFill.GetComponent<Transform>();
            //    Vector3 scale = fillTransform.localScale;
            //    scale.x = curScore / 100f;
            //    fillTransform.localScale = scale;
            //}

            //if (curScore / 100 > happinessSlider.maxValue)
            //{
            //    happinessSlider.maxValue = curScore / 100;
            //}
            //if (curScore <= 0)
            //{
            //    SceneManager.LoadScene("Defeat");
            //    //Colour
            //    //Text mit Defeat oder Win
            //}
        }


        if (!PlayerPrefs.HasKey("currentStatebudget"))
        {
            PlayerPrefs.SetInt("currentStatebudget", 100);
            //Transform fillTransform = statebudgetFill.GetComponent<Transform>();
            //Vector3 scale = fillTransform.localScale;
            //scale.x = 1;
            //fillTransform.localScale = scale;
            ChangeGraphic(100, statebudgetFill.GetComponent<Transform>());
        }
        else
        {
            int curState = PlayerPrefs.GetInt("currentStatebudget");
            Debug.Log("currentStatebudget:" + curState);
            //happinessSlider.value = curScore / 100f;

            //if (curState < 100f)
            //{
            //    Transform fillTransform = statebudgetFill.GetComponent<Transform>();
            //    Vector3 scale = fillTransform.localScale;
            //    scale.x = curState / 100f;
            //    fillTransform.localScale = scale;
            //}
            ChangeGraphic(curState, statebudgetFill.GetComponent<Transform>());
            statebudgetText.text = curState.ToString();
            if (curState <= 0) { SceneManager.LoadScene("Defeat"); }
            //if (curState / 100 > happinessSlider.maxValue)
            //{
            //    happinessSlider.maxValue = curState / 100;
            //}
            //if (curState <= 0)
            //{
            //    //Debug.Log("CurState ist Null");
            //    SceneManager.LoadScene("Defeat");
            //    //Colour
            //    //Text mit Defeat oder Win
            //}
        }

        if (!PlayerPrefs.HasKey("currentOpposition"))
        {
            PlayerPrefs.SetInt("currentOpposition", 100);
            ChangeGraphic(100, oppositionFill.GetComponent<Transform>());
            //Transform fillTransform = oppositionFill.GetComponent<Transform>();
            //Vector3 scale = fillTransform.localScale;
            //scale.x = 1;
            //fillTransform.localScale = scale;
        }
        else
        {
            int curOppo = PlayerPrefs.GetInt("currentOpposition");
            //Debug.Log("currentOpposition:" + curState);
            ChangeGraphic(curOppo, oppositionFill.GetComponent<Transform>());
            oppositionText.text = curOppo.ToString();
            if (curOppo <= 0) { SceneManager.LoadScene("Defeat"); }
            //if (curOppo < 100f)
            //{
            //    Transform fillTransform = oppositionFill.GetComponent<Transform>();
            //    Vector3 scale = fillTransform.localScale;
            //    scale.x = curOppo / 100f;
            //    fillTransform.localScale = scale;
            //}
            //if (curOppo <= 0)
            //{
            //    Debug.Log("CurState ist Null");
            //    SceneManager.LoadScene("Defeat");
            //}
        }

        if (!PlayerPrefs.HasKey("currentFunctionality"))
        {
            PlayerPrefs.SetInt("currentFunctionality", 100);
            ChangeGraphic(100, functionalityFill.GetComponent<Transform>());
            //Transform fillTransform = functionalityFill.GetComponent<Transform>();
            //Vector3 scale = fillTransform.localScale;
            //scale.x = 1;
            //fillTransform.localScale = scale;
        }
        else
        {
            int curFunc = PlayerPrefs.GetInt("currentFunctionality");
            //Debug.Log("currentFunctionality:" + curFunc);
            ChangeGraphic(curFunc, functionalityFill.GetComponent<Transform>());
            functionalityText.text = curFunc.ToString();
            if (curFunc <= 0) { SceneManager.LoadScene("Defeat"); }
            //if (curFunc < 100f)
            //{
            //    Transform fillTransform = functionalityFill.GetComponent<Transform>();
            //    Vector3 scale = fillTransform.localScale;
            //    scale.x = curFunc / 100f;
            //    fillTransform.localScale = scale;
            //}
            //if (curFunc <= 0)
            //{
            //    Debug.Log("CurState ist Null");
            //    SceneManager.LoadScene("Defeat");
            //}
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SekretaerScene");
    }
}
