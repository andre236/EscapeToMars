using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        public List<Level> LevelList;

        void Awake()
        {
            Destroy(GameObject.Find("UIManager(Clone)"));
            Destroy(GameObject.Find("GameManager(Clone)"));
            Destroy(GameObject.Find("SkinManager(Clone)"));
        }

        void Start()
        {
            AddToList();
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
                GameObject newLevelButton = Instantiate(_levelButton) as GameObject;
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

        IEnumerator StartingTransition(string level)
        {
            AudioManager.Instance.PlaySoundEffect(9);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(level);

        }

    }
}