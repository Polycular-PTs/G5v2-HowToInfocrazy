using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VideoPlayerControll : MonoBehaviour
{
    private VideoPlayer videoPlayer;

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

    private bool isDragging = false;
    private bool showEndscreen;

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
        videoPlayer.time = currentSlider.value * (float)videoPlayer.clip.length;
        isDragging = false;
    }

    public void OnSliderValueChanged()
    {
        if (isDragging)
        {
            videoPlayer.time = currentSlider.value * (float)videoPlayer.clip.length;
        }
    }

    private void UpdatePlayPauseUI()
    {
        if (videoPlayer.isPlaying)
            ShowPauseButton();
        else
            ShowPlayButton();
    }

    void Update()
    {
        HandleInput();

        UpdatePlaybackUI();

        HandleEndscreen();
    }

    private void HandleEndscreen()
    {
        if (showEndscreen == true)
        {
            Debug.Log("1");
            //string happiness = PlayerPrefs.GetInt("currentScore").ToString();
            string happinessAdded = PlayerPrefs.GetInt("CurrentHappiness" + PlayerPrefs.GetInt("clickedButtonID")).ToString();
            //string budget = PlayerPrefs.GetInt("currentStatebudget").ToString();
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

    private void UpdatePlaybackUI()
    {
        if (videoPlayer.isPlaying && !isDragging)
        {
            Debug.Log("A1");
            SetCurrentTimeUI();
            SetTotalTimeUI();
            PlayBarSlider();
            showEndscreen = false;
        }
        if (videoPlayer.isPlaying)
        {
            Debug.Log("A2");
            PauseButton.SetActive(true);
        }
        if (videoPlayer.isPaused && currentTime < videoPlayer.clip.length)
        {
            Debug.Log("A3");
            UpdatePlayPauseUI();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseOrPlay();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            backAFewSeconds();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            forwardAFewSeconds();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("A4");
        UpdatePlayPauseUI();
        RestartButton.SetActive(true);
        showEndscreen = true;
    }

    public void RestartedAction()
    {
        RestartButton.SetActive(false);
        videoPlayer.Play();
        Debug.Log("B1");
        showEndscreen = false;
    }

    public void PlayedAction()
    {
        videoPlayer.Play();
        ShowPauseButton();
    }

    public void PausedAction()
    {
        videoPlayer.Pause();
        ShowPlayButton();
    }

    public void backAFewSeconds()
    {
        if (videoPlayer.time > jumpBackSeconds)
        {
            Debug.Log("forward 5");
            double newTime = videoPlayer.time - jumpBackSeconds;
            videoPlayer.time = Mathf.Max(0, (float)newTime);

            currentSlider.value = (float)videoPlayer.time / totalLength;
        }
        else
        {
            double newTime = 0;
            videoPlayer.time = Mathf.Min((float)newTime, (float)videoPlayer.length);

            currentSlider.value = 0;
        }
    }
    public void forwardAFewSeconds()
    {
        if ((videoPlayer.clip.length - videoPlayer.time) > jumpForwardSeconds)
        {
            double newTime = videoPlayer.time + jumpForwardSeconds;
            videoPlayer.time = Mathf.Min((float)newTime, (float)videoPlayer.length);

            currentSlider.value = (float)videoPlayer.time / totalLength;
        }
        else
        {
            double newTime = videoPlayer.clip.length;
            videoPlayer.time = Mathf.Min((float)newTime, (float)videoPlayer.length);

            currentSlider.value = 1;
        }
    }

    public void PauseOrPlay()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            ShowPlayButton();
        }
        else if (videoPlayer.isPaused && currentTime < videoPlayer.clip.length)
        {
            videoPlayer.Play();
            ShowPauseButton();
        }
    }


    private void ShowPlayButton()
    {
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
    }
    private void ShowPauseButton()
    {
        PlayButton.SetActive(false);
        PauseButton.SetActive(true);
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
}
