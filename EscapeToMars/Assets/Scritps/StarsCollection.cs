using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCollection : MonoBehaviour
{
    private Text _starsCollection;
    public int TotalStars { get; set; }

    void Awake()
    {
        _starsCollection = GameObject.Find("StarsCollectionTXT").GetComponent<Text>();
        TotalStars = GameObject.FindGameObjectsWithTag("AbleStar").Length;
        _starsCollection.text = TotalStars.ToString();
       
    }

    void Start()
    {
        _starsCollection.text = TotalStars.ToString();
    }

    private void Update()
    {
        TotalStars = GameObject.FindGameObjectsWithTag("AbleStar").Length;

        _starsCollection.text = TotalStars.ToString() + "/90";
    }



}
