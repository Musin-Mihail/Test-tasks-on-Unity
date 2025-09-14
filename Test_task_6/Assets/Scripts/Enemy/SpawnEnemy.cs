using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy
{
    private readonly List<Transform> _enemies;
    private Transform _enemy;
    private Vector3 _spawnVector;
    private float _randomX;
    private float _randomZ;

    public SpawnEnemy(List<Transform> enemies)
    {
        _enemies = enemies;
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            foreach (var enemy in _enemies)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    _randomX = Random.Range(10, 20);
                    _randomZ = Random.Range(-6, 15);
                    _spawnVector = new Vector3(_randomX, 0.6f, _randomZ);
                    enemy.transform.rotation = Quaternion.identity;
                    EventManager.SpawnEnemy?.Invoke(enemy, _spawnVector);
                    break;
                }
            }
        }
    }
}