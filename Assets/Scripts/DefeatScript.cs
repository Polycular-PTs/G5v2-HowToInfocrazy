using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatScript : MonoBehaviour
{
    string scoreDownText = "Deine Bevölkerung ist unzufrieden und hat dein Büro gestürmt.Deine Amtszeit ist damit zu Ende.";
    string budgetDownText = "Dein Budget ist leer und du kannst deinen Staat nicht mehr aufrecht erhalten. Deine Amtszeit ist damit zu Ende.";
    string functionalityDownText = "Die bürger sehen keinen Sinn mehr für ein korruptes System wie ihres zu arbeiten, jeder Arbeiter legt die Werkzeuge nieder und geht schlafen";
    string oppositionDownText = "Die Bürger sehen in ihren Politischen gegnern mehr als in ihnen, in den nächsten wahlen erzielen Sie ganze 0,5% und werden von der gesellschaft als extremistischer faschist verspottet";
    string alleWerteSindNullText = "Dein ganzer Staat ist zusammengefallen. Deine ganze Bevölkerung ist gegen dich und du hast kein Geld mehr. Deine Amtszeit ist damit zu Ende.";
    public TextMeshProUGUI explainationText;


    private void Start()
    {
        int curState = PlayerPrefs.GetInt("currentStatebudget");
        int curScore = PlayerPrefs.GetInt("currentScore");
        int curOpp = PlayerPrefs.GetInt("currentOpposition");
        int curFunc = PlayerPrefs.GetInt("currentFunctionality");

        if (curScore <= 0 && curState <=0 && curOpp <= 0 && curFunc <= 0)
        {
            explainationText.text = alleWerteSindNullText;
        }
        if (curScore <= 0 && curOpp <= 0 && curFunc <= 0 &&  curState > 0)
        {
            explainationText.text = scoreDownText;
        }
        if (curScore > 0 && curOpp > 0 && curFunc > 0 && curState <= 0 )
        {
            explainationText.text = budgetDownText;
        }
        if (curScore > 0 && curOpp > 0 && curFunc <= 0 && curState > 0)
        {
            explainationText.text = functionalityDownText;
        }
        if (curScore > 0 && curOpp <= 0 && curFunc > 0 && curState > 0)
        {
            explainationText.text = oppositionDownText;
        }

        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("currentStatebudget");
        PlayerPrefs.DeleteKey("currentOpposition");
        PlayerPrefs.DeleteKey("currentFuntionality");

    }

}
