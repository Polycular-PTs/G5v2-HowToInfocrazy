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

    void Start()
    {
        index = Random.Range(0,HappinessManager.Instance.questions.Count);
        HappinessManager.Instance.currentIndex = index;
        currentQuestion = HappinessManager.Instance.questions[index];
        HappinessManager.Instance.currentQuestion = currentQuestion;

        info.text = currentQuestion.question;
    }
}
