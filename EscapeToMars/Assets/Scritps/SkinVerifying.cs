using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinVerifying : MonoBehaviour
{
    [SerializeField]
    private int _indexSkin;
    [SerializeField]
    private Text _equipTXT;

    // Update is called once per frame
    void Update()
    {
      if(PlayerPrefs.GetInt("IndexSkin") == _indexSkin)
        {
            _equipTXT.text = "Equipped";
        }
        else
        {
            _equipTXT.text = "Equip";
        }
    }
}
