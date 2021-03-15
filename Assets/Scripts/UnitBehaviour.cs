using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitBehaviour : Mob
{
    private ActiveEnemies _activeEnemies;
    private int _activeEnemiesCount;

    private GameObject _enemyToAttack;
    protected UnitInfo _selectedEnemyInfo;
    
   
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [Inject]
    private void Construct(ActiveEnemies activeEnemies)
    {
        _activeEnemies = activeEnemies;
    }

    private void Update()
    {
        if (!_isGameOff)
        {
            if (!_isTriggeredOnMob)
            {
                CheckAllEnemies();
            }
            else if (_enemyToAttack)
            {
                if (_isAttackGone)
                {
                    OffFightAnim();
                    CheckEnemyDistance(_enemyToAttack);
                    AttackUnit(_enemyToAttack);
                }
            }
            else
            {
                _isTriggeredOnMob = false;
                OffFightAnim();
            }
        }
    }
    protected virtual void AttackUnit(GameObject enemyToAttack) { }



    protected void CheckEnemyAlive()
    {
        if (_selectedEnemyInfo.GetHealth() <= 0)
        {
            _activeEnemies.DeleteEnemy(_enemyToAttack);
            Destroy(_enemyToAttack);
            OffFightAnim();
        }
    }
    private void CheckEnemyDistance(GameObject enemyToAttack)
    {
        Vector3 direction = enemyToAttack.transform.position - transform.position;
        if (direction.magnitude > _attackRadius)
        {
            _isTriggeredOnMob = false;
            _enemyToAttack = null;
            OffFightAnim();
        }

    }
    private void CheckAllEnemies()
    {
        _activeEnemiesCount = _activeEnemies.GetEnemies().Count;

        foreach (GameObject enemy in _activeEnemies.GetEnemies())
        {
            Vector3 directionUnit = enemy.transform.position - transform.position;
            if (directionUnit.magnitude < _attackRadius)
            {
                _isTriggeredOnMob = true;
                _enemyToAttack = enemy;
                _selectedEnemyInfo = _enemyToAttack.GetComponent<UnitInfo>();
            }
        }
    }
   
}
