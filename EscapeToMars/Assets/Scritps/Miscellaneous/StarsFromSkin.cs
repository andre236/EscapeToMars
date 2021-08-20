using UnityEngine;
using UnityEngine.UI;

public class StarsFromSkin : MonoBehaviour
{
    private Text _stars;

    private void Awake()
    {
        _stars = GameObject.Find("StarsCollectionTXT").GetComponent<Text>();
    }
    void Update()
    {
        _stars.text = PlayerPrefs.GetInt("TotalStars").ToString() + "/90";
    }
}
