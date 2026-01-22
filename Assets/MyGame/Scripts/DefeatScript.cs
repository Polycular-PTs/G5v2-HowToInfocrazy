using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using NUnit.Framework.Interfaces;

public class DefeatScript : MonoBehaviour
{
    int curState;
    int curScore;
    int curOpp;
    int curFunc;

    string scoreDownText = "Deine Bevölkerung ist unzufrieden und hat dein Büro gestürmt. Deine Amtszeit ist damit zu Ende.";
    string budgetDownText = "Dein Budget ist leer und du kannst deinen Staat nicht mehr aufrecht erhalten. Deine Amtszeit ist damit zu Ende.";
    string functionalityDownText = "Die bürger sehen keinen Sinn mehr für ein korruptes System wie ihres zu arbeiten, jeder Arbeiter legt die Werkzeuge nieder und geht schlafen";
    string oppositionDownText = "Die Bürger sehen in ihren politischen Gegnern mehr als in ihnen, in den nächsten Wahlen erzielen Sie ganze 0,5% und werden von der Gesellschaft als extremistischer Faschist verspottet";
    string alleWerteSindNullText = "Dein ganzer Staat ist zusammengefallen. Deine ganze Bevölkerung ist gegen dich und du hast kein Geld mehr. Deine Amtszeit ist damit zu Ende.";
    public TextMeshProUGUI explainationText;


    private void Start()
    {
        curState = PlayerPrefs.GetInt("currentStatebudget");
        curScore = PlayerPrefs.GetInt("currentScore");
        curOpp = PlayerPrefs.GetInt("currentOpposition");
        curFunc = PlayerPrefs.GetInt("currentFunctionality");

        GetDefeatText(curScore, curState, curOpp, curFunc);

        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("currentStatebudget");
        PlayerPrefs.DeleteKey("currentOpposition");
        PlayerPrefs.DeleteKey("currentFunctionality");
    }

    private void GetDefeatText(int curScore, int curState, int curOpp, int curFunc)
    {
        if (AlleWerteNull()) //wenn jeder Wert gleichzeitig null wird, dann ist der ganze Staat "zusammengefallen"
        {
            ZuweisenDesExplainTexts(alleWerteSindNullText);
        }
        else if (curScore <= 0) 
        {
            ZuweisenDesExplainTexts(scoreDownText);
        }
        else if(curState <= 0)
        {
            ZuweisenDesExplainTexts(budgetDownText);
        }
        else if (curOpp <= 0)
        {
            ZuweisenDesExplainTexts(oppositionDownText);
        }
        else if (curFunc <= 0)
        {
            ZuweisenDesExplainTexts(functionalityDownText);
        }
    }

    private bool AlleWerteNull()
    {
        return curScore <= 0 && curState <= 0 && curOpp <= 0 && curFunc <= 0;
    }

    private void ZuweisenDesExplainTexts(string text)
    {
        explainationText.text = text;
    }

}
