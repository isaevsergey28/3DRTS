using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnitBehaviour : UnitBehaviour
{
    
    protected override void AttackUnit(GameObject enemyToAttack)
    {
        if(enemyToAttack)
        {
            Vector3 direction = enemyToAttack.transform.position - transform.position;
            if (direction.magnitude > _stopRadiusForMob)
            {
                _animator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, enemyToAttack.transform.position, Time.fixedDeltaTime);
            }
            else if (_selectedEnemyInfo.GetHealth() > 0)
            {
                _animator.SetBool("isWalking", false);
                StartCoroutine(GiveDamageForEnemy());
            }
            transform.LookAt(enemyToAttack.transform);
        }
        
    }

    private IEnumerator GiveDamageForEnemy()
    {
        
        _isAttackGone = false;
        _animator.SetBool("isMeleeFight", true);
        
        yield return new WaitForSeconds(1f);
        _selectedEnemyInfo.SetHealth(_damage);
        _isAttackGone = true;
        CheckEnemyAlive();
    }
  
}
