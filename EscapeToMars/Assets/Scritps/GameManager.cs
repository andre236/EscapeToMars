using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private GameObject _firstStepGO, _secondStepGO, _lastStepGO;
    private GameObject _flagGO;
    private GameObject _player;
    private Transform _initialPosition;

    private bool _playerDead;

    public static GameManager instance;
    
    public bool GameStarted;
    public bool Win;
    public bool isLoadingStep;

    //public bool isTeste;

    void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
           
        }
        else {
            Destroy(this.gameObject);
        }

        

        SceneManager.sceneLoaded += Load;
        

    }

    void Load(Scene scene, LoadSceneMode sceneMode) {
        GetData();

    }
    

    public void GetData() {
        if (WhereIam.instance.Phase != 1 && WhereIam.instance.Phase != 0) {
            _initialPosition = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
            Instantiate(SkinManager.instance.SkinsPrefabs[PlayerPrefs.GetInt("IndexSkin")], _initialPosition);
            _player = GameObject.FindGameObjectWithTag("Player");
            _flagGO = GameObject.Find("Flag");
            _firstStepGO = GameObject.Find("FirstStep");
            _secondStepGO = GameObject.Find("SecondStep");
            _lastStepGO = GameObject.Find("LastStep");
            
            StartGame();
        }
    }

    public void StartGame() {
        AudioManager.instance.GetBackgroundMusic(Random.Range(1, 2));
        _flagGO.SetActive(false);
        _secondStepGO.SetActive(false);
        _lastStepGO.SetActive(false);
        StartCoroutine("CooldownLoadingStep");
      
    }
    
    
    public void GameOver() {
        if(GameStarted == true && Win == false) {
            UIManager.instance.GameOverUI();
        }
        GameStarted = false;
    }

    public void SuccessfullyLevel(int numberStars) {
        UIManager.instance.SuccessfullyLevelUI(numberStars);
        GameStarted = false;
        Win = true;
        Time.timeScale = 1;
    }

    public void NextPhase() {
        if (Win) {
            int temp = WhereIam.instance.Phase + 1;
            ScoreManager.instance.ResetCurrentPointsPlayer();
            Time.timeScale = 1;
            GameStarted = false;
            SceneManager.LoadScene(temp);
            
            
        }
    }

    public void PlayAgainPhase() {
        Time.timeScale = 1;
        GameStarted = false;
        AudioManager.instance.GetBackgroundMusic(Random.Range(1, 2));
        SceneManager.LoadScene(WhereIam.instance.Phase);
        ScoreManager.instance.ResetCurrentPointsPlayer();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelect() {
        Time.timeScale = 1;
        ScoreManager.instance.ResetCurrentPointsPlayer();
        AudioManager.instance.StopCurrentBackgroundMusic();
        AudioManager.instance.GetBackgroundMusic(3);
        SceneManager.LoadScene("SELECT_MAP");



    }



    public void TriggerFlag() {

        if (ScoreManager.instance.CurrentPointsPlayer == ScoreManager.instance.TotalPointsA) {
            AudioManager.instance.GetSoundEffect(5);
            _flagGO.SetActive(true);
            _secondStepGO.SetActive(true);
            ScoreManager.instance.ReferencePointsB();
            Destroy(_firstStepGO);
            isLoadingStep = true;
            StartCoroutine("CooldownToLoadNextStep");
        }

    }

    public void SecondStar() {
        if (ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB) {
            _lastStepGO.SetActive(true);
            ScoreManager.instance.ReferencePointsC();
            Destroy(_secondStepGO);
            isLoadingStep = true;
            StartCoroutine("CooldownToLoadNextStep");

        }
    }

   
    IEnumerator CooldownToLoadNextStep() {
        yield return new WaitForSeconds(1f);
        isLoadingStep = false;

    }

    IEnumerator CooldownLoadingStep() {
        yield return new WaitForSeconds(3f);
        isLoadingStep = false;
        Win = false;
        if(_player.transform.position == _initialPosition.position)
        {
            GameStarted = true;
        }
        else
        {
            GetData();
        }

    }

}
