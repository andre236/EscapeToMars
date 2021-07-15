using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject _firstStepGO, _secondStepGO, _lastStepGO;
    private GameObject _flagGO;
    private GameObject _player;
    private Transform _initialPosition;

    public static GameManager Instance;

    public bool GameStarted { get; private set; }
    public bool Win { get; private set; }
    public bool IsLoadingStep { get; private set; }

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

    void Load(Scene scene, LoadSceneMode sceneMode)
    {
        GetData();
    }


    public void GetData()
    {
        if (WhereIam.Instance.Phase != 1 && WhereIam.Instance.Phase != 0 && WhereIam.Instance.Phase != 2)
        {
            _initialPosition = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
            Instantiate(SkinManager.instance.SkinsPrefabs[PlayerPrefs.GetInt("IndexSkin")], _initialPosition);
            _player = GameObject.FindGameObjectWithTag("Player");
            _flagGO = GameObject.FindGameObjectWithTag("Flag");
            _firstStepGO = GameObject.Find("FirstStep");
            _secondStepGO = GameObject.Find("SecondStep");
            _lastStepGO = GameObject.Find("LastStep");

            InitializingGame();
        } 
        
    }

    public void InitializingGame()
    {
        AudioManager.Instance.CheckBGMisPlaying();
        if (AudioManager.Instance.IsPlayingBGM == false)
        {
            AudioManager.Instance.PlayBackgroundMusic(Random.Range(1, 4));
        }
        _flagGO.SetActive(false);
        _secondStepGO.SetActive(false);
        _lastStepGO.SetActive(false);
        StartCoroutine("CooldownLoadingStep");
    }

    public void GameOver()
    {
        if (GameStarted == true && Win == false)
        {
            UIManager.Instance.GameOverUI();
        }
        GameStarted = false;
    }

    public void SuccessfullyLevel(int numberStars)
    {
        UIManager.Instance.SuccessfullyLevelUI(numberStars);
        GameStarted = false;
        Win = true;
        Time.timeScale = 1;
    }

    public void NextPhase()
    {
        if (Win)
        {
            int temp = WhereIam.Instance.Phase + 1;
            ScoreManager.instance.ResetCurrentPointsPlayer();
            AudioManager.Instance.StopCurrentBackgroundMusic();
            AudioManager.Instance.PlayBackgroundMusic(Random.Range(1, 4));
            Time.timeScale = 1;
            GameStarted = false;
            Win = false;
            SceneManager.LoadScene(temp);
        }
    }

    public void PlayAgainPhase()
    {
        Time.timeScale = 1;
        GameStarted = false;
        Win = false;
        AudioManager.Instance.CheckBGMisPlaying();
        if (AudioManager.Instance.IsPlayingBGM == false)
        {
            AudioManager.Instance.PlayBackgroundMusic(Random.Range(1, 4));
        }

        SceneManager.LoadScene(WhereIam.Instance.Phase);
        ScoreManager.instance.ResetCurrentPointsPlayer();
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1;
        GameStarted = false;
        Win = false;

        ScoreManager.instance.ResetCurrentPointsPlayer();
        AudioManager.Instance.StopCurrentBackgroundMusic();
        AudioManager.Instance.PlayBackgroundMusic(3);
        SceneManager.LoadScene("SELECT_MAP");
    }

    public void TriggerFlag()
    {
        if (ScoreManager.instance.CurrentPointsPlayer == ScoreManager.instance.TotalPointsA)
        {
            AudioManager.Instance.PlaySoundEffect(5);
            _flagGO.SetActive(true);
            _secondStepGO.SetActive(true);
            ScoreManager.instance.ReferencePointsB();
            Destroy(_firstStepGO);
            IsLoadingStep = true;
            StartCoroutine("CooldownToLoadNextStep");
        }
    }

    public void SecondStar()
    {
        if (ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB)
        {
            _lastStepGO.SetActive(true);
            ScoreManager.instance.ReferencePointsC();
            Destroy(_secondStepGO);
            IsLoadingStep = true;
            StartCoroutine("CooldownToLoadNextStep");
        }
    }

    IEnumerator CooldownToLoadNextStep()
    {
        yield return new WaitForSeconds(1f);
        IsLoadingStep = false;
    }

    IEnumerator CooldownLoadingStep()
    {
        yield return new WaitForSeconds(3f);
        IsLoadingStep = false;
        Win = false;
        if (_player.transform.position == _initialPosition.position)
        {
            GameStarted = true;
        }
        else
        {
            GetData();
        }

    }

}
