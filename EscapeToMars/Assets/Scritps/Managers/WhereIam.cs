using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereIam : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiManagerGO, _gameManagerGO;

    private Animator _trasitionSceneAnim;

    public int Phase;

    public static WhereIam Instance;

    void Awake()
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
        Phase = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += CheckPhase;
    }

    void Start()
    {
        _trasitionSceneAnim.SetBool("Starting", false);
    }

    void CheckPhase(Scene scene, LoadSceneMode sceneMode)
    {
        AudioManager.Instance.PlaySoundEffect(8);
        Phase = SceneManager.GetActiveScene().buildIndex;
        _trasitionSceneAnim = GameObject.Find("TransitionScene").GetComponent<Animator>();

        if (Phase != 1 && Phase != 0 && Phase != 2)
        {
            Instantiate(_uiManagerGO);
            Instantiate(_gameManagerGO);
        }
    }

    public void IsTransitinScenesTrue()
    {
        _trasitionSceneAnim.SetBool("Starting", true);
    }

    public void IsTransitinScenesFalse()
    {
        _trasitionSceneAnim.SetBool("Starting", false);
    }

    public void LoadSkinManager()
    {
        StartCoroutine("LoadingTransitionSceneSkin");
    }

    IEnumerator LoadingTransitionSceneSkin()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SELECT_SKIN");
    }



}
