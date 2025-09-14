using UnityEngine;
using System.IO;
public class Menu : MonoBehaviour
{
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    void Start()
    {
        Level1.SetActive(true);
        if(File.Exists(Application.persistentDataPath  + @"/Level2"))
        {
            Level2.SetActive(true);
        }
        if(File.Exists(Application.persistentDataPath  + @"/Level3"))
        {
            Level3.SetActive(true);
        }
    }
}
