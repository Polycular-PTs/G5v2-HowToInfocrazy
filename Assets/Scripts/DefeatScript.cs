using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatScript : MonoBehaviour
{
    string scoreDownText = "Deine Bevölkerung ist unzufrieden und hat dein Büro gestürmt.Deine Amtszeit ist damit zu Ende.";
    string budgetDownText = "Dein Budget ist leer und du kannst deinen Staat nicht mehr aufrecht erhalten. Deine Amtszeit ist damit zu Ende.";
    string alleWerteSindNullText = "Dein ganzer Staat ist zusammengefallen. Deine ganze Bevölkerung ist gegen dich und du hast kein Geld mehr. Deine Amtszeit ist damit zu Ende.";
    public TextMeshProUGUI explainationText;


    private void Start()
    {
        int curState = PlayerPrefs.GetInt("currentStatebudget");
        int curScore = PlayerPrefs.GetInt("currentScore");

        if (curScore <= 0 && curState <=0)
        {
            explainationText.text = alleWerteSindNullText;
        }
        if (curScore <= 0 && curState > 0)
        {
            explainationText.text = scoreDownText;
        }
        if (curScore > 0 && curState <= 0)
        {
            explainationText.text = budgetDownText;
        }

        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("currentStatebudget");
    }

}
