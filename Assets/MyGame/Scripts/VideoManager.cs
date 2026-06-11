using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer vp;

    [Header("Debugging")]
    public string videoName;
    public int frames;
    public int currentFrame;
    public float normalized;
    public bool isPrepared;

    [Header("UI Elemente")]
    public Slider slider;
    

    void OnEnable()
    {
        vp.prepareCompleted += LoadUI;
    }

    void OnDisable()
    {
        vp.prepareCompleted -= LoadUI;
    }

    void Start()
    {
        HappinessManager.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        videoName = HappinessManager.Instance.currentQuestion.video;
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName) + ".mp4";//HappinessManager.Instance.currentQuestion.video;
        vp.Play();        
    }

    public void LoadUI(VideoPlayer vp)
    {
        frames =  (int)vp.frameCount;
        isPrepared = true;
    }

    void Update()
    {
        if (isPrepared)
        {
            currentFrame = (int)vp.frame;
            normalized = Mathf.InverseLerp(0,frames,currentFrame);
            slider.value = normalized;
        }
    }

    public void ActivateHappiness()
    {
        HappinessManager.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
