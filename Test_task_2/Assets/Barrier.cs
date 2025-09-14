using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float _speed;
    void Start()
    {
        StartCoroutine(Spin());
    }
    void Update()
    {
        
    }
    IEnumerator Spin()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.001f);
            transform.Rotate(0.0f, 0.0f, _speed);
        }
    }
}
