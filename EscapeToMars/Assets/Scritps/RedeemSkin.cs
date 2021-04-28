using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedeemSkin : MonoBehaviour
{
    [SerializeField]
    private Text _redeemTXT;
    public void RedeemSkinMethod(int numberStars)
    {
        if (PlayerPrefs.GetInt("TotalStars") >= numberStars)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _redeemTXT.color = Color.red;
            Debug.Log("Not Enough, you have: "+PlayerPrefs.GetInt("TotalStars")+ " and you need " + numberStars);
        }
    }
}
