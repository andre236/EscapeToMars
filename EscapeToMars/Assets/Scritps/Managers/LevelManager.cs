using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {

        [System.Serializable]
        public class Level
        {
            public string LevelText;
            public bool Able;
            public bool ActiveTXT;
            public int Unlocked;
            public int Stars;
        }

        [SerializeField]
        private GameObject _levelButton;
        [SerializeField]
        private Transform _parentButtons;

        List<GameObject> _allLevelGameObjects = new List<GameObject>();

        public List<Level> LevelList;
        public List<int> NumberAllowedLevels = new List<int>();

        void Awake()
        {
            Destroy(GameObject.Find("UIManager(Clone)"));
            Destroy(GameObject.Find("GameManager(Clone)"));
            Destroy(GameObject.Find("SkinManager(Clone)"));
        }

        void Start()
        {
            AddToList();
            Invoke(nameof(RestrictLevels), 0.01f);
        }



        void ClickLevel(string level)
        {
            AudioManager.Instance.StopCurrentBackgroundMusic();
            AudioManager.Instance.PlaySoundEffect(9);
            WhereIam.Instance.IsTransitinScenesTrue();
            StartCoroutine("StartingTransition", level);
        }

        void AddToList()
        {
            foreach (Level level in LevelList)
            {
                GameObject newLevelButton = Instantiate(_levelButton);
                LevelButton newLevel = newLevelButton.GetComponent<LevelButton>();
                newLevel.numberLevelTXT.text = level.LevelText;


                if (PlayerPrefs.GetInt("Level" + newLevel.numberLevelTXT.text) == 1)
                {
                    level.Unlocked = 1;
                    level.Able = true;
                    level.ActiveTXT = true;
                    level.Stars = 0;
                }

                if (PlayerPrefs.GetInt("StarsLevel" + newLevel.numberLevelTXT.text) == 1)
                {
                    level.Stars = 1;
                }
                if (PlayerPrefs.GetInt("StarsLevel" + newLevel.numberLevelTXT.text) == 2)
                {
                    level.Stars = 2;
                }
                if (PlayerPrefs.GetInt("StarsLevel" + newLevel.numberLevelTXT.text) >= 3)
                {
                    level.Stars = 3;
                }

                newLevel.transform.Find("AbleStar1").gameObject.SetActive(level.Stars >= 1);
                newLevel.transform.Find("AbleStar2").gameObject.SetActive(level.Stars >= 2);
                newLevel.transform.Find("AbleStar3").gameObject.SetActive(level.Stars >= 3);

                newLevel.Unlocked = level.Unlocked;
                newLevel.GetComponent<Button>().interactable = level.Able;
                newLevel.GetComponentInChildren<Text>().enabled = level.ActiveTXT;
                newLevel.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + newLevel.numberLevelTXT.text));

                newLevelButton.transform.SetParent(_parentButtons, false);
            }

        }

        [ContextMenu(nameof(RestrictLevels))]
        void RestrictLevels()
        {

            for (int buttonIndex = 0; buttonIndex < _parentButtons.childCount; buttonIndex++)
            {

                _allLevelGameObjects.Add(_parentButtons.GetChild(buttonIndex).gameObject);
            }

            foreach (GameObject level in _allLevelGameObjects)
            {
                foreach (int levelIndex in NumberAllowedLevels)
                {
                    if (level.GetComponentInChildren<Text>().text == levelIndex.ToString())
                    {
                        level.GetComponent<Button>().interactable = true;
                        level.GetComponentInChildren<Text>().enabled = true;
                        break;
                    }
                    else
                    {
                        level.GetComponent<Button>().interactable = false;
                        level.GetComponentInChildren<Text>().enabled = false;


                    }


                }
            }
        }

        IEnumerator StartingTransition(string level)
        {
            AudioManager.Instance.PlaySoundEffect(9);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(level);

        }

    }
}