using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

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

    [Header("Questions")]
    public List<QuestionsWithAnswers> questions;
    public QuestionsWithAnswers currentQuestion;
    public int currentIndex;
    public bool sameAttempt = false;

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
    }

    void Update()
    {
        UpdateValues();
        UpdateFillbars();
    }

    void UpdateValues()
    {
        fillbars[0].value = happiness;
        fillbars[1].value = budget;
        fillbars[2].value = opposition;
        fillbars[3].value = functionality;
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
