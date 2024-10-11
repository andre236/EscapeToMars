using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
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
        }

        public void SettingAchievementKill()
        {
            if (PlayerPrefs.GetInt("Kills") > 0)
            {
                SteamUserStats.SetAchievement("ACH_ONE_LESS");
                SteamUserStats.StoreStats();
                Debug.Log("Menos 1: Conquista Feita!");
            }

            if (PlayerPrefs.GetInt("Kills") > 99)
            {
                SteamUserStats.SetAchievement("ACH_ISGETTINGSAFE");
                SteamUserStats.StoreStats();
                Debug.Log("Matou 100!: Conquista Feita!");
            }
        }

        public void SettingAchievementsLevels()
        {

            if (PlayerPrefs.GetInt("Level26") == 1)
            {
                SteamUserStats.SetAchievement("ACH_BEATING");
                SteamUserStats.StoreStats();
                Debug.Log("Zerou!: Finalizado o ultimo level.");
            }

            if (PlayerPrefs.GetInt("Level26") == 1)
            {
                SteamUserStats.SetAchievement("ACH_HELLMOD");
                SteamUserStats.StoreStats();
                Debug.Log("Hellmode Liberado!: Finalizado o ultimo level.");
            }
        }

        public void SetAchieveOneStar()
        {
            List<int> listOneStarPrefs = new List<int>();

            for (int i = 1; i <= 30; i++)
            {
                if (PlayerPrefs.GetInt("StarsLevel" + i) > 0)
                {
                    listOneStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                }
            }

            if (listOneStarPrefs.Count >= 30)
            {
                SteamUserStats.SetAchievement("ACH_ONE_STAR");
                SteamUserStats.StoreStats();
            }

        }

        public void SetAchieveTwoStars()
        {
            List<int> listTwoStarPrefs = new List<int>();

            for (int i = 1; i <= 30; i++)
            {
                if (PlayerPrefs.GetInt("StarsLevel" + i) > 1)
                {
                    listTwoStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                }
            }

            if (listTwoStarPrefs.Count >= 30)
            {
                SteamUserStats.SetAchievement("ACH_TWO_STARS");
                SteamUserStats.StoreStats();
            }
        }

        public void SetAchieveThreeStars()
        {
            List<int> listThreeStarPrefs = new List<int>();

            for (int i = 1; i <= 30; i++)
            {
                if (PlayerPrefs.GetInt("StarsLevel" + i) == 3)
                {
                    listThreeStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                }
            }

            if (listThreeStarPrefs.Count >= 30)
            {
                SteamUserStats.SetAchievement("ACH_THREE_STARS");
                SteamUserStats.StoreStats();
            }
        }

    }
}