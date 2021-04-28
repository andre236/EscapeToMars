using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{

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


    public void EquipCustom(int indexSkin)
    {
        PlayerPrefs.SetInt("IndexSkin", indexSkin);
        Debug.Log("Skin: " + SkinsPrefabs[PlayerPrefs.GetInt("IndexSkin")].name + " equipada com sucesso!");
    }

    public void RedeemSkin(int numberStars)
    {
        if(numberStars >= StarsCollection.instance.TotalStars)
        {
            
        }
    }

}
