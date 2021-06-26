using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private int _state, _priceSkin, _indexSkin;

    [SerializeField]
    private Text _equipTXT;
    private Button _equipButton;
    
    public GameObject[] SkinsPrefabs;
    public static SkinManager instance;
    
    void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ControlEquip(int state, int priceSkin, int indexSkin)
    {
        switch (state)
        {
            case 0:
                _equipTXT.text = "Unavaliable";
                _equipButton.interactable = false;
                break;
            case 1:
                _equipTXT.text = "Redeem Skin";
                _equipButton.interactable = true;
                RedeemSkin(priceSkin, indexSkin);
                break;
            case 2:

                break;
            case 3:
                break;
        }
    }

    public void RedeemSkin(int numberStars, int indexSkin)
    {
        Color redColor = new Color(173, 23, 44);
       
        if (numberStars <= StarsCollection.instance.TotalStars && PlayerPrefs.GetInt("HaveSkin"+indexSkin) == 0)
        {
            _equipTXT.color = Color.white;
            PlayerPrefs.SetInt("HaveSkin" + indexSkin.ToString(), 1);
            _equipTXT.text = "Equip";
        }
        else
        {
            _equipTXT.color = redColor;
        }
    }


    public void EquipCustom(int indexSkin)
    {
        if(PlayerPrefs.GetInt("HaveSkin"+indexSkin) == 1 && PlayerPrefs.GetInt("IndexSkin") != indexSkin)
        {
            PlayerPrefs.SetInt("IndexSkin", indexSkin);
            _equipTXT.text = "Equipped";
            Debug.Log("Skin: " + SkinsPrefabs[PlayerPrefs.GetInt("IndexSkin")].name + " equipada com sucesso!");
        }
        else
        {
            Debug.Log("A skin: " + SkinsPrefabs[PlayerPrefs.GetInt("IndexSkin")].name + " já está equipada!");
        }
    }

}
