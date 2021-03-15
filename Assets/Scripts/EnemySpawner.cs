using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _roundObject;
    private RoundSystem _roundSystem;

    private ActiveEnemies _allEnemies;
    
    private IEnemyFactory _enemyFactory;

    private int _oneWaveCount = 5;

    private int _waveCount = 0;

    private bool _isEnemySpawned = false;

    private void Start()
    {
        _roundSystem = _roundObject.GetComponent<RoundSystem>();
        _allEnemies = GetComponent<ActiveEnemies>();
    }

    [Inject]
    private void Construct(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        _enemyFactory.LoadEnemy();
    }
    
    public void SpawnEnemy(UnitType unitType, Vector3 at)
    {
        _allEnemies.AddEnemy(_enemyFactory.CreateEnemy(unitType, at));
    }

    private void Update()
    {
        if(_roundSystem._roundStart)
        {
            _roundSystem._roundStart = false;
            _isEnemySpawned = true;
            _waveCount++;
            SpawnEnemies(_waveCount);
        }
        if(_allEnemies.GetEnemies().Count == 0 && _isEnemySpawned)
        {
            _isEnemySpawned = false;
            
            _roundSystem._isTimeBetweenRoundsStart = false;
            _roundSystem.AddMoney();
        }
    }
    private void SpawnEnemies(int wave)
    {
        for (int i = 0; i < wave * _oneWaveCount; i++) 
        {
            switch(Random.Range(0, 2))
            {
                case 0:
                    SpawnEnemy(UnitType.Melee, Vector3.zero);
                    break;
                case 1:
                    SpawnEnemy(UnitType.Ranged, Vector3.zero);
                    break;
            }
        }
    }
}
