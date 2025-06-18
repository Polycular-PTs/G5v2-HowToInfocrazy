using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerManagerScript : MonoBehaviour
{
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    public GameObject answer4;

    public string textOfButtonAnswer1;
    public string textOfButtonAnswer2;
    public string textOfButtonAnswer3;
    public string textOfButtonAnswer4;

    private string tagOfButtonPlayerChose;

    public string rightAnswerSceneName;
    public string wrongAnswerSceneName1;
    public string wrongAnswerSceneName2;
    public string wrongAnswerSceneName3;
    public string wrongAnswerSceneName4;

    [SerializeField]
    private float delayBeforeLoadingNextScene;

    void Start()
    {
        answer1.GetComponentInChildren<TextMeshProUGUI>().text = textOfButtonAnswer1;
        answer2.GetComponentInChildren<TextMeshProUGUI>().text = textOfButtonAnswer2;
        answer3.GetComponentInChildren<TextMeshProUGUI>().text = textOfButtonAnswer3;
        answer4.GetComponentInChildren<TextMeshProUGUI>().text = textOfButtonAnswer4;
    }

    void Update()
    {
        CheckIfAnswerIsRight();
        CheckIfAnswerIsWrong();
    }

    private void LoadeNextScene_rightAnswer()
    {
        SceneManager.LoadScene(rightAnswerSceneName);
    }

    private void LoadeBeforeScene_wrongAnswer1()
    {
        SceneManager.LoadScene(wrongAnswerSceneName1);
        Debug.Log(wrongAnswerSceneName1 + " should be loaded");
    }
    private void LoadeBeforeScene_wrongAnswer2()
    {
        SceneManager.LoadScene(wrongAnswerSceneName2);
        Debug.Log(wrongAnswerSceneName2 + " should be loaded");
    }
    private void LoadeBeforeScene_wrongAnswer3()
    {
        SceneManager.LoadScene(wrongAnswerSceneName3);
        Debug.Log(wrongAnswerSceneName3 + " should be loaded");
    }
    private void LoadeBeforeScene_wrongAwnser4()
    {
        SceneManager.LoadScene(wrongAnswerSceneName4);
        Debug.Log(wrongAnswerSceneName4 + " should be loaded");
    }

    private void CheckIfAnswerIsRight()
    {
        if (answer1.GetComponent<ButtonClickDetect>().clicked && answer1.tag == "true")
        {
            Invoke(nameof(LoadeNextScene_rightAnswer), delayBeforeLoadingNextScene);
            Debug.Log("1 Answer is true");
            answer1.GetComponent<ButtonClickDetect>().clicked = false;
        }
        if (answer2.GetComponent<ButtonClickDetect>().clicked && answer2.tag == "true")
        {
            Invoke(nameof(LoadeNextScene_rightAnswer), delayBeforeLoadingNextScene);
            Debug.Log("2 Answer is true");
            answer2.GetComponent<ButtonClickDetect>().clicked = false;
        }
        if (answer3.GetComponent<ButtonClickDetect>().clicked && answer3.tag == "true")
        {
            Invoke(nameof(LoadeNextScene_rightAnswer), delayBeforeLoadingNextScene);
            Debug.Log("3 Answer is true");
            answer3.GetComponent<ButtonClickDetect>().clicked = false;
        }
        if (answer4.GetComponent<ButtonClickDetect>().clicked && answer4.tag == "true")
        {
            Invoke(nameof(LoadeNextScene_rightAnswer), delayBeforeLoadingNextScene);
            Debug.Log("4 Answer is true");
            answer4.GetComponent<ButtonClickDetect>().clicked = false;
        }
    }

    private void CheckIfAnswerIsWrong()
    {
        if (answer1.GetComponent<ButtonClickDetect>().clicked && answer1.tag == "false")
        {
            Invoke(nameof(LoadeBeforeScene_wrongAnswer1), delayBeforeLoadingNextScene);
            Debug.Log("1 Answer is false");
            answer1.GetComponent<ButtonClickDetect>().clicked = false;
        }
        else if (answer2.GetComponent<ButtonClickDetect>().clicked && answer2.tag == "false")
        {
            Invoke(nameof(LoadeBeforeScene_wrongAnswer2), delayBeforeLoadingNextScene);
            Debug.Log("2 Answer is false");
            answer2.GetComponent<ButtonClickDetect>().clicked = false;
        }
        else if (answer3.GetComponent<ButtonClickDetect>().clicked && answer3.tag == "false")
        {
            Invoke(nameof(LoadeBeforeScene_wrongAnswer3), delayBeforeLoadingNextScene);
            Debug.Log("3 Answer is false");
            answer3.GetComponent<ButtonClickDetect>().clicked = false;
        }
        else if (answer4.GetComponent<ButtonClickDetect>().clicked && answer4.tag == "false")
        {
            Invoke(nameof(LoadeBeforeScene_wrongAwnser4), delayBeforeLoadingNextScene);
            Debug.Log("4 Answer is false");
            answer4.GetComponent<ButtonClickDetect>().clicked = false;
        }
    }
}
