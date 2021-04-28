using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCredits : MonoBehaviour
{
    public static EventCredits instance;


    private bool _isScaleChanging;

    private Vector3 _scaleChange;

    private GameObject _differentsPlayer;
    private GameObject _cameraMain;
    private GameObject _player;
    private GameObject _tksScreen;
    private GameObject _creditsSpecialThanks;

    private Animator _fadeIn;

    private FollowCamera _followCamera;
    private PlayerMovement _playerMovement;

    public bool FinalCredits { get; private set; } = false;

    private void Awake()
    {
        _cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        _followCamera = _cameraMain.GetComponent<FollowCamera>();
        _differentsPlayer = GameObject.Find("DifferentsPlayer");
        _fadeIn = GameObject.Find("FadeIn").GetComponent<Animator>();
        _tksScreen = GameObject.Find("TKSscreen");
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _creditsSpecialThanks = GameObject.Find("SpecialThanksTXT");
    }

    private void Start()
    {
        _differentsPlayer.SetActive(false);
        _followCamera.enabled = false;
        _scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        _isScaleChanging = false;
        _fadeIn.gameObject.SetActive(false);
        _tksScreen.SetActive(false);
        _creditsSpecialThanks.SetActive(false);
    }

    private void FixedUpdate()
    {
        ChangingScale();
    }


    void InitializingTheGameOver()
    {

        FinalCredits = true;



        PinCameraToPlayer();
        //Sumir a música V
        AudioManager.instance.StopFadeOutCurrentBackgroundMusic();
        StartCoroutine("CooldownToPlayBGM");
        //Tocar uma música V
        _playerMovement.MoveRightPlayer();
        _playerMovement.MoveToDirection("Right");
        _playerMovement.TurnOFFcontrol(true);
        StartCoroutine("CooldownToShowDifferentPlayer");
        StartCoroutine("CooldownToGetOff");
        //Distanciar a camera dos players V
        //dar um Fade in com uma tela preta V
        //Surgir uma imagem aos poucos com os agradecimentos "Thanks for Playing". V
        //Gravar info de apenas aparecer 1 vez.

    }

    void GettingThreeStars()
    {
        int tempPhase = WhereIam.instance.Phase - 1; // 1
        int tempNexPhase = WhereIam.instance.Phase + 0; // 2

        PlayerPrefs.SetInt("Level" + tempPhase, 1);
        if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 3)
        {
            PlayerPrefs.SetInt("StarsLevel" + tempPhase, 3);
        }
        PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
    }

    void PinCameraToPlayer()
    {
        _followCamera.enabled = true;
    }

    void ChangingScale()
    {
        if (_isScaleChanging && _player.transform.localScale.x > 0 && _player.transform.localScale.y > 0)
        {
            _player.transform.localScale += _scaleChange;
            _fadeIn.gameObject.SetActive(true);
            StartCoroutine("ShowScreenTKS");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InitializingTheGameOver();
        }
    }

    IEnumerator CooldownToShowDifferentPlayer()
    {
        yield return new WaitForSeconds(10);
        _differentsPlayer.SetActive(true);
        StartCoroutine("CooldownToShowCredits");
    }

    IEnumerator CooldownToShowCredits()
    {
        yield return new WaitForSeconds(4);
        _creditsSpecialThanks.SetActive(true);
    }

    IEnumerator CooldownToGetOff()
    {
        yield return new WaitForSeconds(43);
        _isScaleChanging = true;
    }

    IEnumerator ShowScreenTKS()
    {
        yield return new WaitForSeconds(3);
        _tksScreen.SetActive(true);
        StartCoroutine("FadeOUTScreenTKS");
    }

    IEnumerator FadeOUTScreenTKS()
    {
        yield return new WaitForSeconds(10);
        _tksScreen.GetComponent<Animator>().SetBool("isFadeOut", true);
        AudioManager.instance.StopCurrentBackgroundMusic();
        StartCoroutine("LoadSceneCooldown");
    }

    IEnumerator LoadSceneCooldown()
    {
        FinalCredits = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MAIN_SCENE");
    }

    IEnumerator CooldownToPlayBGM()
    {
        yield return new WaitForSeconds(3);
        AudioManager.instance.GetBackgroundMusicInTheEnd(0);
        AudioManager.instance.SetStandardVolumeSound();
    }
}
