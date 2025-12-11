using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Infokratie")]
public class QuestionsWithAnswers : ScriptableObject
{
    public string question;
    public string[] answers;
    public int idRightAnswer;
    public int[] happiness;
    public string video;
    public int[] budget;
}
