using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject _menu;
    public GameObject _sausage;
    public GameObject _finish;
    public void RestarGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void HideMenu()
    {
        _menu.SetActive(false);
        if(Finish._victory == 0)
        {
            _sausage.SetActive(true);
        }
        else
        {
            _finish.SetActive(true);
        }
    }
    public void ShowMenu()
    {
        _menu.SetActive(true);
        _sausage.SetActive(false);
        _finish.SetActive(false);
    }
}