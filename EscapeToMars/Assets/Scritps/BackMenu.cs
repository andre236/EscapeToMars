using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMenu : MonoBehaviour
{
    private Button _backMenuButton;
    private Animator _transitionAnim;



    private void Awake()
    {
        _backMenuButton = GetComponent<Button>();
        _transitionAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();
        _backMenuButton.onClick.AddListener(LoadMainMenu);


    }


    public void LoadMainMenu()
    {
        _transitionAnim.SetBool("Starting", true);
        StartCoroutine("LoadingTransitionScene");
    }
  

    IEnumerator LoadingTransitionScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MAIN_SCENE");
    }

}
