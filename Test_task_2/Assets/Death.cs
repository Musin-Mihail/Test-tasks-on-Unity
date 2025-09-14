using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject _reset;
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "Head")
        {
            _reset.GetComponent<Menu>().RestarGame();
        }
    }
}
