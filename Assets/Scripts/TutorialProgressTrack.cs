using TMPro;
using UnityEngine;

public class TutorialProgressTrack : MonoBehaviour
{
    private int tutorialPoints;     //How many hints the user has found

    [Header("Butten that starts the Game")]
    [SerializeField] private GameObject buttonForStartingTheGame;       //The button the user has to press to get to the game

    [Header("Progress Bar Settings")]
    [SerializeField] private GameObject tutorialProgressSlider;     //The UI slider that shows how many hints of the total amount the user has already found

    [Header("How many hints the user has to find to be able to start the game")]
    [SerializeField] private float totalPointsForFullSlider;        //How many hints the user has to find to be able to start the game and have a full slider

    private void Start()
    {
        buttonForStartingTheGame.SetActive(false);

        tutorialPoints = 0;
        UpdateTutorialProgressUI();
    }

    public void UpdateTutorialProgressUI()
    {
        string progressText = "";
        float sliderValue = 0f;
        if (tutorialPoints < totalPointsForFullSlider)
        {
            sliderValue = tutorialPoints / totalPointsForFullSlider;
            progressText = "You have found " + tutorialPoints + " of " + totalPointsForFullSlider + " hints";
        }
        if (tutorialPoints >= totalPointsForFullSlider)
        {
            sliderValue = 1;
            progressText = "You have found all the hints. You can now start the game:";

            buttonForStartingTheGame.SetActive(true);
        }
        tutorialProgressSlider.GetComponent<UnityEngine.UI.Slider>().value = sliderValue;
        tutorialProgressSlider.GetComponentInChildren<TextMeshProUGUI>().text = progressText;
    }

    public void HintDiscovered()
    {
        tutorialPoints++;
        UpdateTutorialProgressUI();
    }
}
