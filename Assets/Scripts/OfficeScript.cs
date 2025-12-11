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

    private void Start()
    {
        
        if (!PlayerPrefs.HasKey("currentScore"))
        {
            PlayerPrefs.SetInt("currentScore", 100);
            Transform fillTransform = happinessFill.GetComponent<Transform>();
            Vector3 scale = fillTransform.localScale;
            scale.x = 1;
            fillTransform.localScale = scale;
            //happinessSlider.maxValue = 1;
            PlayerPrefs.SetString("firstAnswerBoolean", "true");
        }
        else
        {
            int curScore = PlayerPrefs.GetInt("currentScore");
            Debug.Log("CurrentScore1:" + curScore);
            //happinessSlider.value = curScore / 100f;

            if (curScore < 100f)
            {
                Transform fillTransform = happinessFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curScore / 100f;
                fillTransform.localScale = scale;
            }
            happinessText.text = curScore.ToString();
            //if (curScore / 100 > happinessSlider.maxValue)
            //{
            //    happinessSlider.maxValue = curScore / 100;
            //}
            if (curScore <= 0)
            {
                SceneManager.LoadScene("Defeat");
                //Colour
                //Text mit Defeat oder Win
            }
        }


        if (!PlayerPrefs.HasKey("currentStatebudget"))
        {
            PlayerPrefs.SetInt("currentStatebudget", 100);
            Transform fillTransform = statebudgetFill.GetComponent<Transform>();
            Vector3 scale = fillTransform.localScale;
            scale.x = 1;
            fillTransform.localScale = scale;
        }
        else
        {
            int curState = PlayerPrefs.GetInt("currentStatebudget");
            Debug.Log("currentStatebudget:" + curState);
            //happinessSlider.value = curScore / 100f;

            if (curState < 100f)
            {
                Transform fillTransform = statebudgetFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curState / 100f;
                fillTransform.localScale = scale;
            }
            statebudgetText.text = curState.ToString();
            //if (curState / 100 > happinessSlider.maxValue)
            //{
            //    happinessSlider.maxValue = curState / 100;
            //}
            if (curState <= 0)
            {
                Debug.Log("CurState ist Null");
                SceneManager.LoadScene("Defeat");
                //Colour
                //Text mit Defeat oder Win
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SekretaerScene");
    }
}
