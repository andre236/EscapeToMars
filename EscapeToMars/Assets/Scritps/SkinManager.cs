using UnityEngine;
using UnityEngine.UI;

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


}
