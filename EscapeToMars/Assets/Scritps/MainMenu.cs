using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator _trasitionSceneAnim;

    private void Awake()
    {
        _trasitionSceneAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();
    }



    public void LoadSelectPhaseMenu(string nameScene)
    {
        AudioManager.Instance.PlaySoundEffect(9);
        _trasitionSceneAnim.SetBool("Starting", true);
        StartCoroutine(LoadingTransitionScene(nameScene));
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    IEnumerator LoadingTransitionScene(string nameScene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nameScene);

    }


}
