using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public GameObject[] SkinsPrefabs;
    public static SkinManager instance; // PlayerPrefs.GetInt("IndexSkin") é a index que determina a skin que será instanciada.

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


}
