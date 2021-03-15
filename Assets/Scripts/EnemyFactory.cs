using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private object _meleeEnemyPrefab;
    private object _rangedEnemyPrefab;

    private GameObject _enemiesParent;
    private DiContainer _diContainer;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _enemiesParent = GameObject.Find("Enemies");
    }

    public GameObject CreateEnemy(UnitType unitType, Vector3 at)
    {
        GameObject enemy = null;
        switch (unitType)
        {
            case UnitType.Melee:
                enemy = _diContainer.InstantiatePrefab((GameObject)_meleeEnemyPrefab, at, Quaternion.identity, _enemiesParent.transform);
                break;
            case UnitType.Ranged:
                enemy = _diContainer.InstantiatePrefab((GameObject)_rangedEnemyPrefab, at, Quaternion.identity, _enemiesParent.transform);
                break;
        }
        return enemy;
    }
    
    public void LoadEnemy()
    {
        _meleeEnemyPrefab = Resources.Load("MeleeEnemy");
        _rangedEnemyPrefab = Resources.Load("RangedEnemy");
    }
}
