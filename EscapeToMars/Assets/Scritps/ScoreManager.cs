using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public int CurrentPointsPlayer { get; private set; } = 0;
    public int TotalPointsCollected { get; private set; }

    public int TotalStarsCollected { get; private set; } = 0;

    public int TotalPointsA { get; private set; }
    public int TotalPointsB { get; private set; }
    public int TotalPointsC { get; private set; }
    public int AllPointsOnMap { get; set; }


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;

       

    }

 
    void Load(Scene scene, LoadSceneMode mode) {

        if (WhereIam.Instance.Phase != 0 && WhereIam.Instance.Phase != 1) {
            TotalPointsA = GameObject.FindGameObjectsWithTag("Point").Length;
        }
    }

    public void ReferencePointsB() {

        if (WhereIam.Instance.Phase != 0 && WhereIam.Instance.Phase != 1) {
            TotalPointsB = GameObject.FindGameObjectsWithTag("PointB").Length;
        }
    }

    public void ReferencePointsC() {

        if (WhereIam.Instance.Phase != 0 && WhereIam.Instance.Phase != 1) {
            TotalPointsC = GameObject.FindGameObjectsWithTag("PointC").Length;
        }
    }

    public void IncreaseStars(int numberStars)
    {
        TotalStarsCollected += numberStars;
    }

    public void ResetCurrentPointsPlayer() {
        CurrentPointsPlayer = 0;
    }

    public void IncreasePointsPlayer() {
        CurrentPointsPlayer++;
        TotalPointsCollected++;
    }


}
