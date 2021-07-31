using System;
using UnityEngine;
using UnityEngine.UI;
public class RedeemSystem : MonoBehaviour
{
    private string _codeSkin;

    [SerializeField]
    private int _skinPrice, _skinIndexCard;

    [SerializeField]
    private bool _isAvailable, _canRedeem, _canEquip, _haveSkin;


    [SerializeField]
    private Text _equipTXT, _skinPriceTXT;
    private Image _splashArtIMG;

    private void Awake()
    {

    }

    private void Start()
    {
        GetData();
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("Have_Skin" + _skinIndexCard) == _skinIndexCard && _haveSkin && PlayerPrefs.GetInt("IndexSkin") != _skinIndexCard)
        {
            _equipTXT.text = "Equip";
        }
    }


    void GetData()
    {
        Button actionButton = GetComponent<Button>();
        var skinEquipped = PlayerPrefs.GetInt("IndexSkin");
        var haveSkinIndex = PlayerPrefs.GetInt("Have_Skin" + _skinIndexCard);
        int totalStars = PlayerPrefs.GetInt("TotalStars");

        _haveSkin = haveSkinIndex == _skinIndexCard; // Tem a skin na PlayerPrefs.
        _canEquip = _haveSkin && skinEquipped != _skinIndexCard; // Se tem a skin e não está equipada
        _canRedeem = _isAvailable && totalStars >= _skinPrice && !_haveSkin; // Se está disponível, tem estrelas e não tem a skin.

        if (!_isAvailable)
        {
            _equipTXT.text = "Unavaliable";
        }
        else if (_isAvailable && skinEquipped == _skinIndexCard)
        {
            _equipTXT.text = "Equipped";
        } 
        else if (_isAvailable && skinEquipped != _skinIndexCard && _haveSkin)
        {
            _equipTXT.text = "Equip";
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(EquipSkin);
        }
        else if (_isAvailable && skinEquipped != _skinIndexCard && _canRedeem)
        {
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(AcquireSkin);
        }
        else if (_isAvailable && skinEquipped != _skinIndexCard && !_canRedeem)
        {
            _equipTXT.color = Color.red;
            _equipTXT.text = "Redeem";
        }

        _skinPriceTXT.text = _skinPrice.ToString();


    }

    public void AcquireSkin()
    {
        Button actionButton = GetComponent<Button>();
        bool canAcquire = PlayerPrefs.GetInt("TotalStars") >= _skinPrice && !_haveSkin;


        if (_canRedeem)
        {
            _equipTXT.text = "Equip";
            PlayerPrefs.SetInt("Have_Skin" + _skinIndexCard, _skinIndexCard);
            _canEquip = true;
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(EquipSkin);
        }
        else
        {
            _equipTXT.text = "Redeem";
            _equipTXT.color = Color.red;
        }

    }

    public void EquipSkin()
    {
        var skinEquipped = PlayerPrefs.GetInt("IndexSkin");

        if (_skinIndexCard != skinEquipped && _canEquip)
        {
            PlayerPrefs.SetInt("IndexSkin", _skinIndexCard);
            _equipTXT.text = "Equipped";
        }
        else if (_skinIndexCard == skinEquipped && _canEquip)
        {
            _equipTXT.text = "Equipped";
        }

    }

    public void RedeemSkinsByKey(string textRedeem)
    {

        // VAPOR SKIN = vapor_01 == index = 7
        // DPCB = sthellyeah == index = 5
        // TANGO = letsdance == index = 6
        // MEGA A.A = megamanrules == index = 8 e 9
        DateTime date31ago = DateTime.Parse("31/08/2021 23:59");
        _codeSkin = textRedeem.ToLower();


        if(date31ago > DateTime.Now)
        {
            switch (_codeSkin)
            {
                case "vapor_01":
                    CustomManager.Instance.ShowMessageUI("congrats");
                    PlayerPrefs.SetInt("Have_Skin" + 7, 7);
                    Debug.Log("Liberou a skin VAPOR!");
                    break;
                case "sthellyeah":
                    CustomManager.Instance.ShowMessageUI("congrats");
                    PlayerPrefs.SetInt("Have_Skin" + 5, 5);
                    Debug.Log("Liberou a skin DPCB!");
                    break;
                case "letsdance":
                    CustomManager.Instance.ShowMessageUI("congrats");
                    PlayerPrefs.SetInt("Have_Skin" + 6, 6);
                    Debug.Log("Liberou a skin TANGO!");
                    break;
                case "megamanrules":
                    CustomManager.Instance.ShowMessageUI("congrats");
                    PlayerPrefs.SetInt("Have_Skin" + 8, 8);
                    PlayerPrefs.SetInt("Have_Skin" + 9, 9);
                    Debug.Log("Liberou a skin MEGA A.A");
                    break;
                default:
                    CustomManager.Instance.ShowMessageUI("invalid");
                    break;
            }
        }
        else
        {
            CustomManager.Instance.ShowMessageUI("expired");
        }
        

 

    }



}
