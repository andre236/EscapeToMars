using UnityEngine;

public class ConfigSettings : MonoBehaviour
{

    private void Start()
    {
        SetResolutionDefault();
    }

    public void SetResolutionDefault()
    {
        if(!PlayerPrefs.HasKey("Resolution1920") || !PlayerPrefs.HasKey("Resolution1440"))
        {
            PlayerPrefs.SetInt("Resolution1920", 1);
            Screen.SetResolution(1920, 1080, true, 60);
        }
        
        if(PlayerPrefs.GetInt("Resolution1920") == 1)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen, 60);
        }
        else if (PlayerPrefs.GetInt("Resolution1440") == 1)
        {
            Screen.SetResolution(1440, 810, Screen.fullScreen, 60);
        }
    }

    public void SetResolution1920()
    {
        Debug.Log("Setado 1920");
        PlayerPrefs.SetInt("Resolution1920", 1);
        PlayerPrefs.SetInt("Resolution1440", 0);
        Screen.SetResolution(1920, 1080, Screen.fullScreen, 60);

    }

    public void SetResolution1440()
    {
        Debug.Log("Setado 1440");
        PlayerPrefs.SetInt("Resolution1920", 0);
        PlayerPrefs.SetInt("Resolution1440", 1);
        Screen.SetResolution(1440, 810, Screen.fullScreen, 60);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


}
