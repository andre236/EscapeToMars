using Steamworks;
using UnityEngine;

public class SteamAchievement : MonoBehaviour
{
    public static SteamAchievement Instance;

    private void Awake()
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
    }

    void Update()
    {
        if (!SteamManager.Initialized) { return; }

        SettingAchievements();

    }

    void SettingAchievements()
    {
        if (PlayerPrefs.GetInt("Kills") > 0)
        {
            SteamUserStats.SetAchievement("ACH_ONE_LESS");
            SteamUserStats.StoreStats();
        }

        if (PlayerPrefs.GetInt("Level29") == 1)
        {
            SteamUserStats.SetAchievement("ACH_BEATING");
            SteamUserStats.StoreStats();
        }

        if (PlayerPrefs.GetInt("Level26") == 1)
        {
            SteamUserStats.SetAchievement("ACH_BEATING");
            SteamUserStats.StoreStats();
        }


    }
}
