using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{

    public QuestionsWithAnswers currentQuestion;
    public Button[] answerButtons;
    public int rightAnswer;

    [Header("Events")]
    public UnityEvent OnRightAnswer;
    public UnityEvent OnWrongAnswer;

    void Start()
    {
        currentQuestion = HappinessManager.Instance.currentQuestion;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
        }

        rightAnswer = currentQuestion.idRightAnswer;
    }

    public void ChooseAnswer(int index)
    {
        UpdateValue(index);

        if (index == rightAnswer)
        {
            Debug.Log("Right Answer!");
            HappinessManager.Instance.questions.Remove(currentQuestion);
            HappinessManager.Instance.sameAttempt = false;
            OnRightAnswer?.Invoke();
        }
        else
        {
            Debug.Log("Wrong Answer!");
            HappinessManager.Instance.sameAttempt = true;
            OnWrongAnswer?.Invoke();
        }
    }

    void UpdateValue(int index)
    {
        switch (index)
        {
            case 0:
                HappinessManager.Instance.happiness += currentQuestion.happiness[0];
                HappinessManager.Instance.budget += currentQuestion.happiness[1];
                HappinessManager.Instance.opposition += currentQuestion.happiness[2];
                HappinessManager.Instance.functionality += currentQuestion.happiness[3];
                break;

            case 1:
                HappinessManager.Instance.happiness += currentQuestion.budget[0];
                HappinessManager.Instance.budget += currentQuestion.budget[1];
                HappinessManager.Instance.opposition += currentQuestion.budget[2];
                HappinessManager.Instance.functionality += currentQuestion.budget[3];
                break;
            
            case 2:
                HappinessManager.Instance.happiness += currentQuestion.opposition[0];
                HappinessManager.Instance.budget += currentQuestion.opposition[1];
                HappinessManager.Instance.opposition += currentQuestion.opposition[2];
                HappinessManager.Instance.functionality += currentQuestion.opposition[3];
                break;

            case 3:
                HappinessManager.Instance.happiness += currentQuestion.functionality[0];
                HappinessManager.Instance.budget += currentQuestion.functionality[1];
                HappinessManager.Instance.opposition += currentQuestion.functionality[2];
                HappinessManager.Instance.functionality += currentQuestion.functionality[3];
                break;
        }
    }
}
