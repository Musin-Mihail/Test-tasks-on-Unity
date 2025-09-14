using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public GameObject _finish;
    public GameObject _sausage;
    public Text _finishTime;
    float _seconds;
    public Text _time;
    public static int _victory;
    void Start() 
    {
        _victory = 0;
        _seconds = 0;
    }
    void Update() 
    {
        if(_victory == 0)
        {
            _seconds += Time.deltaTime;
            float minutes = Mathf.FloorToInt(_seconds / 60); 
            float seconds = Mathf.FloorToInt(_seconds % 60);
            _time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "Head")
        {
            _victory = 1;
            _finish.SetActive(true);
            float minutes = Mathf.FloorToInt(_seconds / 60); 
            float seconds = Mathf.FloorToInt(_seconds % 60);
            _finishTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            _sausage.SetActive(false);
        }
    }
}
