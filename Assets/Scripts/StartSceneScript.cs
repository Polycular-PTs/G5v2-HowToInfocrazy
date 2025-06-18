using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.DeleteKey("CurrentID");
        PlayerPrefs.DeleteKey("currentStatebudget");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Office");
    }
}
