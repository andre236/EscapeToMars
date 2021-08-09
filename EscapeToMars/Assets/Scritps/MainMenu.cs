using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.F9) && Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < 30; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetPlayerPrefs();
        }
    }
    */
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
