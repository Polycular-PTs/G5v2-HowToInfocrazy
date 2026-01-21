using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.WSA;
//using static System.IO.Enumeration.FileSystemEnumerable<TResult>;
//using static System.IO.Enumeration.FileSystemEnumerable<TResult>;

public enum Stufe { stufe1, stufe2, stufe3}

public class LeakEventManager : MonoBehaviour
{
    [Header("UI Office")]
    public GameObject happinessFill;
    public TextMeshProUGUI happinessText;

    public GameObject statebudgetFill;
    public TextMeshProUGUI statebudgetText;

    public GameObject oppositionFill;
    public TextMeshProUGUI oppositionText;

    public GameObject functionalityFill;
    public TextMeshProUGUI functionalityText;

    [Header("UI Leak")]
    public GameObject leakPanel; // wird aktiviert sobald Event startet
    public TextMeshProUGUI stufeText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI titleText;
    public UnityEngine.UI.Button[] answerButtons;
    public TextMeshProUGUI[] answerButtonsText;

    private int stufe;          // 1-3
    //Generate Stufe:
    private int stufeGeneratingMinimumInclusive = 1;
    private int stufeGeneratingMaximumExclusive = 4;
    private bool inputReceived;    // ob Spieler reagiert hat
    //private float maxTime = 10f;   // 10 Sekunden Reaktionszeit
    float waitTimeMin=4f;
    float waitTimeMax = 5f;

    private bool leakActive;
    public int id = 0;
    float chance = 0.3f; // 0.1 - 10% Chance
    public LeakData[] allLeaks;
    public LeakData[] allLeaksStufe1;
    public LeakData[] allLeaksStufe2;
    public LeakData[] allLeaksStufe3;

    public int happinessValueOfThisLeak;
    public int budgetValueOfThisLeak;
    public int oppositionValueOfThisLeak;
    public int functionalityValueOfThisLeak;

    public Color colorBG_ST;
    //stufe1
    //public int timeST1 = 30;
    //public int attentionPerSecondST1 = 10;
    //public int minusWhenTimeIsUpST1 = 50; //minus
    //private StufeVorlage stufe1;
    private StufeVorlage[] stufen = new StufeVorlage[3];

    //stufe2
    //public int timeST2 = 20;
    //public int attentionPerSecondST2 = 100;
    //public int minusWhenTimeIsUpST2 = 80;
    //private StufeVorlage stufe2;

    //stufe3
    //public int timeST3 = 10;
    //public int attentionPerSecondST3 = 1000;
    //public int minusWhenTimeIsUpST3 = 100;
    //private StufeVorlage stufe3;


    void Start()
    {
        for(int i=0; i<stufen.Length; i++)
        {
            stufen[i] = new StufeVorlage();
        }
        stufen[(int)Stufe.stufe1].InitiateStufeValues(30, 10, 50, 50);
        stufen[(int)Stufe.stufe2].InitiateStufeValues(20, 100, 80, 80);
        stufen[(int)Stufe.stufe3].InitiateStufeValues(10, 1000, 100, 100);

        // PlayerPrefs.SetInt("currentOpposition", 100);
        //PlayerPrefs.SetInt("currentFunctionality", 100);

        leakPanel.SetActive(false);
        StartCoroutine(LeakChecker());
        for(int a = 0; a<3; a++)
        {
            PlayerPrefs.DeleteKey("CurrentLeakID"+a+1);
        }
        //PlayerPrefs.DeleteKey("CurrentLeakID1");
        //PlayerPrefs.DeleteKey("CurrentLeakID2");
        //PlayerPrefs.DeleteKey("CurrentLeakID3");
    }

    IEnumerator LeakChecker()
    {
        while (true)
        {
            // Warte zufällige Zeit bevor geprüft wird
            float wait = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(wait);

            // Wahrscheinlichkeit, dass jetzt ein Leak passiert
            
            float randomValue = Random.value;
            //Debug.Log(randomValue);

            if (randomValue < chance)
            {
                TriggerLeakEvent();
            }
        }
    }

    private LeakData[] GetLeaksForStufe(int stufe) //bei der aktuellen Stufe wird dem currentLeakData die richtige Stufe zugewiesen und unten mit der jeweiligen ID den aktuellen Leak abgefragt.
    {
        switch (stufe)
        {
            case 1: return allLeaksStufe1;
            case 2: return allLeaksStufe2;
            case 3: return allLeaksStufe3;
            default: return allLeaksStufe1;
        }
    }

    // Wird von außen getriggert, z.B. durch Random-Event-System
    public void TriggerLeakEvent()
    {
        if (leakActive) return; // Sicherheitsblock

        leakActive = true;
        StartCoroutine(LeakEventRoutine());
    }

    private IEnumerator LeakEventRoutine()
    {
        // Reset
        inputReceived = false;

        // Generate new leak stufe
        stufe = Random.Range(stufeGeneratingMinimumInclusive, stufeGeneratingMaximumExclusive);
        leakPanel.SetActive(true);
        int[] timeTable = { stufen[(int)Stufe.stufe1].timeST, stufen[(int)Stufe.stufe2].timeST, stufen[(int)Stufe.stufe3].timeST }; //zuweisen der Zeit, die man bei den unterschiedlichen Stufen hat
        float timeLeft = timeTable[stufe - 1]; //je nachdem welche Stufe gerade ist, wird sie (-1, damit es mit 123 und 012 zusammenpasst) rausgesucht und der timeLeft variable zugewiesen 
        stufeText.text = "Stufe: " + stufe;


        for(int b=1; b < 4; b++)
        {
            if (stufe == b) //Abfrage, welche Stufe es ist
            {
                if (!PlayerPrefs.HasKey("CurrentLeakID" + stufe))
                {
                    
                    PlayerPrefs.SetInt("CurrentLeakID" + stufe, 0); //besetzen des CurrentLeakID der Stufe mit dem jeweiligen Fragenwert
                    id = PlayerPrefs.GetInt("CurrentLeakID" + stufe);
                }
                else
                {
                    id = PlayerPrefs.GetInt("CurrentLeakID" + stufe);
                    PlayerPrefs.DeleteKey("CurrentLeakID" + stufe);
                }

                LeakData[] currentLeakData = GetLeaksForStufe(stufe); //bei der aktuellen Stufe wird dem currentLeakData das richtige Array der richtigen Stufe zugewiesen und unten mit der jeweiligen ID den aktuellen Leak abgefragt.

                //if (allLeaks.Length > id) //besetzen des Leakfensters mit Werten, nur wenn die ID von der bestimmten Stufe unter der Länge von 1 ist, weil wir nicht mehr als 1 Scriptable Object haben. AllLeaks hat als Inhalt die aktuelle Menge an Leaks zur bestimmten Stufe.
                if (id <= currentLeakData.Length)
                {
                    titleText.text = currentLeakData[id].inhalt;
                    PlayerPrefs.SetInt("rightAnswerID", currentLeakData[id].idRightAnswer);
                    for (int a = 0; a < 4; a++)
                    {
                        answerButtonsText[a].text = currentLeakData[id].answers[a];
                    }
                }
            }
        }

        while (timeLeft > 0 && !inputReceived)
        {
            timeLeft -= Time.deltaTime; //die bestimmte Zeit (20s) wird minus der aktuellen Zeit/Frames gerechnet, nach 60 Frames -> 1s weniger
            countdownText.text = "Zeit: " + timeLeft.ToString("0.0") + "s";
            yield return null; //-> Pause bis zum nächsten Frame/1 Durchlauf pro Frame
        }

        // Timeout oder Spieler hat reagiert
        if (!inputReceived) //wenn kein Input kommt/Zeit abläuft
        {
            Debug.Log("Timeout → stärkster Negativeffekt!");
            ApplyEffects("Timeout");
            leakActive = false;
        }
        else
        {
            //Updaten, was bei PlayerDecision gesetzt wird

            WertUpdaten2("currentScore", true, happinessValueOfThisLeak, happinessText, happinessFill);
            WertUpdaten2("currentStatebudget", true, budgetValueOfThisLeak, statebudgetText, statebudgetFill);
            WertUpdaten2("currentOpposition", false, oppositionValueOfThisLeak, oppositionText, oppositionFill);
            WertUpdaten2("currentFunctionality", true, functionalityValueOfThisLeak, functionalityText, functionalityFill);

            leakActive = false;
        }
    }

    private void WertUpdaten2(string wert, bool addition, int formerValue, TextMeshProUGUI currentText, GameObject currentFill)
    {
        int curValue = PlayerPrefs.GetInt(wert);
        if (addition == true)
        {
            curValue += formerValue;
        }
        else
        {
            curValue -= formerValue;
        }
        PlayerPrefs.SetInt(wert, curValue);
        ChangeGraphic(curValue, currentFill.GetComponent<Transform>());
        currentText.text = curValue.ToString();
    }

    private void ChangeGraphic(int curValue, Transform fillTransform)
    {
        if (curValue < 100f)
        {
            Vector3 scale = fillTransform.localScale;
            scale.x = curValue / 100f;
            fillTransform.localScale = scale;
            Debug.Log("Graphic changed");
        }
    }

    // Wird von Buttons aufgerufen
    public void PlayerDecision(int buttonID)
    {
        if (inputReceived) return; // Nur 1x erlauben
        inputReceived = true;

        Debug.Log("Spieler wählte: " + buttonID);

        if (buttonID == PlayerPrefs.GetInt("rightAnswerID") + 1)
        {
            Debug.Log("Richtig");
            happinessValueOfThisLeak = 0;
            budgetValueOfThisLeak = 0;
            oppositionValueOfThisLeak = 0;
            functionalityValueOfThisLeak = 0;
        }
        else
        {
            Debug.Log("Falsch");
            if (stufe == 1)
            {
                Debug.Log(stufe);
                for(int i = 1; i < 5; i++)
                {
                    // wenn Button 1 geklickt, soll die Value von dem Leak von z.B. der Happiness vom Array auf Platz 0 bzw. 1/2/3 in den aktuellen Wert geladen werden.
                    if (buttonID == i)
                    {
                        happinessValueOfThisLeak = allLeaksStufe1[id].happiness[buttonID - 1];
                        budgetValueOfThisLeak = allLeaksStufe1[id].budget[buttonID - 1];
                        oppositionValueOfThisLeak = allLeaksStufe1[id].opposition[buttonID - 1];
                        functionalityValueOfThisLeak = allLeaksStufe1[id].functionality[buttonID - 1];
                    }
                }
                //Debug.Log("-10 bis -30 bei Zufriedenheit");
                //Debug.Log("-40 bei Budget");
                //Debug.Log("+10 bei Opposition");
                //Debug.Log("25 bei Funktionalität");
            }
            if (stufe == 2)
            {
                for (int i = 1; i < 5; i++)
                {
                    
                    if (buttonID == i)
                    {
                        happinessValueOfThisLeak = allLeaksStufe2[id].happiness[buttonID - 1];
                        budgetValueOfThisLeak = allLeaksStufe2[id].budget[buttonID - 1];
                        oppositionValueOfThisLeak = allLeaksStufe2[id].opposition[buttonID - 1];
                        functionalityValueOfThisLeak = allLeaksStufe2[id].functionality[buttonID - 1];
                    }
                }
                //Debug.Log("-40 bis -60 bei Zufriedenheit");
                //Debug.Log("-60 bis -80 bei Budget");
                //Debug.Log("+25 bei Opposition");
                //Debug.Log("50 bei Funktionalität");
            }
            if (stufe == 3)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (buttonID == i)
                    {
                        happinessValueOfThisLeak = allLeaksStufe3[id].happiness[buttonID - 1];
                        budgetValueOfThisLeak = allLeaksStufe3[id].budget[buttonID - 1];
                        oppositionValueOfThisLeak = allLeaksStufe3[id].opposition[buttonID - 1];
                        functionalityValueOfThisLeak = allLeaksStufe3[id].functionality[buttonID - 1];
                    }
                }

                //Debug.Log("-70 bis -90 bei Zufriedenheit");
                //Debug.Log("-80 bis -100 bei Budget");
                //Debug.Log("+50 bei Opposition");
                //Debug.Log("100 bei Funktionalität");
            }
            Debug.Log(happinessValueOfThisLeak);
            Debug.Log(budgetValueOfThisLeak);
            Debug.Log(oppositionValueOfThisLeak);
            Debug.Log(functionalityValueOfThisLeak);
        }

        leakPanel.SetActive(false);
        id += 1; //Steigerung des ID Wertes, damit andere Fragen kommen
        PlayerPrefs.SetInt("CurrentLeakID" + stufe, id);
    }





    private void ApplyEffects(string decision)
    {
        if (decision == "Timeout")
        {
            // Schlimmster Schaden
            if (stufe == 1)
            {
                happinessValueOfThisLeak = -stufen[(int)Stufe.stufe1].minusWhenTimeIsUpST;
                budgetValueOfThisLeak = -stufen[(int)Stufe.stufe1].minusWhenTimeIsUpST;
                oppositionValueOfThisLeak = stufen[(int)Stufe.stufe1].oppositionWhenTimeIsUpST;
                functionalityValueOfThisLeak = -stufen[(int)Stufe.stufe1].minusWhenTimeIsUpST;
            }
            if (stufe == 2)
            {
                happinessValueOfThisLeak = -stufen[(int)Stufe.stufe2].minusWhenTimeIsUpST;
                budgetValueOfThisLeak = -stufen[(int)Stufe.stufe2].minusWhenTimeIsUpST;
                oppositionValueOfThisLeak = stufen[(int)Stufe.stufe2].oppositionWhenTimeIsUpST;
                functionalityValueOfThisLeak = -stufen[(int)Stufe.stufe2].minusWhenTimeIsUpST;
            }
            if (stufe == 3)
            {
                happinessValueOfThisLeak = -stufen[(int)Stufe.stufe3].minusWhenTimeIsUpST;
                budgetValueOfThisLeak = -stufen[(int)Stufe.stufe3].minusWhenTimeIsUpST;
                oppositionValueOfThisLeak = stufen[(int)Stufe.stufe3].oppositionWhenTimeIsUpST;
                functionalityValueOfThisLeak = -stufen[(int)Stufe.stufe3].minusWhenTimeIsUpST;
            }

            int curScore = PlayerPrefs.GetInt("currentScore");
            curScore += happinessValueOfThisLeak;
            PlayerPrefs.SetInt("currentScore", curScore);

            int curStatebudget = PlayerPrefs.GetInt("currentStatebudget");
            curStatebudget += budgetValueOfThisLeak;
            PlayerPrefs.SetInt("currentStatebudget", curStatebudget);

            int curOpposition = PlayerPrefs.GetInt("currentOpposition");
            curOpposition += oppositionValueOfThisLeak;
            PlayerPrefs.SetInt("currentOpposition", curOpposition);
            Debug.Log("currentOppositionValue" + curOpposition);

            int curFunctionality = PlayerPrefs.GetInt("currentFunctionality");
            curFunctionality += functionalityValueOfThisLeak;
            PlayerPrefs.SetInt("currentFunctionality", curFunctionality);
            Debug.Log("CurrentFunctionalityValue" + curFunctionality);

            leakActive = false;

            if (curScore < 100f)
            {
                Transform fillTransform = happinessFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curScore / 100f;
                fillTransform.localScale = scale;
            }
            happinessText.text = curScore.ToString();

            if (curScore <= 0)
            {
                SceneManager.LoadScene("Defeat");
            }


            if (curStatebudget < 100f)
            {
                Transform fillTransform = statebudgetFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curStatebudget / 100f;
                fillTransform.localScale = scale;
            }
            statebudgetText.text = curStatebudget.ToString();

            if (curStatebudget <= 0)
            {
                Debug.Log("CurState ist Null");
                SceneManager.LoadScene("Defeat");
            }


            if (curOpposition < 100f)
            {
                Transform fillTransform = oppositionFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curOpposition / 100f;
                fillTransform.localScale = scale;
            }
            oppositionText.text = curOpposition.ToString();
            //GameOver für Opposition und Funktionalität fehlt noch


            if (curFunctionality < 100f)
            {
                Transform fillTransform = functionalityFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curFunctionality / 100f;
                fillTransform.localScale = scale;
            }
            functionalityText.text = curFunctionality.ToString();

           
        }
    }
}