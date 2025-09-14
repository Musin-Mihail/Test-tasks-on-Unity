using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl
{
    private float _value;
    private readonly Transform _bullet;
    public bool Busy;

    public BulletControl(Transform bullet)
    {
        _bullet = bullet;
        Busy = false;
    }

    public IEnumerator Move(Transform start, Transform end, float speed)
    {
        Busy = true;
        var startPosition = start.position;
        _bullet.position = startPosition;
        _bullet.gameObject.SetActive(true);
        _value = 0;
        while (_value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            _value += 0.01f * speed;
            var endPosition = end.position;
            var center = Vector3.Lerp(startPosition, endPosition, 0.5f) + new Vector3(0, 5, 0);
            var line = new List<Vector3>
            {
                startPosition,
                center,
                endPosition
            };
            Lerp(line);
            if (!end.gameObject.activeSelf)
            {
                _bullet.gameObject.SetActive(false);
                break;
            }
        }

        _bullet.gameObject.SetActive(false);
        Busy = false;
    }

    private void Lerp(IReadOnlyList<Vector3> line)
    {
        var list = new List<Vector3>();
        for (var i = 0; i < line.Count - 1; i++)
        {
            list.Add(Vector3.Lerp(line[i], line[i + 1], _value));
        }

        _bullet.position = Vector3.Lerp(list[0], list[1], _value);
    }
}