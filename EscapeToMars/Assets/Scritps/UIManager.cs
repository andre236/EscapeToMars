using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{

    private GameObject _backgroundShadowImage;

    private GameObject _uiWinLevelGO;
    private GameObject _uiLoseLevelGO;
    private GameObject _uiPauseLevelGO;

    private Button _pauseButton;
    private Button _resumeButton;
    private Button _nextButton;
    private Button _restartWinButton;
    private Button _restartLoseButton;
    private Button _restartPauseButton;
    private Button _quitPauseButton;
    private Button _quitLoseButton;
    private Button _quitWinButton;

    private Animator _uiWinStarsAnim;

    public static UIManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;

    }

    private void Start()
    {
        GetData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GameStarted)
        {
            PauseGameUI();
        }
    }

    void Load(Scene scene, LoadSceneMode mode)
    {
        GetData();

    }

    void GetData()
    {
        if (WhereIam.instance.Phase != 1 && WhereIam.instance.Phase != 0 && WhereIam.instance.Phase != 2)
        {
            _uiLoseLevelGO = GameObject.Find("Canvas").transform.Find("UILoseLevel").gameObject;
            _uiWinLevelGO = GameObject.Find("Canvas").transform.Find("UIWinLevel").gameObject;
            _uiPauseLevelGO = GameObject.Find("Canvas").transform.Find("UIPauseMenu").gameObject;
            _backgroundShadowImage = GameObject.Find("Canvas").transform.Find("ShadowBackgroundUI").gameObject;

            _pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
            _resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
            _nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            _restartWinButton = GameObject.Find("RestartWinButton").GetComponent<Button>();
            _restartLoseButton = GameObject.Find("RestartLoseButton").GetComponent<Button>();
            _restartPauseButton = GameObject.Find("RestartPauseButton").GetComponent<Button>();
            _quitPauseButton = GameObject.Find("QuitPauseButton").GetComponent<Button>();
            _quitWinButton = GameObject.Find("QuitWinButton").GetComponent<Button>();
            _quitLoseButton = GameObject.Find("QuitLoseButton").GetComponent<Button>();

            _uiWinStarsAnim = _uiWinLevelGO.GetComponent<Animator>();

            _nextButton.onClick.AddListener(GameManager.Instance.NextPhase);
            _restartWinButton.onClick.AddListener(GameManager.Instance.PlayAgainPhase);
            _restartLoseButton.onClick.AddListener(GameManager.Instance.PlayAgainPhase);
            _restartPauseButton.onClick.AddListener(GameManager.Instance.PlayAgainPhase);

            _quitPauseButton.onClick.AddListener(GameManager.Instance.GoToLevelSelect);
            _quitWinButton.onClick.AddListener(GameManager.Instance.GoToLevelSelect);
            _quitLoseButton.onClick.AddListener(GameManager.Instance.GoToLevelSelect);

            _pauseButton.onClick.AddListener(PauseGameUI);
            _resumeButton.onClick.AddListener(ReturnFromPauseGame);

            TurnOffUI();
        }

    }

    public void GameOverUI()
    {
        StartCoroutine("CoolDownToShowLoseUI");
    }

    public void SuccessfullyLevelUI(int numberStars)
    {
        StartCoroutine("CooldownToShowUIwin", numberStars);
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        if (GameManager.Instance.Win == false)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(WhereIam.instance.Phase);
            ScoreManager.instance.ResetCurrentPointsPlayer();
        }
        Time.timeScale = 1;
    }

    public void PauseGameUI()
    {
        _uiPauseLevelGO.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _backgroundShadowImage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnFromPauseGame()
    {
        _uiPauseLevelGO.SetActive(false);
        Time.timeScale = 1;
        _pauseButton.gameObject.SetActive(true);
        _backgroundShadowImage.gameObject.SetActive(false);
    }

    public void TurnOffUI()
    {
        StartCoroutine("CoolDownTurnOffUI");
    }

    IEnumerator CooldownToShowUIwin(int numberStars)
    {
        yield return new WaitForSeconds(0.6f);
        _pauseButton.gameObject.SetActive(false);
        _uiWinLevelGO.SetActive(true);
        _uiWinStarsAnim.SetInteger("ShowNumberStars", numberStars);
        _backgroundShadowImage.gameObject.SetActive(true);
    }


    IEnumerator CoolDownToShowLoseUI()
    {
        yield return new WaitForSeconds(0.6f);
        _pauseButton.gameObject.SetActive(false);
        _uiLoseLevelGO.SetActive(true);
        _backgroundShadowImage.gameObject.SetActive(true);

    }

    IEnumerator CoolDownTurnOffUI()
    {
        yield return new WaitForSeconds(0.1f);

        _uiLoseLevelGO.SetActive(false);
        _uiWinLevelGO.SetActive(false);
        _uiPauseLevelGO.SetActive(false);
        _backgroundShadowImage.gameObject.SetActive(false);
    }



}
