using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeakEventManager_Stand_10Dez_V01 : MonoBehaviour
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
    private bool inputReceived;    // ob Spieler reagiert hat
    //private float maxTime = 10f;   // 10 Sekunden Reaktionszeit

    private bool leakActive;
    public int id = 0;
    public LeakData[] allLeaks;
    public LeakData[] allLeaksStufe1;
    public LeakData[] allLeaksStufe2;
    public LeakData[] allLeaksStufe3;

    //public int[] happinessValueOfThisLeak;
    //public int[] budgetValueOfThisLeak;
    //public int[] oppositionValueOfThisLeak;
    //public int[] functionalityValueOfThisLeak;
    public int happinessValueOfThisLeak;
    public int budgetValueOfThisLeak;
    public int oppositionValueOfThisLeak;
    public int functionalityValueOfThisLeak;

    //stufe1
    public int timeST1 = 30;
    public Color colorBG_ST1;
    public int attentionPerSecondST1 = 10;
    public int minusWhenTimeIsUpST1 = 50; //minus

    //stufe2
    public int timeST2 = 20;
    public Color colorBG_ST2;
    public int attentionPerSecondST2 = 100;
    public int minusWhenTimeIsUpST2 = 80;

    //stufe3
    public int timeST3 = 10;
    public Color colorBG_ST3;
    public int attentionPerSecondST3 = 1000;
    public int minusWhenTimeIsUpST3 = 100;


    void Start()
    {
        PlayerPrefs.SetInt("currentOpposition", 0);
        PlayerPrefs.SetInt("currentFunctionality", 0);

        leakPanel.SetActive(false);
        StartCoroutine(LeakChecker());
        PlayerPrefs.DeleteKey("CurrentLeakID1");
        PlayerPrefs.DeleteKey("CurrentLeakID2");
        PlayerPrefs.DeleteKey("CurrentLeakID3");
    }

    IEnumerator LeakChecker()
    {
        while (true)
        {
            // Warte zufällige Zeit bevor geprüft wird
            float wait = Random.Range(2f, 4f);
            yield return new WaitForSeconds(wait);


            // Wahrscheinlichkeit, dass jetzt ein Leak passiert
            float chance = 0.5f; // 0.1 - 10% Chance
            float randomValue = Random.value;
            //Debug.Log(randomValue);

            if (randomValue < chance)
            {
                TriggerLeakEvent();
            }
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
        stufe = Random.Range(1, 4);
        leakPanel.SetActive(true);
        int[] timeTable = { timeST1, timeST2, timeST3 };
        float timeLeft = timeTable[stufe - 1];
        stufeText.text = "Stufe: " + stufe;


        for (int b = 1; b < 4; b++)
        {

            if (stufe == b)
            {

                //Debug.Log(PlayerPrefs.GetInt("CurrentID"));
                if (!PlayerPrefs.HasKey("CurrentLeakID" + stufe))
                {

                    PlayerPrefs.SetInt("CurrentLeakID" + stufe, 0);
                    id = PlayerPrefs.GetInt("CurrentLeakID" + stufe);
                }
                else
                {
                    id = PlayerPrefs.GetInt("CurrentLeakID" + stufe);
                    PlayerPrefs.DeleteKey("CurrentLeakID" + stufe);
                }

                LeakData[] currentLeakData = allLeaks;
                if (stufe == 1)
                {
                    currentLeakData = allLeaksStufe1;
                    Debug.Log("AllLeakStufe1");
                }
                if (stufe == 2)
                {
                    currentLeakData = allLeaksStufe2;
                    Debug.Log("AllLeakStufe2");
                }
                if (stufe == 3)
                {
                    currentLeakData = allLeaksStufe3;
                    Debug.Log("AllLeakStufe3");
                }

                if (allLeaks.Length > id)
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
            timeLeft -= Time.deltaTime;
            countdownText.text = "Zeit: " + timeLeft.ToString("0.0") + "s";
            yield return null;
        }

        // Timeout oder Spieler hat reagiert
        if (!inputReceived)
        {
            Debug.Log("Timeout → stärkster Negativeffekt!");
            ApplyEffects("Timeout");
            leakActive = false;
        }
        else
        {
            //Updaten, was bei PlayerDecision gesetzt wird
            int curScore = PlayerPrefs.GetInt("currentScore");
            int addition = happinessValueOfThisLeak;
            curScore += addition;
            PlayerPrefs.SetInt("currentScore", curScore);

            int curStatebudget = PlayerPrefs.GetInt("currentStatebudget");
            int addition2 = budgetValueOfThisLeak;
            curStatebudget += addition2;
            PlayerPrefs.SetInt("currentStatebudget", curStatebudget);

            int curOpposition = PlayerPrefs.GetInt("currentOpposition");
            int addition3 = oppositionValueOfThisLeak;
            curOpposition += addition3;
            PlayerPrefs.SetInt("currentOpposition", curOpposition);
            Debug.Log("currentOppositionValue" + curOpposition);

            int curFunctionality = PlayerPrefs.GetInt("currentFunctionality");
            int addition4 = functionalityValueOfThisLeak;
            curFunctionality += addition4;
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

        leakPanel.SetActive(false);
        //id += 1;
        PlayerPrefs.SetInt("CurrentLeakID" + stufe, id);
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
        }
        else
        {
            Debug.Log("Falsch");
            if (stufe == 1)
            {
                Debug.Log(stufe);
                for (int i = 1; i < 5; i++)
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
            //Debug.Log(happinessValueOfThisLeak);
            //Debug.Log(budgetValueOfThisLeak);
            //Debug.Log(oppositionValueOfThisLeak);
            //Debug.Log(functionalityValueOfThisLeak);
        }

        leakPanel.SetActive(false);
        id += 1;
        PlayerPrefs.SetInt("CurrentLeakID" + stufe, id);

        //string decision="";
        //ApplyEffects(decision);
    }





    private void ApplyEffects(string decision)
    {
        // Hier bestimmst du das richtige Verhalten.
        // Beispiel: "Transparenz" ist die infokratische Lösung.

        //int effect = 0;

        //if (decision == "Transparenz")
        //{
        //    // Gute Reaktion → kleiner Schaden
        //    effect = -(stufe / 2);
        //}
        if (decision == "Timeout")
        {
            // Schlimmster Schaden
            if (stufe == 1)
            {
                happinessValueOfThisLeak = -minusWhenTimeIsUpST1;
                budgetValueOfThisLeak = -minusWhenTimeIsUpST1;
                oppositionValueOfThisLeak = 50;
                functionalityValueOfThisLeak = -minusWhenTimeIsUpST1;
            }
            if (stufe == 2)
            {
                happinessValueOfThisLeak = -minusWhenTimeIsUpST2;
                budgetValueOfThisLeak = -minusWhenTimeIsUpST2;
                oppositionValueOfThisLeak = 80;
                functionalityValueOfThisLeak = -minusWhenTimeIsUpST2;
            }
            if (stufe == 3)
            {
                happinessValueOfThisLeak = -minusWhenTimeIsUpST3;
                budgetValueOfThisLeak = -minusWhenTimeIsUpST3;
                oppositionValueOfThisLeak = 100;
                functionalityValueOfThisLeak = -minusWhenTimeIsUpST3;
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

            //if (curOpposition <= 0)
            //{
            //    Debug.Log("CurOpp ist Null");
            //    SceneManager.LoadScene("Defeat");
            //}



            if (curFunctionality < 100f)
            {
                Transform fillTransform = functionalityFill.GetComponent<Transform>();
                Vector3 scale = fillTransform.localScale;
                scale.x = curFunctionality / 100f;
                fillTransform.localScale = scale;
            }
            functionalityText.text = curFunctionality.ToString();

            //if (curFunctionality <= 0)
            //{
            //    Debug.Log("CurFunct ist Null");
            //    SceneManager.LoadScene("Defeat");
            //}


            //effect = stufe * 2;
        }
        //else
        //{
        //    // Normale schlechte Reaktion
        //    effect = stufe;
        //}

        //Debug.Log("Auswirkung auf Reputation: " + effect);
        // TODO: Wert verändern, z. B.
        // PlayerStats.Reputation += effect;
    }
}