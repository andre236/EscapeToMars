using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Configurations
{
    public class ConfigSettings : MonoBehaviour
    {
        private GameObject[] _bgms;
        private GameObject[] _sfxs;
        [SerializeField]
        private Slider _audioBGMSlider;
        [SerializeField]
        private Slider _audioSFXslider;
        [SerializeField]
        private Toggle _fullscreenToggle;
        [SerializeField]
        private Dropdown _resolutionDropDown;


        private void Start()
        {
            AudioManager.Instance.SetStandardBGMVolumeSound();
            AudioManager.Instance.SetStandardSFXVolumeSound();
            _audioBGMSlider.value = PlayerPrefs.GetFloat("MasterBGMVolume");
            _audioSFXslider.value = PlayerPrefs.GetFloat("MasterSFXVolume");
            _fullscreenToggle.isOn = Screen.fullScreen;
            if (!PlayerPrefs.HasKey("ResolutionDropDown"))
            {
                PlayerPrefs.SetInt("ResolutionDropDown", 4);
                _resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionDropDown");
            }
            else
            {
                _resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionDropDown");
            }
        }

        private void OnEnable()
        {
            _bgms = GameObject.FindGameObjectsWithTag("BGM");
            _sfxs = GameObject.FindGameObjectsWithTag("SFX");
            AudioManager.Instance.SetStandardBGMVolumeSound();
            AudioManager.Instance.SetStandardSFXVolumeSound();
            _audioBGMSlider.value = PlayerPrefs.GetFloat("MasterBGMVolume");
            _audioSFXslider.value = PlayerPrefs.GetFloat("MasterSFXVolume");
        
            _fullscreenToggle.isOn = Screen.fullScreen;
        }


        public void SetBGMvolume(float volume)
        {
            PlayerPrefs.SetFloat("MasterBGMVolume", volume);
            for (int i = 0; i < _bgms.Length; i++)
            {
                _bgms[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterBGMVolume");
            }
        }

        public void SetSFXvolume(float volume)
        {
            PlayerPrefs.SetFloat("MasterSFXVolume", volume);
            for (int i = 0; i < _sfxs.Length; i++)
            {
                _sfxs[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterSFXVolume");
            }
        }

        public void SetResolution(int resolutionIndex)
        {
            PlayerPrefs.SetInt("ResolutionDropDown", resolutionIndex);
            _resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionDropDown");

            switch (resolutionIndex)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case 1:
                    Screen.SetResolution(1728, 972, Screen.fullScreen);
                    break;
                case 2:
                    Screen.SetResolution(1536, 864, Screen.fullScreen);
                    break;
                case 3:
                    Screen.SetResolution(1440, 810, Screen.fullScreen);
                    break;
                case 4:
                    Screen.SetResolution(1344, 756, Screen.fullScreen);
                    break;
                case 5:
                    Screen.SetResolution(1152, 648, Screen.fullScreen);
                    break;
                case 6:
                    Screen.SetResolution(960, 540, Screen.fullScreen);
                    break;
                default:
                    Screen.SetResolution(1440, 810, Screen.fullScreen);
                    break;
            }
        }

        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }


    }
}