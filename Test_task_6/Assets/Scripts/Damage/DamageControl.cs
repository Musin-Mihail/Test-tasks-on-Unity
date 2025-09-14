using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageControl
{
    private float _value;
    private readonly Transform _damage;
    public readonly TMP_Text _damageValue;
    public bool Busy;

    public DamageControl(Transform damage)
    {
        _damage = damage;
        Busy = false;
        RectTransform transform = _damage.GetChild(0) as RectTransform;
        _damageValue = transform.GetComponent<TMP_Text>();
    }

    public IEnumerator Move(Vector3 start, int damage)
    {
        _damage.GetChild(0);
        Busy = true;
        _damageValue.text = damage.ToString();
        _damage.position = start + new Vector3(0, 1, 0);
        _damage.gameObject.SetActive(true);
        var endPosition = start + new Vector3(0, 3, 0);
        while (Vector3.Distance(_damage.position, endPosition) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            _damage.position = Vector3.MoveTowards(_damage.position, endPosition, 0.05f);
        }
        _damage.gameObject.SetActive(false);
        Busy = false;
    }
}