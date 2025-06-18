using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OfficeScript : MonoBehaviour
{
    public int score = 100;
    public Slider happinessSlider;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

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
            happinessSlider.maxValue = 1;
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
            int curScore = PlayerPrefs.GetInt("currentStatebudget");
            Debug.Log("currentStatebudget:" + curScore);
            //happinessSlider.value = curScore / 100f;

            if (curScore < 100f)
            {
                Transform fillTransform = statebudgetFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curScore / 100f;
                fillTransform.localScale = scale;
            }
            statebudgetText.text = curScore.ToString();
            //if (curScore / 100 > happinessSlider.maxValue)
            //{
            //    happinessSlider.maxValue = curScore / 100;
            //}
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SekretaerScene");
    }
}
