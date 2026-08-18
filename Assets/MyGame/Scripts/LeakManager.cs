using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeakManager : MonoBehaviour
{
    public int currentStage = 0;
    public List<LeakDataStage> leakstages;
    public bool activeLeaks;

    public List<LeakData> stageLeaks;
    
    [Header("Debugging")]
    public LeakData currentLeak;
    public float timeToNextLeak = 15;
    public float timeToAnswer = 5;
    public float elapsedTime;

    [Header("UI Elements")]
    public Button[] answerButtons;
    public TMP_Text leakQuestion;
    public GameObject leakEventScreen;
    public Image timeOutDisplay;
    public TMP_Text levelDisplay;
    public Toggle toggle;

    public static LeakManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Restart();
    }

    private void OnEnable()
    {
        HappinessManager.OnLost += DropBelowZero;
        HappinessManager.OnReset += Restart;
        Quiz.OnWrongAnswer += Pause;
        Quiz.OnRightAnswer += Pause;
    }

    private void OnDisable()
    {
        HappinessManager.OnLost -= DropBelowZero;
        HappinessManager.OnReset -= Restart;
        Quiz.OnWrongAnswer -= Pause;
        Quiz.OnRightAnswer -= Pause;
    }

    public void Pause()
    {
        StopAllCoroutines();
    }

    public void Resume()
    {
        Restart();
    }

    public void ToggleActive()
    {
        activeLeaks = toggle.isOn;
        if (activeLeaks)
        {
            Restart();
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public void Restart()
    {
        StopAllCoroutines();
        if (activeLeaks)
        {
            if (leakstages.Count > 0)
            {
                stageLeaks = new List<LeakData>(leakstages[currentStage].leaks);
                currentLeak = stageLeaks[Random.Range(0, stageLeaks.Count)];
                //stageLeaks.Remove(currentLeak);
            }

            //SetUIElements();
            leakEventScreen.SetActive(false);
            StartCoroutine(CountdownToLeak());
        }

    }

    public void DropBelowZero()
    {
        SceneManager.LoadScene("Defeat");
    }


    IEnumerator CountdownToLeak()
    {
        yield return new WaitForSeconds(timeToNextLeak);
        leakEventScreen.SetActive(true);
        SetUIElements();

        elapsedTime = 0;

        while (elapsedTime < timeToAnswer)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float norm = Mathf.InverseLerp(timeToAnswer, 0, elapsedTime);
            timeOutDisplay.fillAmount = norm;
            timeOutDisplay.color = Color.Lerp(Color.red, Color.white, norm);
            yield return null;
        }

        bool notLost = TimeOut();
        ResetLeak(notLost);
   
    }

    public bool TimeOut()
    {
            bool notZero = 
            HappinessManager.Instance.UpdateValues(
            leakstages[currentStage].happiness,
            leakstages[currentStage].budget,
            leakstages[currentStage].opposition,
            leakstages[currentStage].functionality);

        return notZero;
    }

    public void ResetLeak(bool onContinue)
    {
        StopAllCoroutines();
        leakEventScreen.SetActive(false);

        if (onContinue)
        {
            currentStage++;

            if (leakstages.Count > 0 && currentStage < leakstages.Count)
            {
                stageLeaks = new List<LeakData>(leakstages[currentStage].leaks);
                currentLeak = stageLeaks[Random.Range(0, stageLeaks.Count)];
                //stageLeaks.Remove(currentLeak);
                levelDisplay.text = "STUFE " + (currentStage + 1);

                StartCoroutine(CountdownToLeak());
            }
        }
        else
        {
            currentStage = 0;

            if (leakstages.Count > 0 && currentStage < leakstages.Count)
            {
                stageLeaks = new List<LeakData>(leakstages[currentStage].leaks);
                currentLeak = stageLeaks[Random.Range(0, stageLeaks.Count)];
                levelDisplay.text = "STUFE " + (currentStage + 1);
            }
        }



    }

    public void SetUIElements()
    {
        leakQuestion.text = currentLeak.inhalt;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentLeak.answers[i];
        }
    }

    public void SelectOption(int index)
    {
        switch (index)
        {
            case 0:
                HappinessManager.Instance.AddValues(
                    currentLeak.happiness[0],
                    currentLeak.happiness[1],
                    currentLeak.happiness[2],
                    currentLeak.happiness[3]);
                break;
            case 1:
                HappinessManager.Instance.AddValues(
                    currentLeak.budget[0],
                    currentLeak.budget[1],
                    currentLeak.budget[2],
                    currentLeak.budget[3]);
                break;
            case 2:
                HappinessManager.Instance.AddValues(
                    currentLeak.opposition[0],
                    currentLeak.opposition[1],
                    currentLeak.opposition[2],
                    currentLeak.opposition[3]);
                break;
            case 3:
                HappinessManager.Instance.AddValues(
                    currentLeak.functionality[0],
                    currentLeak.functionality[1],
                    currentLeak.functionality[2],
                    currentLeak.functionality[3]);
                break;
            
        }

        ResetLeak(HappinessManager.Instance.CheckValues());
    }
}
