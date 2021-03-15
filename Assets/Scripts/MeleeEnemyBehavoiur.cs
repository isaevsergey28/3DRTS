using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavoiur : EnemyBehaviour
{
    protected override void AttackUnit(GameObject unitToAttack)
    {
        Vector3 direction = unitToAttack.transform.position - transform.position;
        if (direction.magnitude > _stopRadiusForMob)
        {
            _animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, unitToAttack.transform.position, Time.fixedDeltaTime);
        }
        else if(_selectedUnitInfo.GetHealth() > 0)
        {
            _animator.SetBool("isWalking", false);
            StartCoroutine(GiveDamageForUnit());
        }
        transform.LookAt(unitToAttack.transform);
    }
    private IEnumerator GiveDamageForUnit()
    {
        _isAttackGone = false;
        _animator.SetBool("isMeleeFight", true);
        yield return new WaitForSeconds(1f);
        _selectedUnitInfo.SetHealth(_damage);
        _isAttackGone = true;
        base.CheckUnitAlive();
    }

    protected override void AttackFortress()
    {
        base.AttackFortress();
        StartCoroutine(GiveDamageForFortress());
    }
    private IEnumerator GiveDamageForFortress()
    {
        _isAttackGone = false;
        _animator.SetBool("isMeleeFight", true);
        yield return new WaitForSeconds(1f);
        _fortInfo.SetFortressHealth(_damage);
        _isAttackGone = true;
        base.CheckFortressAlive();
    }
}
