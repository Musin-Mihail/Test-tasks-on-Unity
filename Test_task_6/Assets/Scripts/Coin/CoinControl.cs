using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl
{
    private float _value;
    private readonly Transform _coin;
    public bool Busy;

    public CoinControl(Transform coin)
    {
        _coin = coin;
        Busy = false;
    }

    public IEnumerator Move(Vector3 start)
    {
        Busy = true;
        _coin.position = start;
        _coin.gameObject.SetActive(true);
        var endPosition = new Vector3(-2.6f, 11.3f, 0);
        var center = Vector3.Lerp(start, endPosition, 0.5f) + new Vector3(0, 5, 0);
        var line = new List<Vector3>
        {
            start,
            center,
            endPosition
        };
        _value = 0;
        while (_value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            _value += 0.01f;
            Lerp(line);
        }

        _coin.gameObject.SetActive(false);
        EventManager.EndCoin?.Invoke();
        Busy = false;
    }

    private void Lerp(IReadOnlyList<Vector3> line)
    {
        var list = new List<Vector3>();
        for (var i = 0; i < line.Count - 1; i++)
        {
            list.Add(Vector3.Lerp(line[i], line[i + 1], _value));
        }

        _coin.position = Vector3.Lerp(list[0], list[1], _value);
    }
}