using System.Collections;
using UnityEngine;

namespace Managers
{
    public class CustomManager : MonoBehaviour
    {
        private GameObject _showMessageUI, _windowMessage, _congratsTXT, _alreadyTXT, _invalidCodeTXT, _expiredCodeTXT;

        public static CustomManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _showMessageUI = GameObject.Find("ShowMessageUI");
            _windowMessage = GameObject.Find("WindowMessage");
            _congratsTXT = GameObject.Find("CongratsTXT");
            _alreadyTXT = GameObject.Find("AlreadyTXT");
            _invalidCodeTXT = GameObject.Find("InvalidCodeTXT");
            _expiredCodeTXT = GameObject.Find("ExpiredCodeTXT");

            _showMessageUI.SetActive(false);
            _windowMessage.SetActive(false);
            _congratsTXT.gameObject.SetActive(false);
            _alreadyTXT.gameObject.SetActive(false);
            _invalidCodeTXT.gameObject.SetActive(false);
            _expiredCodeTXT.gameObject.SetActive(false);
        }


        public void ShowMessageUI(string message)
        {
            _showMessageUI.SetActive(true);
            StartCoroutine(ShowWindowMessage(message));
        }

        IEnumerator ShowWindowMessage(string message)
        {
            yield return new WaitForSeconds(3f);
            _windowMessage.SetActive(true);
            if (message == "congrats")
            {
                _congratsTXT.gameObject.SetActive(true);
            }
            if (message == "invalid")
            {
                _invalidCodeTXT.gameObject.SetActive(true);
            }
            if (message == "expired")
            {
                _expiredCodeTXT.gameObject.SetActive(true);
            }
        }
    }
}