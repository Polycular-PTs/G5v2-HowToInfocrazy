using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;
using JetBrains.Annotations;
using TMPro;

public class VideoPlayerControll : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public Material playButtonMat;
    public Material pauseButtonMat;
    public Text currentMinutes;
    public Text currentSeconds;
    public Text totalMinutes;
    public Text totalSeconds;

    private float currentTime;
    private float totalLength;
    public Slider currentSlider;

    [SerializeField]
    private float jumpBackSeconds;
    [SerializeField]
    private float jumpForwardSeconds;

    private bool toggle = false;

    [SerializeField]
    private float timeBetweenClicks;

    private bool isDragging = false;
    private bool videoStopTextGo;

    public TextMeshProUGUI budgetUndZufriedenheitText;
    public GameObject Background;

    public GameObject PlayButton;
    public GameObject PauseButton;
    public GameObject RestartButton;



    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        Background.SetActive(false);
        videoPlayer.Play();
        budgetUndZufriedenheitText.gameObject.SetActive(false);
        
    }

    public void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    public void OnSliderBeginDrag()
    {
        isDragging = true;
    }

    public void OnSliderEndDrag()
    {
        isDragging = false;
        videoPlayer.time = currentSlider.value * (float)videoPlayer.clip.length;
    }

    public void OnSliderValueChanged()
    {
        if (isDragging)
        {
            videoPlayer.time = currentSlider.value * (float)videoPlayer.clip.length;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && toggle == false)
        {
           PauseOrPlay();
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            backAFewSeconds();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            forwardAFewSeconds();
        }


        if (videoPlayer.isPlaying && !isDragging)
        {
            Debug.Log("A1");
            SetCurrentTimeUI();
            SetTotalTimeUI();
            PlayBarSlider();
            videoStopTextGo = false;
        }
        if (videoPlayer.isPlaying)
        {

            Debug.Log("A2");
            PauseButton.SetActive(true);
        }
        if (videoPlayer.isPaused && currentTime < videoPlayer.clip.length)
        {
            Debug.Log("A3");
            PlayButton.SetActive(true);
        }

        if (videoStopTextGo == true)
        {
            Debug.Log("1");
            string happiness = PlayerPrefs.GetInt("currentScore").ToString();
            string happinessAdded = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID")).ToString();
            string budget = PlayerPrefs.GetInt("currentStatebudget").ToString();
            string budgetAdded = PlayerPrefs.GetInt("CurrentBudget" + PlayerPrefs.GetInt("clickedButtonID")).ToString();
            Debug.Log(happinessAdded + budgetAdded);
            budgetUndZufriedenheitText.text = "Du verlierst " + happinessAdded + "% an Zufriedenheit der Bevölkerung und " + budgetAdded + "% vom Budget";
            Debug.Log(budgetUndZufriedenheitText);
            budgetUndZufriedenheitText.gameObject.SetActive(true);
            Debug.Log("2");
            Background.SetActive(true);
            Debug.Log("3");
            
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("A4");
        PlayButton.SetActive(false);
        PauseButton.SetActive(false);
        RestartButton.SetActive(true);
        videoStopTextGo = true;
    }

    public void RestartedAction()
    {
        RestartButton.SetActive(false);
        videoPlayer.Play();
        Debug.Log("B1");
        videoStopTextGo = false;
    }

    public void PlayedAction()
    {
        PlayButton.SetActive(false);
        videoPlayer.Play();
        Debug.Log("B2");
    }

    public void PausedAction()
    {
        PauseButton.SetActive(false);
        videoPlayer.Pause();
        Debug.Log("B2");
    }

    public void backAFewSeconds()
    {
        if (videoPlayer.time > jumpBackSeconds)
        {
            double newTime = videoPlayer.time - jumpBackSeconds;
            videoPlayer.time = Mathf.Max(0, (float)newTime);

            currentSlider.value = (currentTime - 5f) / totalLength;
        }
    }
    public void forwardAFewSeconds()
    {
        double newTime = videoPlayer.time + jumpForwardSeconds;
        videoPlayer.time = Mathf.Min((float)newTime, (float)videoPlayer.length);

        currentSlider.value = (currentTime + 5f) / totalLength;
    }

    public void PauseOrPlay()
    {
        toggle = true;

        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            ShowPlayButton();
            Debug.Log("Video should have stopped");
        }
        else
        {
            videoPlayer.Play();
            ShowPauseButton();
            SetTotalTimeUI();
            Debug.Log("Video should play");
        }

        Invoke(nameof(ToggleReset), timeBetweenClicks); //nameof = Konvertierung zu String
    }



    private void ShowPlayButton()
    {

    }
    private void ShowPauseButton()
    {

    }

    private void SetCurrentTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.time / 60).ToString("00");
        string seconds = ((int)videoPlayer.time % 60).ToString("00");

        currentMinutes.text = minutes + ":";
        currentSeconds.text = seconds;
    }
    private void SetTotalTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int)videoPlayer.clip.length % 60).ToString("00");

        totalMinutes.text = minutes + ":";
        totalSeconds.text = seconds;
    }

    private void PlayBarSlider()
    {
        totalLength = Mathf.Floor((int)videoPlayer.clip.length);
        currentTime = Mathf.Floor((int)videoPlayer.time);

        currentSlider.value = currentTime / totalLength;
    }

    private void ToggleReset()
    {
        toggle = false;
    }
}
