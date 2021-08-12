using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSettings : MonoBehaviour
{
    private GameObject[] _bgms;
    [SerializeField]
    private Slider _audioSlider;
    [SerializeField]
    private Toggle _fullscreenToggle;
    [SerializeField]
    private Dropdown _resolutionDropDown;


    private void Start()
    {
        AudioManager.Instance.SetStandardVolumeSound();
        _audioSlider.value = PlayerPrefs.GetFloat("MasterVolume");
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
        AudioManager.Instance.SetStandardVolumeSound();
        _audioSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _fullscreenToggle.isOn = Screen.fullScreen;
    }


    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        for (int i = 0; i < _bgms.Length; i++)
        {
            _bgms[i].gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
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
                Screen.SetResolution(1536 , 864, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1440 , 810, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(1344 , 756, Screen.fullScreen);
                break;
            case 5:
                Screen.SetResolution(1152 , 648, Screen.fullScreen);
                break;
            case 6:
                Screen.SetResolution(960 , 540, Screen.fullScreen);
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
