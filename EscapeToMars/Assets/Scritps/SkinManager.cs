using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public int CurrentSkinIndex { get; private set; } = 0;

    public GameObject[] SkinsPrefabs;

    public static SkinManager instance;


    void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
    public void EquipCustom(int indexSkin)
    {
        CurrentSkinIndex = indexSkin;
    }

    public void RedeemSkin()
    {

    }

}
