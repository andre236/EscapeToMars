using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    private bool _isLastLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int tempPhase = WhereIam.instance.Phase - 1; // 1
        int tempNexPhase = WhereIam.instance.Phase + 0; // 2
        int tempStarsAdd = PlayerPrefs.GetInt("TotalStars");

        if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA && !_isLastLevel)
        {
            GameManager.instance.SuccessfullyLevel(1);
            PlayerPrefs.SetInt("Level" + tempPhase, 1);
            if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 1)
            {
                PlayerPrefs.SetInt("StarsLevel" + tempPhase, 1);
                PlayerPrefs.SetInt("TotalStars", +1);
            }
            PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
        }

        if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB && !_isLastLevel)
        {
            GameManager.instance.SuccessfullyLevel(2);
            PlayerPrefs.SetInt("Level" + tempPhase, 1);
            if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 2)
            {
                PlayerPrefs.SetInt("StarsLevel" + tempPhase, 2);
                PlayerPrefs.SetInt("TotalStars", +2);

            }
            PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
        }

        if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB + ScoreManager.instance.TotalPointsC && !_isLastLevel)
        {
            GameManager.instance.SuccessfullyLevel(3);
            PlayerPrefs.SetInt("Level" + tempPhase, 1);
            if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 3)
            {
                PlayerPrefs.SetInt("StarsLevel" + tempPhase, 3);

            }
            PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
        }

    }

}
