using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeakManager : MonoBehaviour
{
    public int currentStage = 0;
    public List<LeakDataStage> leakstages;


    public List<LeakData> stageLeaks;
    
    [Header("Debugging")]
    public LeakData currentLeak;
    public float cooldown = 15;
    public float timer = 30;

    [Header("UI Elements")]
    public Button[] answerButtons;
    public TMP_Text leakQuestion;


    void Start()
    {
        if (leakstages.Count > 0)
        {
           stageLeaks = new List<LeakData>(leakstages[currentStage].leaks);
           currentLeak = stageLeaks[Random.Range(0,stageLeaks.Count)];
           //stageLeaks.Remove(currentLeak);
        }
        
        SetUIElements();
        
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            TimeOut();
            timer = 15;
        }
    }

    public void TimeOut()
    {
        HappinessManager.Instance.UpdateValues(
            leakstages[currentStage].happiness,
            leakstages[currentStage].budget,
            leakstages[currentStage].opposition,
            leakstages[currentStage].functionality);
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
                HappinessManager.Instance.UpdateValues(
                    currentLeak.happiness[0],
                    currentLeak.happiness[1],
                    currentLeak.happiness[2],
                    currentLeak.happiness[3]);
                break;
            case 1:
                HappinessManager.Instance.UpdateValues(
                    currentLeak.budget[0],
                    currentLeak.budget[1],
                    currentLeak.budget[2],
                    currentLeak.budget[3]);
                break;
            case 2:
                HappinessManager.Instance.UpdateValues(
                    currentLeak.opposition[0],
                    currentLeak.opposition[1],
                    currentLeak.opposition[2],
                    currentLeak.opposition[3]);
                break;
            case 3:
                HappinessManager.Instance.UpdateValues(
                    currentLeak.functionality[0],
                    currentLeak.functionality[1],
                    currentLeak.functionality[2],
                    currentLeak.functionality[3]);
                break;
            
        }
    }
}
