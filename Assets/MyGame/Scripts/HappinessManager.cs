using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HappinessManager : MonoBehaviour
{
    [Header("Main Stats")]
    public int happiness;
    public int budget;
    public int opposition;
    public int functionality;

    [Header("Fillbars")]
    public Fillbars[] fillbars;

    public static HappinessManager Instance;
    public GameObject menu;

    [Header("Questions")]
    public List<QuestionsWithAnswers> questions;
    public QuestionsWithAnswers currentQuestion;
    public int currentIndex;
    public bool sameAttempt = false;

    [Header("Events")]
    public static Action OnLost;
    public static Action OnReset;

    void Awake()
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
        UpdateValues(happiness, budget, opposition, functionality);
    }

    void Update()
    {
        //UpdateValues(happiness,budget,opposition,functionality);
        UpdateFillbars();

        ToggleMenu();
    }

    public void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //print(menu.activeSelf);
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                menu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool CheckValues()
    {
        if (happiness <= 0 ||
            budget <= 0     ||
            opposition <= 0 ||
            functionality <= 0)
        {
            OnLost?.Invoke();
            return false;
            //HappinessManager.Instance = null;
            //Destroy(this.gameObject);
        }

        return true;
    }

    public bool UpdateValues(int h, int  b, int o, int f)
    {
        happiness = Mathf.Clamp(h,0,100);
        budget = Mathf.Clamp(b, 0, 100);
        opposition = Mathf.Clamp(o, 0, 100);
        functionality = Mathf.Clamp(f, 0, 100);

        fillbars[0].value = happiness;
        fillbars[1].value = budget;
        fillbars[2].value = opposition;
        fillbars[3].value = functionality;

        return CheckValues();
    }

    public bool AddValues(int h, int b, int o, int f)
    {
        happiness += h;
        happiness = Mathf.Clamp(happiness, 0, 100);
        budget += b;
        budget = Mathf.Clamp(budget, 0, 100);
        opposition += o;
        opposition = Mathf.Clamp(opposition, 0, 100);
        functionality += f;
        functionality = Mathf.Clamp(functionality, 0, 100);

        fillbars[0].value = happiness;
        fillbars[1].value = budget;
        fillbars[2].value = opposition;
        fillbars[3].value = functionality;

        return CheckValues();
    }

    void UpdateFillbars()
    {
        for (int i = 0; i < fillbars.Length; i++)
        {
            fillbars[i].titleText.text = fillbars[i].name;
            float value = Mathf.InverseLerp(0,100,fillbars[i].value);
            fillbars[i].fill.fillAmount = value;
            fillbars[i].valueText.text = Mathf.Clamp(fillbars[i].value,0,100).ToString();
        }
    }

    public void Restart()
    {
        if (SceneManager.GetActiveScene().ToString() != "01_Office")
        {
            SceneManager.LoadScene("01_Office");
        }
        UpdateValues(100,100,100,100);
        OnReset?.Invoke();

        sameAttempt = false;
        currentIndex = 0;
        Time.timeScale = 1;
    }
}

[Serializable]
public class Fillbars
{
    public string name;
    public int value;
    public Image fill;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI valueText;
}
