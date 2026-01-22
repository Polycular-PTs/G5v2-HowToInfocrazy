using UnityEngine;

[CreateAssetMenu(fileName = "NewLeak", menuName = "LeakInfokratie")]
public class LeakData : ScriptableObject
{
    public string inhalt;
    public string[] answers;
    public int idRightAnswer;
    public int[] happiness;
    public int[] budget;
    public int[] opposition;
    public int[] functionality;
}