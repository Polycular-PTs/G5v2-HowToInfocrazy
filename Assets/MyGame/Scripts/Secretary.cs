using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Secretary : MonoBehaviour
{
    [SerializeField]
    private int index;
    [SerializeField]
    private QuestionsWithAnswers currentQuestion;

    [SerializeField]
    private TextMeshProUGUI info;

    [SerializeField]
    private bool sameAttempt = false;

    void Start()
    {
        sameAttempt = HappinessManager.Instance.sameAttempt;

        if (!sameAttempt)
        {
            index = Random.Range(0,HappinessManager.Instance.questions.Count);
            HappinessManager.Instance.currentIndex = index;
            // currentQuestion = HappinessManager.Instance.questions[index];
            // HappinessManager.Instance.currentQuestion = currentQuestion;
        }

        currentQuestion = HappinessManager.Instance.questions[HappinessManager.Instance.currentIndex];
        HappinessManager.Instance.currentQuestion = currentQuestion;

        info.text = currentQuestion.question;
    }
}
