using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Interfaces;

public class OfficeScript : MonoBehaviour
{
    public int score = 100;
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

    string scorePlayerPrefName = "currentScore";
    string budgetPlayerPrefName = "currentStatebudget";
    string oppositionPlayerPrefName = "currentOpposition";
    string functionalityPlayerPrefName = "currentFunctionality";
    string firstAnswerBoolName = "firstAnswerBoolean";
    string sekretarianSceneName = "SekretaerScene";
    string defeatSceneName = "Defeat";

    //private bool firstAnswer;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    public GameObject oppositionFill;
    public TextMeshProUGUI oppositionText;

    public GameObject functionalityFill;
    public TextMeshProUGUI functionalityText;

    private void LoadDefeatScene(int whenThisNumberIsZero)
    {
        if (whenThisNumberIsZero <= 0)
        {
            SceneManager.LoadScene(defeatSceneName);
        }
    }

    private void ChangeGraphic(int curValue, Transform fillTransform)
    {
        if (curValue <= 100f)
        {
            Vector3 scale = fillTransform.localScale;
            scale.x = curValue / 100f;
            fillTransform.localScale = scale;
        }
    }

    private void ResetPlayerPrefsValue(string name, Transform fillTransform)
    {
        PlayerPrefs.SetInt(name, 100);
        ChangeGraphic(100, fillTransform.GetComponent<Transform>());
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(scorePlayerPrefName))
        {
            ResetPlayerPrefsValue(scorePlayerPrefName, happinessFill.GetComponent<Transform>());
            //PlayerPrefs.SetInt("currentScore", 100);
            //ChangeGraphic(100, happinessFill.GetComponent<Transform>());
            PlayerPrefs.SetInt(firstAnswerBoolName, 1);
        }
        else
        {
            int curScore = PlayerPrefs.GetInt(scorePlayerPrefName);
            ChangeGraphic(curScore, happinessFill.GetComponent<Transform>());
            happinessText.text = curScore.ToString();
            LoadDefeatScene(curScore);
            //if (curScore <= 0) { SceneManager.LoadScene("Defeat"); }
        }


        if (!PlayerPrefs.HasKey(budgetPlayerPrefName))
        {
            ResetPlayerPrefsValue(budgetPlayerPrefName, statebudgetFill.GetComponent<Transform>());
            //PlayerPrefs.SetInt("currentStatebudget", 100);
            //ChangeGraphic(100, statebudgetFill.GetComponent<Transform>());
        }
        else
        {
            int curState = PlayerPrefs.GetInt(budgetPlayerPrefName);
            ChangeGraphic(curState, statebudgetFill.GetComponent<Transform>());
            statebudgetText.text = curState.ToString();
            LoadDefeatScene(curState);
            //if (curState <= 0) { LoadDefeatScene(); }
        }

        if (!PlayerPrefs.HasKey(oppositionPlayerPrefName))
        {
            ResetPlayerPrefsValue(oppositionPlayerPrefName, oppositionFill.GetComponent<Transform>());
            //PlayerPrefs.SetInt("currentOpposition", 100);
            //ChangeGraphic(100, oppositionFill.GetComponent<Transform>());
        }
        else
        {
            int curOppo = PlayerPrefs.GetInt(oppositionPlayerPrefName);
            ChangeGraphic(curOppo, oppositionFill.GetComponent<Transform>());
            oppositionText.text = curOppo.ToString();
            LoadDefeatScene(curOppo);
            //if (curOppo <= 0) { SceneManager.LoadScene("Defeat"); }
        }

        if (!PlayerPrefs.HasKey(functionalityPlayerPrefName)) 
        {
            ResetPlayerPrefsValue(functionalityPlayerPrefName, functionalityFill.GetComponent<Transform>());
            //PlayerPrefs.SetInt("currentFunctionality", 100);
            //ChangeGraphic(100, functionalityFill.GetComponent<Transform>());
        }
        else
        {
            int curFunc = PlayerPrefs.GetInt(functionalityPlayerPrefName);
            ChangeGraphic(curFunc, functionalityFill.GetComponent<Transform>());
            functionalityText.text = curFunc.ToString();
            LoadDefeatScene(curFunc);
            //if (curFunc <= 0) { SceneManager.LoadScene("Defeat"); }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sekretarianSceneName);
    }
}
