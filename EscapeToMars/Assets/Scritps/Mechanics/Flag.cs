﻿using Managers;
using Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechanics
{
    public class Flag : MonoBehaviour
    {
        [SerializeField]
        private bool _isLastLevel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //int tempPhase = WhereIam.Instance.Phase - 2; // 1 ( Subtract "2" for Scenes: Main Scene, Shop Scene and Level Manager Scene.)
            //int tempNexPhase = WhereIam.Instance.Phase - 1; // 2
            string nameLevel = SceneManager.GetActiveScene().name;

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(1);
                PlayerPrefs.SetInt(nameLevel, 1);

                if (PlayerPrefs.GetInt("Stars" + nameLevel) < 1)
                {
                    PlayerPrefs.SetInt("Stars" + nameLevel, 1);
                    PlayerPrefs.SetInt("TotalStars", +1);
                }

                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(2);
                PlayerPrefs.SetInt(nameLevel, 1);
                if (PlayerPrefs.GetInt("Stars" + nameLevel) < 2)
                {
                    PlayerPrefs.SetInt("Stars" + nameLevel, 2);
                    PlayerPrefs.SetInt("TotalStars", +2);

                }
                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }

            if (collision.gameObject.CompareTag("Player") && ScoreManager.instance.CurrentPointsPlayer >= ScoreManager.instance.TotalPointsA + ScoreManager.instance.TotalPointsB + ScoreManager.instance.TotalPointsC && !_isLastLevel)
            {
                GameManager.Instance.SuccessfullyLevel(3);
                PlayerPrefs.SetInt(nameLevel, 1);
                if (PlayerPrefs.GetInt("Stars" + nameLevel) < 3)
                {
                    PlayerPrefs.SetInt("Stars" + nameLevel, 3);
                }

                //PlayerPrefs.SetInt("Level" + tempNexPhase, 1);
            }


        }

    }
}