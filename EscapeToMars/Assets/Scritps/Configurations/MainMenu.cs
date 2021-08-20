using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Configurations
{
    public class MainMenu : MonoBehaviour
    {
        private Animator _trasitionSceneAnim;

        private void Awake()
        {
            _trasitionSceneAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();
            StartingPrefs();
        }

        /*
        private void Update()
        {
            //EnterGodMod();
        }
        */

        void EnterGodMod()
        {
            List<int> listOneStarPrefs = new List<int>();
            List<int> listTwoStarPrefs = new List<int>();
            List<int> listThreeStarPrefs = new List<int>();

            if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.U))
            {
                for (int i = 0; i < 30; i++)
                {
                    PlayerPrefs.SetInt("Level" + i, 1);
                }
                Debug.Log("Todos os leveis foram desbloqueados.");
            }

            if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                for (int i = 1; i < 30; i++)
                {
                    PlayerPrefs.SetInt("StarsLevel" + i, 1);
                }
                Debug.Log("Adicionado 1 estrela em todos os leveis.");
            }

            if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 1; i < 30; i++)
                {
                    PlayerPrefs.SetInt("StarsLevel" + i, 2);
                }
                Debug.Log("Adicionado 2 estrela em todos os leveis.");
            }

            if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 1; i < 30; i++)
                {
                    PlayerPrefs.SetInt("StarsLevel" + i, 3);
                }
                Debug.Log("Adicionado 3 estrela em todos os leveis.");
            }

            if (Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                ResetPlayerPrefs();
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                for (int i = 1; i <= 30; i++)
                {
                    if (PlayerPrefs.GetInt("StarsLevel" + i) > 0)
                    {
                        listOneStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                    }
                }
                Debug.Log(listOneStarPrefs.Count);
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 1; i <= 30; i++)
                {
                    if (PlayerPrefs.GetInt("StarsLevel" + i) > 1)
                    {
                        listTwoStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                    }
                }
                Debug.Log(listTwoStarPrefs.Count);
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 1; i <= 30; i++)
                {
                    if (PlayerPrefs.GetInt("StarsLevel" + i) == 3)
                    {
                        listThreeStarPrefs.Add(PlayerPrefs.GetInt("StarsLevel" + i));
                    }
                }
                Debug.Log(listThreeStarPrefs.Count);
            }
        }

        public void ResetPlayerPrefs()
        {

            PlayerPrefs.DeleteAll();
        }

        public void LoadSelectPhaseMenu(string nameScene)
        {
            AudioManager.Instance.PlaySoundEffect(9);
            _trasitionSceneAnim.SetBool("Starting", true);
            StartCoroutine(LoadingTransitionScene(nameScene));
        }

        public void ExitApp()
        {
            AudioManager.Instance.PlaySoundEffect(9);
            _trasitionSceneAnim.SetBool("Starting", true);
            StartCoroutine("ExitingAPP");
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        void StartingPrefs()
        {
            if (!PlayerPrefs.HasKey("IndexSkin"))
            {
                PlayerPrefs.SetInt("IndexSkin", 0);
            }
            if (!PlayerPrefs.HasKey("Kills"))
            {
                PlayerPrefs.SetInt("Kills", 0);
            }


        }

        IEnumerator LoadingTransitionScene(string nameScene)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nameScene);

        }

        IEnumerator ExitingAPP()
        {
            yield return new WaitForSeconds(2f);
            Application.Quit();
        }


    }
}