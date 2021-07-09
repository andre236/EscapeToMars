
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {


    private float _volumeTemp = 0.001f;
    private bool _isFadeOut = false;

    private AudioSource[] _backgroundMusics;
    private AudioSource[] _soundsEffects;

    [SerializeField]
    private Button _muteSoundButton;
    private Animator _muteSoundAnim;



    public bool IsMuted { get; private set; } = false;

    public static AudioManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);          
        }
        else {
            Destroy(gameObject);
        }


        _backgroundMusics = GameObject.Find("BackgroundMusics").GetComponentsInChildren<AudioSource>();
        _soundsEffects = GameObject.Find("SoundEffects").GetComponentsInChildren<AudioSource>();

        SceneManager.sceneLoaded += Load;


        DontDestroyOnLoad(_muteSoundButton);

    }

    private void Start()
    {
        PlayBackgroundMusic(0);
    }

    void Load(Scene scene, LoadSceneMode loadSceneMode)
    {
        _muteSoundButton = GameObject.Find("MuteButton").GetComponent<Button>();
        _muteSoundAnim = _muteSoundButton.GetComponent<Animator>();
        _muteSoundAnim.SetBool("isMuted", IsMuted);
        _muteSoundButton.onClick.AddListener(MuteDesmuteAllSounds);
        

    }

    void Update()
    {
        _muteSoundAnim.SetBool("isMuted", IsMuted);

        if (_isFadeOut && _volumeTemp > 0)
        {
            _volumeTemp -= Time.deltaTime * 0.1f;
            Debug.Log(_volumeTemp);
        }
        else
        {
            _isFadeOut = false;
            _volumeTemp = 0.1f;
        }
    }

    public void PlaySoundEffect(int indexSFX, float pitchSFX = 1, ulong timeToPlay = 0) {
        _soundsEffects[indexSFX + 1].pitch = pitchSFX;
        _soundsEffects[indexSFX + 1].Play();
    }


    public void PlayBackgroundMusic(int indexBGM) {
        
        for(int i = 1; i < _backgroundMusics.Length; i++)
        {
            _backgroundMusics[i].Stop();
        }
        _backgroundMusics[indexBGM + 1].Play();
        _backgroundMusics[indexBGM + 1].mute = IsMuted;
        _backgroundMusics[indexBGM + 1].pitch = 1f;

    }

    public void PlayBackgroundMusicInTheEnd(int indexBGM)
    {
        for (int i = 1; i < _backgroundMusics.Length; i++)
        {
            _backgroundMusics[i].Stop();
        }
        _backgroundMusics[indexBGM + 1].Play();
        _backgroundMusics[indexBGM + 1].mute = IsMuted;
        _backgroundMusics[indexBGM + 1].pitch = 0.8f;
    }

    public void StopCurrentBackgroundMusic() {
        for (int i = 1; i < _backgroundMusics.Length; i++)
        {
            _backgroundMusics[i].Stop();
        }
    }

    public void StopFadeOutCurrentBackgroundMusic()
    {
        float CurrentVolume = _backgroundMusics[1].volume;
        _backgroundMusics[2].volume = CurrentVolume * _volumeTemp;
        _isFadeOut = true;
    }

    public void SetStandardVolumeSound()
    {
        for(int i = 1; i < 5; i++)
        {
            _backgroundMusics[i].volume = 0.25f;
        }
    }

    public void MuteDesmuteAllSounds() {
    

        for (int i = 0; i < _soundsEffects.Length; i++) {
            if (_soundsEffects[i].mute == false) {
                _soundsEffects[i].mute = true;
                IsMuted = true;
            }
            else {
                _soundsEffects[i].mute = false;
                IsMuted = false;
            }
        }

        for (int i = 0; i < _backgroundMusics.Length; i++) {
            if (_backgroundMusics[i].mute == false) {
                _backgroundMusics[i].mute = true;
            }
            else {
                _backgroundMusics[i].mute = false;
            }
        }

        _muteSoundAnim.SetBool("isMuted", IsMuted);

    }

    public void MuteDesmuteSoundEffect() {
        for (int i = 0; i < _soundsEffects.Length; i++) {
            if (_soundsEffects[i].mute) {
                _soundsEffects[i].mute = false;
            }
            else {
                _soundsEffects[i].mute = true;
            }
        }

    }

    public void MuteDesmuteBackgroundMusic() {
        for (int i = 0; i < _backgroundMusics.Length; i++) {
            if (_backgroundMusics[i].mute) {
                _backgroundMusics[i].mute = false;
            }
            else {
                _backgroundMusics[i].mute = true;
            }
        }

    }



}
