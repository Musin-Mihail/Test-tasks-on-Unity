using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Move : MonoBehaviour
{
    public string Level;
    public List<Transform> _ListTransform;
    int Win = 0;
    static public float t;
    public float Speed;
    void Start()
    {
        Application.targetFrameRate = 60;
        t = 0;
        t += 0.0002f;
        transform.position = Bezier.GetPoint2(_ListTransform, t);
        Vector3 _new = Bezier.GetPoint2(_ListTransform, t + 0.01f);
        transform.LookAt(_new);
        t += Speed;
    }
    void Update()
    {
        if(Input.GetMouseButton(0) && t < 1)
        {
            transform.position = Bezier.GetPoint2(_ListTransform, t);
            Vector3 _new = Bezier.GetPoint2(_ListTransform, t + 0.01f);
            transform.LookAt(_new);
            t += Speed;
        }
        if (t >= 0.99f && Win == 0)
        {
            FindObjectOfType<Win>(true).gameObject.SetActive(true);
            FileInfo fi = new FileInfo(Application.persistentDataPath + "/" + Level);
            fi.Create();
            Destroy(GetComponent<Move>());
        }
    }
}