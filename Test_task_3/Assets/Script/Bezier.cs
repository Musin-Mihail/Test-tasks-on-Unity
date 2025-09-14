using System.Collections.Generic;
using UnityEngine;
public class Bezier
{
    public static Vector3 GetPoint2(List<Transform> _listVector3, float t)
    {
        List<Vector3> _newListVector3 = new List<Vector3>();
        foreach (var item in _listVector3)
        {
            _newListVector3.Add(item.position);
        }
        int _count = 0;
        int _count2 = 0;
        for (int j = 0; j < _listVector3.Count-1; j++)
        {
            _count2 = _newListVector3.Count-1;
            for (int i = _count; i < _count2; i++)
            {
                _newListVector3.Add(Lerp(_newListVector3[i],_newListVector3[i+1],t));
                _count++;
            }
            _count++;
        }
        return(Lerp(_newListVector3[_newListVector3.Count-2],_newListVector3[_newListVector3.Count-1],t));
    }
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return a +(b-a) * t;
    }
}