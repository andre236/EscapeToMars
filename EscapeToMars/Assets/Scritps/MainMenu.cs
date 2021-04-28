using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private Animator _trasitionSceneAnim;
    

    public void LoadSelectPhaseMenu() {
        _trasitionSceneAnim.SetBool("Starting", true);
        StartCoroutine("LoadingTransitionScene");
       
    }

    private void Awake() {
        _trasitionSceneAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();
       

    }

    public void ExitApp() {
        Application.Quit();
    }
    
    public void OpenITCHIO()
    {
        Application.OpenURL("https://andre-arruda.itch.io/escape-to-mars");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/AncarusBardo");
    }

    IEnumerator LoadingTransitionScene(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SELECT_MAP");

    }
}
