using UnityEngine;

public class ScoreServiceForAnswer : MonoBehaviour
{
    public void ChangeValues(string playerPrefName, string playerPrefName2)
    {
        int curValue = PlayerPrefs.GetInt(playerPrefName);
        int addition = PlayerPrefs.GetInt(playerPrefName2 + PlayerPrefs.GetInt("clickedButtonID"));
        curValue += addition;
        PlayerPrefs.SetInt(playerPrefName, curValue);
    }
}
