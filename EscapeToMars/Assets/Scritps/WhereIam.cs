using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereIam : MonoBehaviour {
    
    [SerializeField]
    private GameObject _uiManagerGO, _gameManagerGO;

    private Animator _trasitionSceneAnim;

    public static WhereIam instance;
    
    public int Phase = -1;



    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += CheckPhase;
    }

    private void Start() {
        _trasitionSceneAnim.SetBool("Starting", false);
       
    }

    void CheckPhase(Scene scene, LoadSceneMode sceneMode) {
        Phase = SceneManager.GetActiveScene().buildIndex;

        _trasitionSceneAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();
       

        if (Phase != 1 && Phase != 0 && Phase != 2) {
            Instantiate(_uiManagerGO);
            Instantiate(_gameManagerGO);
        }

       
       

    }

    public void IsTransitinScenesTrue() {
        _trasitionSceneAnim.SetBool("Starting", true);
    }

    public void IsTransitinScenesFalse() {
        _trasitionSceneAnim.SetBool("Starting", false);
    }

    IEnumerator StartingTransition() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Phase);
        _trasitionSceneAnim.SetBool("Starting", false);

    }

  
}
