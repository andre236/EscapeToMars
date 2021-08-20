using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Managers;

public class EndingScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _fadeIn, _tksScreen;

    private Text _specialThanksTXT;
    private Button _muteButton;
    private Button _pauseButton;

    private void Awake()
    {
        StartCoroutine("CooldownToReference");
        _fadeIn = GameObject.Find("FadeIn");
        _tksScreen = GameObject.Find("TKSscreen");
        _specialThanksTXT = GameObject.Find("SpecialThanksTXT").GetComponent<Text>();
        _muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
        _pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
    }

    private void Start()
    {
        _fadeIn.SetActive(false);
        _tksScreen.SetActive(false);
        _specialThanksTXT.gameObject.SetActive(false);
    }

    void SettingStars()
    {
        int tempPhase = WhereIam.Instance.Phase - 2; // 1 ( Subtract "2" for Scenes: Main Scene, Shop Scene and Level Manager Scene.)
        int tempNexPhase = WhereIam.Instance.Phase - 1; // 2
        
        PlayerPrefs.SetInt("Level" + tempPhase, 1);
        if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 3)
        {
            PlayerPrefs.SetInt("StarsLevel" + tempPhase, 3);
        }

        PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("ShowCredits");
        }
    }


    IEnumerator CooldownToReference()
    {
        yield return new WaitForSeconds(2f);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator ShowCredits()
    {
        SettingStars();
        _player.GetComponent<PlayerMovement>().MoveRightPlayer();
        _player.GetComponent<PlayerMovement>().TurnOFFcontrol(true);
        _muteButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        AudioManager.Instance.StartFadeOut();
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.SetStandardBGMVolumeSound();
        AudioManager.Instance.PlayBackgroundMusicInTheEnd();
        yield return new WaitForSeconds(5f);
        _specialThanksTXT.gameObject.SetActive(true);
        yield return new WaitForSeconds(35f);
        _fadeIn.SetActive(true);
        yield return new WaitForSeconds(2f);
        _tksScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        _tksScreen.GetComponent<Animator>().SetBool("isFadeOut", true);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.StopCurrentBackgroundMusic();
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayBackgroundMusic(0);
        SceneManager.LoadScene(0);

    }


}
