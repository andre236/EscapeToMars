using Managers;
using Systems;
using UnityEngine;

namespace Mechanics
{
    public class Flag : MonoBehaviour
    {
        [SerializeField]
        private bool _isLastLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int tempPhase = WhereIam.Instance.Phase - 2; // 1 ( Subtract "2" for Scenes: Main Scene, Shop Scene and Level Manager Scene.)
            int tempNexPhase = WhereIam.Instance.Phase - 1; // 2

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(1);
                PlayerPrefs.SetInt("Level" + tempPhase, 1);

                if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 1)
                {
                    PlayerPrefs.SetInt("StarsLevel" + tempPhase, 1);
                    PlayerPrefs.SetInt("TotalStars", +1);
                }

                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(2);
                PlayerPrefs.SetInt("Level" + tempPhase, 1);
                if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 2)
                {
                    PlayerPrefs.SetInt("StarsLevel" + tempPhase, 2);
                    PlayerPrefs.SetInt("TotalStars", +2);

                }
                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB + ScoreManager.instance.TotalPointsC && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(3);
                PlayerPrefs.SetInt("Level" + tempPhase, 1);
                if (PlayerPrefs.GetInt("StarsLevel" + tempPhase) < 3)
                {
                    PlayerPrefs.SetInt("StarsLevel" + tempPhase, 3);
                }

                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }


        }

    }
}