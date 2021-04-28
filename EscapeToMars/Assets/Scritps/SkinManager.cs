using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _skinsPrefabs;

    public static SkinManager instance;

    void Awake()
    {
       if(instance == null && WhereIam.instance.Phase != 0 && WhereIam.instance.Phase != 1)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    public void EquipCustom()
    {

    }

    public void RedeemSkin()
    {

    }

}
