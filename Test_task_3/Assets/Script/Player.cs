using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<GameObject> _mainGameObjects;
    public List<GameObject> _gameObjects;
    public List<Vector3> Vertox;
    public List<int> Tringle;
    public List<Vector3> Normals;
    Vector3 _oldVector3;
    public GameObject mesh;
    int count = 0;
    void Start()
    {
        _mainGameObjects = _gameObjects;
        _oldVector3 = transform.position;
        foreach (var item in _mainGameObjects)
        {
            Vertox.Add(item.transform.position);
            Normals.Add(-Vector3.down);
        }
    }
    void Update ()
    {
        if(Vector3.Distance(transform.position, _oldVector3) > 0.1f)
        {
            Mesh _mesh = new Mesh();
            _mesh.name = "Test Mesh2";
            count++;
            _oldVector3 = transform.position;
            foreach (var item in _mainGameObjects)
            {
                Vertox.Add(item.transform.position);
                Normals.Add(-Vector3.down);
            }
            for (int t = 0; t < _mainGameObjects.Count-1; t++)
            {
                for (int i = 0; i < count; i++)
                {
                    int Index1 = i * _mainGameObjects.Count + t;
                    int Index2 = Index1 + _mainGameObjects.Count;
                    int Index3 = Index2 + 1;
                    int Index4 = Index1;
                    int Index5 = Index3;
                    int Index6 = Index1 + 1;
                    Tringle.Add(Index1);
                    Tringle.Add(Index2);
                    Tringle.Add(Index3);
                    Tringle.Add(Index4);
                    Tringle.Add(Index5);
                    Tringle.Add(Index6);
                } 
            }
            _mesh.SetVertices(Vertox);
            _mesh.SetTriangles(Tringle, 0);
            _mesh.SetNormals(Normals);
            _mesh.RecalculateNormals();
            Tringle.Clear();
            mesh.GetComponent<MeshFilter>().sharedMesh = _mesh; 
            mesh.GetComponent<MeshCollider>().sharedMesh = _mesh; 
        } 
    }
}