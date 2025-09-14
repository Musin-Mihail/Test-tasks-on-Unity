using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<Vector3> DeadEnemy;
    public static Action EndCoin;
    public static Action<List<Transform>> AddEnemies;
    public static Action<Transform, Vector3> SpawnEnemy;
    public static Func<Vector3, int> Hit;
}