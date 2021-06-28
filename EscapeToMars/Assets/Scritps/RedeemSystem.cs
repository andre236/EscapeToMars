using UnityEngine;
using UnityEngine.UI;
public class RedeemSystem : MonoBehaviour
{
    [SerializeField]
    private int _skinPrice, _skinIndex;

    [SerializeField]
    private bool _canRedeem, _canBuy, _canEquip;
    
    [SerializeField]
    private Text _equipTXT, _skinPriceTXT;
    private Image _splashArtIMG;

    public void Redeem()
    {
        int totalStars = PlayerPrefs.GetInt("TotalStars");
        _canBuy = totalStars >= _skinPrice;

        if (_canRedeem)
        {
            if(_canBuy)
            {
                _equipTXT.text = "Equip";
                _canEquip = true;
                PlayerPrefs.SetInt("Have_Skin" + _skinIndex, _skinIndex);
            }
            else
            {
                _equipTXT.color = Color.red;
            }
        }

        if (_canEquip)
        {
            if (PlayerPrefs.GetInt("IndexSkin") != _skinIndex)
            {
                _equipTXT.color = Color.white;
                _equipTXT.text = "Equipped";
                PlayerPrefs.SetInt("IndexSkin", _skinIndex);
            }
        }

    }

}
